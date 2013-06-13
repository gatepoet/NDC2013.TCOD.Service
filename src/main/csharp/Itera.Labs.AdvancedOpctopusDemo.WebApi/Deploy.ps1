. .\DeployUtil.ps1

$ServiceName = "NDC2013.TCOD.Service"
$ServiceExecutable = $ServiceName + ".exe"

Install-WebApi-Service $ServiceName $ServiceExecutable

