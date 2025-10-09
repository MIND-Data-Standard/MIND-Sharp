Param(
  [string]$Root = (Resolve-Path (Join-Path -Path $PSScriptRoot -ChildPath '..\..')),
  [string]$Session = "default",
  [switch]$WriteArchitecture,
  [switch]$WriteNamespaceStats
)

# Default switches to true if not explicitly passed
if (-not $PSBoundParameters.ContainsKey('WriteArchitecture')) { $WriteArchitecture = $true }
if (-not $PSBoundParameters.ContainsKey('WriteNamespaceStats')) { $WriteNamespaceStats = $true }

function Get-RelativePath {
  param(
    [Parameter(Mandatory=$true)][string]$BasePath,
    [Parameter(Mandatory=$true)][string]$FullPath
  )
  $base = (Resolve-Path $BasePath).Path
  $full = (Resolve-Path $FullPath).Path
  $baseUri = New-Object System.Uri($base + [IO.Path]::DirectorySeparatorChar)
  $fullUri = New-Object System.Uri($full)
  $relUri = $baseUri.MakeRelativeUri($fullUri)
  $rel = [System.Uri]::UnescapeDataString($relUri.ToString())
  return $rel -replace '/', [IO.Path]::DirectorySeparatorChar
}

<#!
Generates lightweight, reproducible AI assistance summaries from repository state.
Outputs JSON artifacts under ai/summaries/ (tracked) WITHOUT reading ignored private context.

Usage Examples:
  powershell -ExecutionPolicy Bypass -File ai/scripts/build-summaries.ps1
  powershell -ExecutionPolicy Bypass -File ai/scripts/build-summaries.ps1 -Session s1
#>

$ErrorActionPreference = 'Stop'
$repoRoot = $Root
$parentOfScript = Split-Path -Parent $PSScriptRoot
$summariesDir = Join-Path -Path $parentOfScript -ChildPath 'summaries'
New-Item -ItemType Directory -Force -Path $summariesDir | Out-Null

function Get-NamespaceStats {
  param([string]$SourceRoot)
  $csFiles = Get-ChildItem -Path $SourceRoot -Recurse -File | Where-Object { $_.Extension -ieq '.cs' -and $_.FullName -notmatch '\\bin\\|\\obj\\' }
  $nsCounts = @{}
  foreach ($f in $csFiles) {
    $content = Get-Content -Raw -Path $f.FullName
    if ($content -match 'namespace\s+([A-Za-z0-9_.]+)') {
      $matches = ([regex]::Matches($content, 'namespace\s+([A-Za-z0-9_.]+)'))
      foreach ($m in $matches) {
        $ns = $m.Groups[1].Value.Trim()
        if (-not $nsCounts.ContainsKey($ns)) { $nsCounts[$ns] = 0 }
        $nsCounts[$ns] += 1
      }
    }
  }
  return $nsCounts.GetEnumerator() | Sort-Object Name | ForEach-Object { [pscustomobject]@{ namespace = $_.Key; fileCount = $_.Value } }
}

function New-ArchitectureSummary {
  param([string]$SourceRoot)
  $projects = Get-ChildItem -Path $SourceRoot -Recurse -File | Where-Object { $_.Extension -ieq '.csproj' -and $_.FullName -notmatch '\\bin\\|\\obj\\' }
  $list = @()
  foreach ($p in $projects) {
    try {
      $projXml = [xml](Get-Content -Raw -Path $p.FullName)
    } catch {
      Write-Warning "Failed to parse XML: $($p.FullName)"
      continue
    }
    $pgs = @()
    if ($projXml.Project.PropertyGroup -is [System.Array]) { $pgs = $projXml.Project.PropertyGroup } elseif ($projXml.Project.PropertyGroup) { $pgs = @($projXml.Project.PropertyGroup) }
    $tfmText = $null
    foreach ($pg in $pgs) {
      if ($pg.TargetFrameworks -and $pg.TargetFrameworks.InnerText) { $tfmText = $pg.TargetFrameworks.InnerText; break }
      if ($pg.TargetFramework -and $pg.TargetFramework.InnerText) { $tfmText = $pg.TargetFramework.InnerText; break }
    }
    if (-not $tfmText) { $tfmText = '' }
    $tfms = if ([string]::IsNullOrWhiteSpace($tfmText)) { @() } else { $tfmText.Split(';') }
    $list += [pscustomobject]@{
      project = (Get-RelativePath -BasePath $SourceRoot -FullPath $p.FullName)
      targetFrameworks = $tfms
    }
  }
  return $list | Sort-Object project
}

$srcRoot = Join-Path $repoRoot 'src'
$timeUtc = [DateTime]::UtcNow.ToString('o')

if ($WriteArchitecture) {
  $arch = New-ArchitectureSummary -SourceRoot $repoRoot
  $archObj = [pscustomobject]@{
    generatedAtUtc = $timeUtc
    session = $Session
    kind = 'architecture-projects'
    projectCount = $arch.Count
    projects = $arch
  }
  $archPath = Join-Path $summariesDir 'architecture-projects.json'
  $archObj | ConvertTo-Json -Depth 6 | Set-Content -Path $archPath -Encoding UTF8
  Write-Host "Wrote $archPath"
}

if ($WriteNamespaceStats) {
  $ns = Get-NamespaceStats -SourceRoot $srcRoot
  $nsObj = [pscustomobject]@{
    generatedAtUtc = $timeUtc
    session = $Session
    kind = 'namespace-distribution'
    namespaceCount = $ns.Count
    namespaces = $ns
  }
  $nsPath = Join-Path $summariesDir 'namespace-distribution.json'
  $nsObj | ConvertTo-Json -Depth 4 | Set-Content -Path $nsPath -Encoding UTF8
  Write-Host "Wrote $nsPath"
}

Write-Host 'Summary generation complete.'
