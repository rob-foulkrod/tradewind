targetScope = 'subscription'


@minLength(1)
@maxLength(64)
@description('Name of the environment that can be used as part of naming resource convention')
param environmentName string

var tags = {
  'env-name': environmentName
}


var resourceToken = toLower(uniqueString(subscription().id, environmentName, location))

@minLength(1)
@description('Primary location for all resources')
param location string

resource rg 'Microsoft.Resources/resourceGroups@2021-04-01' = {
  name: 'rg-${environmentName}'
  location: location
  tags: tags
}

//invoke the resources.bicep file
module resources './webapp.bicep' = {
  scope: rg
  name: 'resourcesDeployment'
  params: {
    location: location
    tags: tags
    webAppName: 'webapp-${environmentName}'
    sku: 'S1'
    resourceToken: resourceToken // making sure the web app name is unique

  }
}

output webAppName string = resources.outputs.webAppName
output webAppUrl string = resources.outputs.webAppUrl
output resourceGroupName string = rg.name
output resourceGroupId string = rg.id
output location string = location
