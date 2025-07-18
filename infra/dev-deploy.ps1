$jsonResult = az deployment sub create `
  --template-file main.bicep `
  --location "EastUS2" `
  --parameters dev.parameters.json `
  | ConvertFrom-Json


Write-Output $jsonResult

$ovar = $jsonResult.properties.outputs
Write-Output $ovar

$webAppName = $jsonResult.properties.outputs.webAppName.value
$webAppUrl = $jsonResult.properties.outputs.webAppUrl.value
$location = $jsonResult.properties.outputs.location.value
$resourceGroupName = $jsonResult.properties.outputs.resourceGroupName.value
$resourceGrouId = $jsonResult.properties.outputs.resourceGroupId.value

#write the all to the Pipeline variable
Write-Host "##vso[task.setvariable variable=webAppName;isoutput=true]$webAppName"
Write-Host "##vso[task.setvariable variable=webAppUrl;isoutput=true]$webAppUrl"
Write-Host "##vso[task.setvariable variable=location;isoutput=true]$location"
Write-Host "##vso[task.setvariable variable=resourceGroupName;isoutput=true]$resourceGroupName"
Write-Host "##vso[task.setvariable variable=resourceGroupId;isoutput=true]$resourceGrouId"

# write for Github (not needed here)
"webAppName=$webAppName" | Out-File -FilePath $env:GITHUB_OUTPUT -Append
"webAppUrl=$webAppUrl" | Out-File -FilePath $env:GITHUB_OUTPUT -Append
"location=$location" | Out-File -FilePath $env:GITHUB_OUTPUT -Append
"resourceGroupName=$resourceGroupName" | Out-File -FilePath $env:GITHUB_OUTPUT -Append
"resourceGroupName=$resourceGroupId" | Out-File -FilePath $env:GITHUB_OUTPUT -Append