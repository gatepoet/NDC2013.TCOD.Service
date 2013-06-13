. .\DeployUtil.ps1

$ServiceName = "Itera.NDC2013.TCOD.Service"
$ServiceExecutable = $ServiceName + ".exe"

Install-WebApi-Service $ServiceName $ServiceExecutable

