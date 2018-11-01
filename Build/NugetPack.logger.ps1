$pathLogger = "..\src\Framework\PropellerLogger\Propeller.Logger.csproj"
$pathProj = "..\src\Framework\PropellerLogger\Propeller.Logger.csproj"
#$pathLogger = "..\src\Framework\PropellerLogger\Propeller.Logger.nuspec"
$pathLoggerSc7 = "..\src\Framework\PropellerLogger\Propeller.Logger.sc7.nuspec"

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

#& $msbuild $pathProj /p:Configuration=sc7 /t:Rebuild /m
& $msbuild $pathProj /p:Configuration=Release /t:Rebuild /m


#nuget pack $pathLoggerSc7 -Prop Configuration=sc7
nuget pack $pathLogger -Prop Configuration=Release