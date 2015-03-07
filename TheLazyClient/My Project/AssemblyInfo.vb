Imports System
Imports System.Reflection
Imports System.Runtime.InteropServices
Imports System.Globalization
Imports System.Resources
Imports System.Windows

' La información general sobre un ensamblado se controla mediante el siguiente 
' conjunto de atributos. Cambie estos atributos para modificar la información
' asociada con un ensamblado.

' Revisar los valores de los atributos del ensamblado

<Assembly: AssemblyTitle("TheLazyClient")> 
<Assembly: AssemblyDescription("")> 
<Assembly: AssemblyCompany("")> 
<Assembly: AssemblyProduct("TheLazyClient")> 
<Assembly: AssemblyCopyright("Copyright ©  2015")> 
<Assembly: AssemblyTrademark("")> 
<Assembly: ComVisible(false)>

'Para comenzar a compilar aplicaciones que se puedan traducir, establezca 
'<UICulture>CultureYouAreCodingWith</UICulture> en el archivo .vbproj
'dentro de <PropertyGroup>. Por ejemplo, si utiliza inglés de EE.UU. en 
'sus archivos de código fuente, establezca <UICulture> en "en-US".  Después, quite los comentarios del
'atributo NeutralResourceLanguage incluido a continuación.  Actualice "en-US" en la línea
'siguiente de forma que coincida con el valor de UICulture en el archivo del proyecto.

'<Assembly: NeutralResourcesLanguage("en-US", UltimateResourceFallbackLocation.Satellite)> 


'El atributo ThemeInfo describe dónde se encuentran los diccionarios de recursos genéricos y específicos de un tema.
'Primer parámetro: lugar en el que se encuentran los diccionarios de recursos específicos de un tema
'(se utiliza si no se encuentra ningún recurso en la página, 
' ni diccionarios de recursos de la aplicación)

'Segundo parámetro: lugar en el que se encuentra el diccionario de recursos genérico
'(se utiliza si no se encuentra ningún recurso en la página, 
'aplicación ni diccionarios de recursos específicos de un tema)
<Assembly: ThemeInfo(ResourceDictionaryLocation.None, ResourceDictionaryLocation.SourceAssembly)>



'El siguiente GUID sirve como identificador de typelib si este proyecto se expone a COM
<Assembly: Guid("e4762034-63c3-4346-b11d-0773224b177a")> 

' La información de versión de un ensamblado consta de los cuatro valores siguientes:
'
'      Versión principal
'      Versión secundaria 
'      Número de compilación
'      Revisión
'
' Puede especificar todos los valores o usar los valores predeterminados de número de compilación y de revisión 
' mediante el asterisco ('*'), como se muestra a continuación:
' <Assembly: AssemblyVersion("1.0.*")> 

<Assembly: AssemblyVersion("1.0.0.0")> 
<Assembly: AssemblyFileVersion("1.0.0.0")> 
