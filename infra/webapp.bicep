param webAppName string = uniqueString(resourceGroup().id) // Generate a unique string for the web app name
param sku string = 'S1' // Tier of the App Service plan
param location string = resourceGroup().location // Location for all resources
param resourceToken string
param tags object = {}// Environment name for tagging
var appServicePlanName = toLower('AppServicePlan-${webAppName}')


var abbrs = loadJsonContent('./abbrev.json')


module monitoring 'br/public:avm/ptn/azd/monitoring:0.1.0' = {
  name: 'monitoring'
  params: {
    logAnalyticsName: '${abbrs.operationalInsightsWorkspaces}${resourceToken}'
    applicationInsightsName: '${abbrs.insightsComponents}${resourceToken}'
    applicationInsightsDashboardName: '${abbrs.portalDashboards}${resourceToken}'
    location: location
    tags: tags
  }
}


module serverfarm 'br/public:avm/res/web/serverfarm:0.4.0' = {
  name: 'serverfarmDeployment'
  params: {
    name: appServicePlanName
    location: location
    tags: tags
    skuName: sku
    skuCapacity: 1
    diagnosticSettings: [
      {
        name: 'basicSetting'
        workspaceResourceId: monitoring.outputs.logAnalyticsWorkspaceResourceId
      }
    ]
  }
}




module webApp 'br/public:avm/res/web/site:0.12.0' = {
  name: 'webappdeployment'
  params: {
    kind: 'app'
    tags: tags
    name: webAppName
    serverFarmResourceId: serverfarm.outputs.resourceId
    appInsightResourceId: monitoring.outputs.applicationInsightsResourceId
    diagnosticSettings: [
      {
        name: 'basicSetting'
        workspaceResourceId: monitoring.outputs.logAnalyticsWorkspaceResourceId
      }
    ]
    httpsOnly: true
    location: location
    managedIdentities: {
      systemAssigned: true
    }

    publicNetworkAccess: 'Enabled'

    siteConfig: {
      alwaysOn: true
      metadata: [
        {
          name: 'linuxFxVersion'
          value: 'DOTNETCORE|9.0'
        }
      ]
    }
    appSettingsKeyValuePairs: {
      APPLICATIONINSIGHTS_CONNECTIONSTRING: monitoring.outputs.applicationInsightsConnectionString
    }
  }
}

output webAppUrl string = webApp.outputs.defaultHostname
output webAppName string = webApp.outputs.name
