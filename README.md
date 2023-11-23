# ci_dotnet
## Demo de CI/CD Github actions con solución .NET 7

La solución contiene 5 directorios principales para la aplicación:
1. database: Directorio donde se colocan los archivos de migración de base de datos
con el formato del dotnet tool ["Evolve"](https://evolve-db.netlify.app/) 
2. core: Librería de lógica de negocio y clases base en dotnet 
3. api: Proyecto de API Rest utilizando ["Minimal API ASP .NET Core"](https://learn.microsoft.com/en-us/aspnet/core/tutorials/min-web-api?view=aspnetcore-8.0&tabs=visual-studio) y patron CQRS
4. spabff: Proyecto de frontend implementando el patron BFF (Backend for frontend). 
Es un proyecto ASP .NET Core que contiene un proyecto Next js exportado como archivos estaticos en la directorio wwwroot y que implementa un reverse proxy al API, utilizando ["YARP reverse proxy"](https://microsoft.github.io/reverse-proxy/)
5. test: Proyecto de test unitarios del proyecto "core" utilizando XUnit y Coverlet para medir la cobertura

## Implementar en local:
* Tener instalado [".NET 7"](https://dotnet.microsoft.com/en-us/download/dotnet/7.0)
* Implementar un servidor de base de datos local con SQL Server Developer Edition. Se puede implementar usando docker engine en el ["WSL 2"](https://learn.microsoft.com/en-us/windows/wsl/install) o en ["Podman"](https://podman.io/)
* Con el CLI de preferencia y en el directorio del proyecto "3. api" ejecutar el comando: ```dotnet user-secrets init```
* Setear el secreto de la conexión a base de datos: ```dotnet user-secrets set 'DemoDbConnection' '${valor}'```
* Setear el secreto de ubicación del directorio "1. base de datos" en el sistema de archivos: ```dotnet user-secrets set 'DatabaseLocation' '${valor}'```
* Se puede ejecutar en Visual Studio o VS Code (ya existen las configuraciones de lanzamiento)

## Para la emular los ambientes:
* Implementar una máquina virtual de Windows Server 2019+, por ejemplo con ["Virtual Box"](https://medium.com/@brianmwambia3/a-step-by-step-guide-setting-up-windows-server-2019-on-oracle-virtualbox-1a7b39090589)
* En la máquina virtual, implementar un servidor de ["SQL Server Developer Edition 2019+"](https://www.sqlservertutorial.net/install-sql-server/)
* En la máquina virtual, habilitar ["IIS 10"](https://www.rootusers.com/how-to-install-iis-in-windows-server-2019/)  

