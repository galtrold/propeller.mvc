$pathCore = "..\src\Framework\PropellerMvcCore\Propeller.Mvc.Core.csproj"
$pathModel = "..\src\Framework\PropellerMvcModel\Propeller.Mvc.Model.csproj"
$pathPresentation = "..\src\Framework\PropellerMvcPresentation\Propeller.Mvc.Presentation.csproj"

function Resolve-MsBuild {
	$msb2017 = Resolve-Path "${env:ProgramFiles(x86)}\Microsoft Visual Studio\*\*\MSBuild\*\bin\msbuild.exe" -ErrorAction SilentlyContinue
	if($msb2017) {
		Write-Host "Found MSBuild 2017 (or later)."
		Write-Host $msb2017
		return $msb2017
	}

	$msBuild2015 = "${env:ProgramFiles(x86)}\MSBuild\14.0\bin\msbuild.exe"

	if(-not (Test-Path $msBuild2015)) {
		throw 'Could not find MSBuild 2015 or later.'
	}

	Write-Host "Found MSBuild 2015."
	Write-Host $msBuild2015

	return $msBuild2015
}

$msBuild = Resolve-MsBuild

& $msbuild "..\propeller.mvc.sln" /p:Configuration=Release /t:Rebuild /m


nuget pack $pathCore -Prop Configuration=Release
nuget pack $pathModel -Prop Configuration=Release
nuget pack $pathPresentation -Prop Configuration=Release