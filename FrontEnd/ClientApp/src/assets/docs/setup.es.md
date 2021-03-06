<p><a name="Contents"></a></p><h1> Contenido </h1>
<ul>
<li> <a href="about?id=setup#InstallTools">Instalar herramientas y clonar repositorio</a> </li>
<li> <a href="about?id=setup#DevelopVsCode">Desarrollar con VsCode</a> </li>
<li> <a href="about?id=setup#DevelopVS">Desarrollar con Visual Studio</a> </li>
<li> <a href="about?id=setup#DevelopOther">Desarrollar en otras plataformas</a> </li>
<li> <a href="about?id=setup#Database">Base de datos</a> </li>
<li> <a href="about?id=setup#GoogleCloud">Cuenta de Google Cloud Platform</a> </li>
<li> <a href="about?id=setup#GoogleApi">Google API Keys</a> </li>
</ul><p> Estas páginas de documentación se pueden encontrar en FrontEnd / ClientApp / src / app / assets / docs. Haga correcciones allí y emita una <a href="https://github.com/govmeeting/govmeeting">solicitud de extracción en Gitub.</a> </p><hr /><p><a name="InstallTools"></a></p><h1> Instalar herramientas y clonar repositorio <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p>
<ul>
<li> Instala git. <a href="https://gitforwindows.org">Git para Windows</a> , <a href="https://git-scm.com/download/mac">Git para Mac</a> </li>
<li> Instalar <a href="https://nodejs.org/en/download/">Node.js.</a> </li>
<li> Instale <a href="https://dotnet.microsoft.com/download">.Net Core SDK.</a> </li>
<li> "Fork" el proyecto en github </li>
<li> Clonar el proyecto localmente </li>
<li> Cree una carpeta de hermanos en el repositorio clonado llamado "_SECRETS" </li>
</ul><p> La carpeta "_SECRETS" es para claves y contraseñas que no están almacenadas en el repositorio público. Estos serían necesarios para ejecutar los servicios de la API de Google. </p><hr /><p><a name="DevelopVsCode"></a></p><h1> Desarrollar con VsCode <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p><h2> Instalar VsCode </h2>
<ul>
<li> Instale <a href="https://code.visualstudio.com/download">Visual Studio Code</a> e inícielo. </li>
<li> Abra las extensiones del panel lateral izquierdo e instale: 
<ul>
<li> "Depurador para Chrome" de Microsoft </li>
<li> "C
# para Visual Studio Code" de Microsoft </li>
<li> "SQL Server (mssql)" de Microsoft </li>
<li> "Todo Tree" de Gruntfuggly - muestra TODO líneas en código (opcional) </li>
<li> "Powershell" de Microsoft - para depurar scripts de compilación de Powershell (opcional) </li>
</ul></li>
</ul><h2> Depurar / ejecutar ClientApp y WebApp </h2>
<ul>
<li> Abra la carpeta Govmeeting en VsCode </li>
<li> Abra un panel de terminales en VsCode </li>
<li> cd FrontEnd / ClientApp </li>
<li> npm install </li>
<li> npm start </li>
<li> En el panel de depuración, configure la configuración de inicio "WebApp & ClientApp-W" </li>
<li> Presione F5 (depurar) o Ctrl-F5 (ejecutar sin depurar) </li>
</ul><p> ClientApp se abrirá en un navegador. </p>
<ul>
<li> Haga clic en cualquiera de los elementos del menú "Acerca de" para ver la documentación. </li>
<li> Haga clic en el elemento del menú de ubicación "Boothbay Harbor". Verá el tablero abierto para esta ubicación. </li>
</ul><p> Para verificar que ClientApp está llamando a la API de WebApp para recuperar datos. </p>
<ul>
<li> Haga clic en "Transcripción de corrección de pruebas". Verá un panel de video y texto transcrito. Haz clic en el botón de reproducción de video. </li>
<li> Haga clic en "Agregar etiquetas a la transcripción". Verá una transcripción de una reunión para etiquetar. </li>
<li> Haga clic en "Ver la última reunión". Verá una transcripción completa para ver. </li>
</ul><p> La mayoría de las otras tarjetas de tablero no llaman a la aplicación web sino que devuelven datos de prueba. </p><p> ClientApp es servido por el servidor webpack-dev-server que comenzó con "npm start". WebApp utiliza el servidor Kestrel incluido en Asp.Net Core. El servidor Kestrel responde a llamadas de API web. Pero representa las solicitudes internas de ClientApp al servidor webpack-dev. </p><h2> Depurar / Ejecutar ClientApp independiente </h2>
<ul>
<li> En app.module.ts, cambie "isAspServerRunning" de verdadero a falso. </li>
<li> npm start </li>
<li> En el panel de depuración, configure la configuración de inicio "ClientApp" </li>
<li> Presione F5 (depurar) o Ctrl-F5 (ejecutar sin depurar) </li>
</ul><p> Cuando "isAspServerRunning" se establece en falso, se utilizan servicios de código auxiliar, en lugar de llamar a la API de la aplicación web. Esto es útil cuando solo estamos modificando código en ClientApp. </p><h2> Depuración / Ejecutar WorkflowApp </h2>
<ul>
<li> En el panel de depuración, configure la configuración de inicio "WorkflowApp" </li>
<li> Presione F5 (depurar) o Ctrl-F5 (ejecutar sin depurar) </li>
</ul><p> Cuando WorkflowApp lo inicia: </p>
<ul>
<li> Copia algunos archivos de prueba en la carpeta Datafles / RECEIVED: un archivo PDF de transcripción y un archivo MP4 de grabación. </li>
<li> Procesa el archivo PDF de transcripción y crea un archivo JSON listo para ser etiquetado. </li>
<li> Procese el archivo MP4 de grabación transcribiéndolo en la nube y crea un archivo JSON listo para ser revisado. </li>
</ul><p> Los resultados se pueden encontrar en Datafiles / PROCESSING. Sin embargo, primero deberá configurar una <a href="about?id=setup#GoogleCloud">cuenta de Google Cloud</a> para que la grabación se transcriba. </p><hr /><p><a name="DevelopVS"></a></p><h1> Desarrollar con Visual Studio <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p>
<ul>
<li> Instale la <a href="https://visualstudio.microsoft.com/free-developer-offers/">edición</a> gratuita de <a href="https://visualstudio.microsoft.com/free-developer-offers/">Visual Studio Community Edition.</a> </li>
<li> Durante la instalación, seleccione las cargas de trabajo "ASP.NET" y "escritorio .NET". </li>
<li> Instalar extensiones: (todo por Mads Kristensen) 
<ul>
<li> "NPM Task Runner" </li>
<li> "Command Task Runner" </li>
<li> "Editor de rebajas" </li>
</ul></li>
<li> Abra el archivo de solución "govmeeting.sln" </li>
</ul><h2> Depurar / ejecutar ClientApp y WebApp </h2>
<ul>
<li> En Task Runner Explorer (ClientApp): 
<ul>
<li> ejecutar "instalar" </li>
<li> ejecutar "inicio" </li>
</ul></li>
<li> Establecer proyecto de inicio en "WebApp" </li>
<li> Presione F5 (depurar) o Ctrl-F5 (ejecutar sin depurar) </li>
<li> Se ejecutará WebApp y se abrirá un navegador que mostrará ClientApp. </li>
</ul><p> NOTA: Existe un problema con la configuración de puntos de interrupción en Angular ClientApp en Visual Studio. Ver: <a href="https://github.com/govmeeting/govmeeting/issues/80">Github número 80</a> </p><h2> Debug WorkflowApp </h2>
<ul>
<li> Abre el panel de depuración. </li>
<li> Establezca el proyecto de inicio en "WorkflowApp" </li>
<li> Presione F5 (depurar) o Ctrl-F5 (ejecutar sin depurar) </li>
</ul><p> Nota: Consulte las notas para WorkflowApp en "Código de Visual Studio" </p><hr /><p><a name="DevelopOther"></a></p><h1> Desarrollar en otras plataformas <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p><p> En su perfil, establezca la variable de entorno ASPNETCORE_ENVIRONMENT en "Desarrollo". Esto es utilizado por WebApp y WorkflowApp. </p><h2> Compilar y ejecutar ClientApp </h2><p> Ejecutar: </p>
<ul>
<li> CD Frontend / ClientApp </li>
<li> npm install </li>
<li> npm start </li>
</ul><p> Vaya a localhost: 4200 en su navegador. La aplicación del cliente se cargará. Algunas características no funcionarán hasta que se ejecute la aplicación web. </p><h2> Cree y ejecute WebApp con ClientApp </h2><p> Ejecutar: </p>
<ul>
<li> (hacer arriba: "Compilar e iniciar ClientApp") </li>
<li> cd ../../Backend/WebApp </li>
<li> dotnet build webapp.csproj </li>
<li> dotnet run bin / debug / dotnet2.2 / webapp.dll </li>
</ul><p> Vaya a localhost: 5000 en su navegador. La aplicación del cliente se cargará. </p><h2> Compilar y ejecutar ClientApp independiente </h2>
<ul>
<li> En app.module.ts, cambie "isAspServerRunning" de verdadero a falso. </li>
<li> (hacer arriba: "Compilar e iniciar ClientApp") </li>
</ul><h2> Compilar y ejecutar WorkflowApp </h2><p> Ejecutar: </p>
<ul>
<li> cd Backend / WorkflowApp </li>
<li> dotnet build workflowapp.csproj </li>
<li> dotnet run bin / debug / dotnet2.0 / workflowapp.dll </li>
</ul><p> Nota: Consulte las notas para WorkflowApp en "Código de Visual Studio" </p><!-- END OF README SECTION --><hr /><p><a name="Database"></a></p><h1> Base de datos <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p><p> Es posible que no necesite instalar y configurar la base de datos para realizar el desarrollo. Hay comprobantes de prueba que sustituyen a la base de datos de llamadas. Consulte "Comprobantes de prueba" a continuación. </p><h2> Instalar proveedor </h2><p> Si está utilizando Visual Studio o Visual Studio Code, el proveedor Sql Server Express LocalDb ya está instalado. De lo contrario, realice la "Instalación del proveedor LocalDb" a continuación. </p><h3> Instalación del proveedor LocalDb </h3><p> Vaya a <a href="https://www.microsoft.com/en-us/sql-server/sql-server-downloads">SQL Server Express.</a> Para Windows, descargue la edición especializada "Express" de SQL Server. Durante la instalación, elija "Personalizado" y seleccione "LocalDb". </p><p> LocalDb está disponible también para MacOs y Linux. Si lo instala para cualquiera de las plataformas, actualice este documento con los pasos y haga una Solicitud de extracción. </p><h3> Otros proveedores </h3><p> Además de LocalSb, EF Core es compatible con <a href= "https://docs.microsoft.com/en-us/ef/core/providers/?tabs=dotnet-core-cli">otros proveedores,</a> que puede utilizar para el desarrollo, incluido SqlLite. Deberá modificar la configuración de DbContext en startup.cs y la cadena de conexión en appsettings.json. </p><h2> Construir esquema de base de datos </h2><p> La base de datos se crea a través de la función "codificar primero" de Entity Framework Core. Examina las clases de C
# en el modelo de datos y crea automáticamente todas las tablas y relaciones. Hay dos pasos: (1) Crear el código de "migraciones" para realizar la actualización y (2) ejecutar la actualización. </p>
<ul>
<li> cd Backend / WebApp </li>
<li> dotnet ef migrations add initial --project .. \ Database \ DatabaseAccess_Lib </li>
<li> actualización de la base de datos de dotnet ef --project .. \ Database \ DatabaseAccess_Lib </li>
</ul><h2> Explore la base de datos creada </h2><h3> En VsCode </h3><p> Agregue lo siguiente a su configuración de usuario.json en VsCode: </p><pre> <code> "mssql.connections": [ { "server": "(localdb)\\mssqllocaldb", "database": "Govmeeting", "authenticationType": "Integrated", "profileName": "GMProfile", "password": "" } ],</code> </pre>
<ul>
<li> Presione ctrl-alt-D o presione el icono del servidor SQL en el margen izquierdo. </li>
<li> Abra la conexión de GMProfile para ver y trabajar con objetos de base de datos. </li>
<li> Abrir "Tablas". Debería ver todas las tablas creadas cuando creó el esquema anterior. Esto incluye las tablas AspNetxxxx para autorizaciones y las tablas para el modelo de datos Govmeeting. </li>
</ul><h3> En Visual Studio </h3>
<ul>
<li> Vaya al elemento del menú: Ver -&gt; Explorador de objetos de SQL Server. </li>
<li> Expanda SQL Server -&gt; (localdb) \ MSSQLLocalDb -&gt; Bases de datos -&gt; Govmeeting </li>
<li> Abrir "Tablas". Debería ver todas las tablas creadas cuando creó el esquema anterior. Esto incluye las tablas AspNetxxxx para autorizaciones y las tablas para el modelo de datos Govmeeting. </li>
</ul><h3> Otras plataformas </h3><p> Existe el <a href="https://github.com/Microsoft/sqlopsstudio?WT.mc_id=-blog-scottha">SQL Operations Studio</a> multiplataforma y de código abierto <a href="https://github.com/Microsoft/sqlopsstudio?WT.mc_id=-blog-scottha">,</a> "una herramienta de administración de datos que permite trabajar con SQL Server, Azure SQL DB y SQL DW desde Windows, macOS y Linux". Puede descargar <a href="https://docs.microsoft.com/en-us/sql/sql-operations-studio/download?view=sql-server-2017&WT.mc_id=-blog-scottha">SQL Operations Studio gratis aquí.</a> </p><p> Si utiliza esta u otra herramienta para explorar las bases de datos de SQL Server, actualice estas instrucciones. </p><h2> Trozos de prueba </h2><p> El código para almacenar / recuperar datos de transcripción en la base de datos aún no está escrito. Por lo tanto, DatabaseRepositories_Lib utiliza datos de prueba estáticos en su lugar. En WebApp / appsettings.json, la propiedad "UseDatabaseStubs" se establece en "true", diciéndole que llame a las rutinas de código auxiliar. </p><p> Sin embargo, el registro de usuario y el código de inicio de sesión en la aplicación web utilizan la base de datos. Accede a las tablas de autenticación de usuario de Asp.Net. WebApp autentica las llamadas API de ClientApp en función del usuario conectado actualmente. </p><p> Puede usar el valor del preprocesador "NOAUTH" en la aplicación web para omitir la autenticación. Use uno de estos métodos: </p>
<ul>
<li> En FixasrController.cs o AddtagsController.cs, descomente la línea "#if NOAUTH" en la parte superior del archivo. </li>
<li> Para deshabilitarlo para todos los controladores, agregue esto a WebApp.csproj: </li>
</ul><pre> <code> &lt;PropertyGroup Condition="&#39;$(Configuration)|$(Platform)&#39;==&#39;Debug|AnyCPU&#39;&gt; &lt;DefineConstants&gt;NOAUTH&lt;/DefineConstants&gt; &lt;/PropertyGroup&gt;</code> </pre>
<ul>
<li> En Visual Studio, vaya a las páginas de propiedades de la aplicación web -&gt; Crear -&gt; e ingrese NOAUTH en el cuadro "Símbolos de compilación condicional". </li>
</ul><hr /><p><a name="GoogleCloud"></a></p><h1> Cuenta de Google Cloud Platform <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p><p> Para usar las API de Google Speech para la conversión de voz a texto, necesita una cuenta de Google Cloud Platform (GCP). Para la mayoría del trabajo de desarrollo en Govmeeting, puede usar los datos de prueba existentes. Pero si desea transcribir nuevas grabaciones, tendrá un GCP. La API de Google puede transcribir grabaciones en más de 120 idiomas y variantes. </p><p> Google proporciona a los desarrolladores una cuenta gratuita que incluye un crédito (actualmente $ 300). El costo actual de usar la API de voz es gratuito por hasta 60 minutos de conversión por mes. Después de eso, el costo del "modelo mejorado" (que es lo que necesitamos) es de $ 0.009 por 15 segundos. ($ 2.16 por hora) </p>
<ul>
<li><p> Abra una cuenta con <a href="https://cloud.google.com/free/">Google Cloud Platform</a> </p></li>
<li><p> Vaya al <a href="http://console.cloud.google.com">Panel de Google Cloud</a> y cree un proyecto. </p></li>
<li><p> Vaya a la <a href="http://console.developers.google.com">Consola de desarrollador de Google</a> y habilite las API de almacenamiento de voz y nube </p></li>
<li><p> Genere una credencial de "cuenta de servicio" para este proyecto. Haga clic en Credenciales en la consola del desarrollador. </p></li>
<li><p> Otorgue a esta cuenta permisos de "Editor" en el proyecto. Haz clic en la cuenta. En la página siguiente, haga clic en Permisos. </p></li>
<li><p> Descargue el archivo JSON de credencial. </p></li>
<li><p> Coloque el archivo en la carpeta <code>_SECRETS</code> que creó cuando clonó el repositorio. </p></li>
<li><p> Cambie el nombre del archivo <code>TranscribeAudio.json</code> . </p></li>
</ul><p> NOTA: Los pasos anteriores pueden haber cambiado ligeramente. Si es así, actualice este documento. </p><h2> Prueba de transcripción de voz a texto </h2>
<ul>
<li><p> Establezca el proyecto de inicio en Visual Studio en <code>Backend/WorkflowApp</code> . Presione F5. </p></li>
<li><p> Copie (no mueva) uno de los archivos MP4 de muestra de testdata a Datafiles / RECEIVED. </p></li>
</ul><p> El programa ahora reconocerá que ha aparecido un nuevo archivo y comenzará a procesarlo. El archivo MP4 se moverá a "COMPLETADO" cuando termine. Verá los resultados en sufolders, que se crearon en el directorio "Datafiles". </p><p> En appsettings.json, hay una propiedad "RecordingSizeForDevelopment". Actualmente está configurado en "180". Esto hace que la rutina de transcripción en ProcessRecording_Lib procese solo los primeros 180 segundos de la grabación. </p><hr /><p><a name="GoogleApi"></a></p><h1> Google API Keys <br/></h1><p> <a href="about?id=setup#Contents">[Contenido]</a> </p><p> Necesitará estas claves si desea usar o trabajar en ciertas funciones del proceso de registro e inicio de sesión. </p>
<ul>
<li> Las claves ReCaptcha son necesarias para usar ReCaptcha durante el registro del usuario. Se pueden obtener en <a href="https://developers.google.com/recaptcha/">Google reCaptcha</a> . </li>
<li> Las credenciales de OAuth 2.0 se utilizan para iniciar sesión en el usuario externo sin necesidad de que el usuario cree una cuenta personal en el sitio. Visite la <a href="https://console.developers.google.com/">Consola API de Google</a> para obtener credenciales, como un ID de cliente y un secreto de cliente. </li>
</ul><p> Cree un archivo llamado "appsettings.Development.json" en la carpeta "_SECRETS". Debe contener las claves que acaba de obtener: </p><pre> <code>{ "ExternalAuth": { "Google": { "ClientId": "your-client-Id", "ClientSecret": "your-client-secret" } }, "ReCaptcha:SiteKey": "your-site-key", "ReCaptcha:Secret": "your-secret" }</code> </pre><hr /><h2> Prueba reCaptcha </h2>
<ul>
<li> Ejecute el proyecto de aplicación web. </li>
<li> Haga clic en "Registrarse" en la esquina superior derecha. </li>
<li> La opción reCaptcha debería aparecer. </li>
</ul><hr /><h2> Probar la autenticación de Google </h2>
<ul>
<li> Ejecute el proyecto de aplicación web. </li>
<li> Haga clic en "Iniciar sesión" en la esquina superior derecha. </li>
<li> En "Usar otro servicio para iniciar sesión", elija "Google". </li>
</ul>