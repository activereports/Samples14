For Visual Studio 2012 users.
The default target framework version of the sample is 4.6.2. But if target framework version is 4.7.0 or higher, remove 
	<dependentAssembly>
        <assemblyIdentity name="System.IO.Compression" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="0.0.0.0-4.2.0.0" newVersion="4.0.0.0"/>
     </dependentAssembly>

of System.IO.Compression in the app.config. If target framework version of the sample again is 4.6.2, return
	<dependentAssembly>
        <assemblyIdentity name="System.IO.Compression" culture="neutral" publicKeyToken="b77a5c561934e089"/>
        <bindingRedirect oldVersion="0.0.0.0-4.2.0.0" newVersion="4.0.0.0"/>
     </dependentAssembly>
.