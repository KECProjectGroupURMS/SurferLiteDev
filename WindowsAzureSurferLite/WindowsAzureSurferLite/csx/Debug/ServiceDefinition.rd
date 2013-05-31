<?xml version="1.0" encoding="utf-8"?>
<serviceModel xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance" xmlns:xsd="http://www.w3.org/2001/XMLSchema" name="WindowsAzureSurferLite" generation="1" functional="0" release="0" Id="71a7afc4-6519-43e5-ba11-bad52a624aee" dslVersion="1.2.0.0" xmlns="http://schemas.microsoft.com/dsltools/RDSM">
  <groups>
    <group name="WindowsAzureSurferLiteGroup" generation="1" functional="0" release="0">
      <componentports>
        <inPort name="WCFServiceWebRoleSurferLite:Endpoint1" protocol="http">
          <inToChannel>
            <lBChannelMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/LB:WCFServiceWebRoleSurferLite:Endpoint1" />
          </inToChannel>
        </inPort>
      </componentports>
      <settings>
        <aCS name="WCFServiceWebRoleSurferLite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="">
          <maps>
            <mapMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/MapWCFServiceWebRoleSurferLite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </maps>
        </aCS>
        <aCS name="WCFServiceWebRoleSurferLiteInstances" defaultValue="[1,1,1]">
          <maps>
            <mapMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/MapWCFServiceWebRoleSurferLiteInstances" />
          </maps>
        </aCS>
      </settings>
      <channels>
        <lBChannel name="LB:WCFServiceWebRoleSurferLite:Endpoint1">
          <toPorts>
            <inPortMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLite/Endpoint1" />
          </toPorts>
        </lBChannel>
      </channels>
      <maps>
        <map name="MapWCFServiceWebRoleSurferLite:Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" kind="Identity">
          <setting>
            <aCSMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLite/Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" />
          </setting>
        </map>
        <map name="MapWCFServiceWebRoleSurferLiteInstances" kind="Identity">
          <setting>
            <sCSPolicyIDMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLiteInstances" />
          </setting>
        </map>
      </maps>
      <components>
        <groupHascomponents>
          <role name="WCFServiceWebRoleSurferLite" generation="1" functional="0" release="0" software="C:\Users\Uddhab\Documents\GitHub\SurferLiteDev\WindowsAzureSurferLite\WindowsAzureSurferLite\csx\Debug\roles\WCFServiceWebRoleSurferLite" entryPoint="base\x64\WaHostBootstrapper.exe" parameters="base\x64\WaIISHost.exe " memIndex="1792" hostingEnvironment="frontendadmin" hostingEnvironmentVersion="2">
            <componentports>
              <inPort name="Endpoint1" protocol="http" portRanges="80" />
            </componentports>
            <settings>
              <aCS name="Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString" defaultValue="" />
              <aCS name="__ModelData" defaultValue="&lt;m role=&quot;WCFServiceWebRoleSurferLite&quot; xmlns=&quot;urn:azure:m:v1&quot;&gt;&lt;r name=&quot;WCFServiceWebRoleSurferLite&quot;&gt;&lt;e name=&quot;Endpoint1&quot; /&gt;&lt;/r&gt;&lt;/m&gt;" />
            </settings>
            <resourcereferences>
              <resourceReference name="DiagnosticStore" defaultAmount="[4096,4096,4096]" defaultSticky="true" kind="Directory" />
              <resourceReference name="WCFServiceWebRoleSurferLite.svclog" defaultAmount="[1000,1000,1000]" defaultSticky="true" kind="Directory" />
              <resourceReference name="EventStore" defaultAmount="[1000,1000,1000]" defaultSticky="false" kind="LogStore" />
            </resourcereferences>
          </role>
          <sCSPolicy>
            <sCSPolicyIDMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLiteInstances" />
            <sCSPolicyUpdateDomainMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLiteUpgradeDomains" />
            <sCSPolicyFaultDomainMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLiteFaultDomains" />
          </sCSPolicy>
        </groupHascomponents>
      </components>
      <sCSPolicy>
        <sCSPolicyUpdateDomain name="WCFServiceWebRoleSurferLiteUpgradeDomains" defaultPolicy="[5,5,5]" />
        <sCSPolicyFaultDomain name="WCFServiceWebRoleSurferLiteFaultDomains" defaultPolicy="[2,2,2]" />
        <sCSPolicyID name="WCFServiceWebRoleSurferLiteInstances" defaultPolicy="[1,1,1]" />
      </sCSPolicy>
    </group>
  </groups>
  <implements>
    <implementation Id="b677e1c9-2ebb-489a-ab99-f3c66c3fa845" ref="Microsoft.RedDog.Contract\ServiceContract\WindowsAzureSurferLiteContract@ServiceDefinition">
      <interfacereferences>
        <interfaceReference Id="8297ab67-971b-4e44-8b5a-8d694f25e215" ref="Microsoft.RedDog.Contract\Interface\WCFServiceWebRoleSurferLite:Endpoint1@ServiceDefinition">
          <inPort>
            <inPortMoniker name="/WindowsAzureSurferLite/WindowsAzureSurferLiteGroup/WCFServiceWebRoleSurferLite:Endpoint1" />
          </inPort>
        </interfaceReference>
      </interfacereferences>
    </implementation>
  </implements>
</serviceModel>