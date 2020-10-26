$pathCore = "..\src\Framework\PropellerMvcCore\Propeller.Mvc.Core.csproj"
$pathModel = "..\src\Framework\PropellerMvcModel\Propeller.Mvc.Model.csproj"
$pathPresentation = "..\src\Framework\PropellerMvcPresentation\Propeller.Mvc.Presentation.csproj"

function Resolve-MsBuild {
	$msb2017 = Resolve-Path "${env:ProgramFiles(x86)}\Microsoft Visual Studio\*\*\*\*\bin\msbuild.exe" -ErrorAction SilentlyContinue
	if($msb2017) {
		$lastestMsBuild = $msb2017[-1]
		Write-Host "Found MSBuild 2017 (or later)."
		Write-Host $lastestMsBuild
		return $lastestMsBuild
	}

	# $msBuild2015 = "${env:ProgramFiles(x86)}\MSBuild\14.0\bin\msbuild.exe"

	# if(-not (Test-Path $msBuild2015)) {
	# 	throw 'Could not find MSBuild 2015 or later.'
	# }

	# Write-Host "Found MSBuild 2015."
	# Write-Host $msBuild2015

	# return $msBuild2015
}

$msBuild = Resolve-MsBuild
Write-Host "`nBuilding solution`n"
& $msBuild "..\propeller.mvc.sln" /p:Configuration=Release /t:Rebuild /m


nuget pack $pathCore -Prop Configuration=Release -Symbols
nuget pack $pathModel -Prop Configuration=Release -Symbols
nuget pack $pathPresentation -Prop Configuration=Release -Symbols