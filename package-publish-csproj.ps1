#!/usr/bin/env pwsh
<#
.SYNOPSIS
  搜索指定相对目录下所有 .csproj，依次 restore、pack，并汇总 NuGet 包到 ./.nuget
.DESCRIPTION
  - 自动递归查找 $RelativeFolders 中的所有 .csproj
  - 对每个项目执行 dotnet restore
  - dotnet pack --configuration Release，输出到 ./nupkgs
  - 最后将 ./nupkgs 下所有 .nupkg 移动到项目根的 .nuget 目录
.PARAMETER RelativeFolders
  要搜索的相对目录数组，默认 ["src","tests","modules"]
#>

Param(
    [string[]]$RelativeFolders = @("src","modules")
)

# 工作目录
$root = Get-Location

# 输出目录
$nupkgsDir = Join-Path $root "nupkgs"
$nugetDir  = Join-Path $root ".nuget"

# 确保目录存在
foreach ($d in @($nupkgsDir,$nugetDir)) {
    if (-not (Test-Path $d)) { New-Item -Path $d -ItemType Directory | Out-Null }
}

# 1. 搜索所有 .csproj
$csprojs = foreach ($rel in $RelativeFolders) {
    $dir = Join-Path $root $rel
    if (Test-Path $dir) {
        Get-ChildItem -Path $dir -Filter *.csproj -Recurse -File |
            Select-Object -ExpandProperty FullName
    }
}
if (-not $csprojs) {
    Write-Warning "未找到任何 .csproj，检查 RelativeFolders 参数或目录结构。"
    exit 1
}

# 2. 依次 restore + pack
foreach ($proj in $csprojs) {
    Write-Host "`n## Processing $proj ##" -ForegroundColor Cyan

    Write-Host "→ dotnet restore"
    dotnet restore $proj

    Write-Host "→ dotnet pack"
    dotnet pack $proj `
        --configuration Release `
        --no-restore `
        --output $nupkgsDir
}

# 3. 汇总到 .nuget
Get-ChildItem -Path $nupkgsDir -Filter *.nupkg -File | ForEach-Object {
    Write-Host "→ Moving $($_.Name) to $nugetDir"
    Move-Item $_.FullName -Destination $nugetDir -Force
}

Write-Host "`nAll done! .nupkg files are in $nugetDir" -ForegroundColor Green
