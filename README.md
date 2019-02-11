# Polideportivo
Polideportivo Api with NetCore

Para arrancar el proyecto:
  1) Descargar carpeta NetCore_Polideportivo en el equipo.
  2) Ejecutar la solucion del proyecto (.sln) con Visual Studio.
  3) Descargar la carpeta Base de Datos del repositorio, contiene el script para montar la estructura de la BD y los datos de las tablas.
  4) Con el Sql Server Management Studio, importar la estructura y los datos anteriores para montar la Base de Datos.
  5) Copiamos el nombre del servidor que nos aparace en sql server managament studio, o podemos mirar el nombre del servidor abriendo la
      ventana de sql explorer en Visual Studio, y lo modificamos en el siguiente archivo:
       En la carpeta del proyecto Models en el archivo ApplicationDbContext, en la función 'OnConfiguring' en la línea 28,
       sustituimos el nombre por vuestro nombre de servidor y si habeis llamado a la BD de otra forma sustituimos el Polideportivo_test
       por el nombre nuevo.  
  6) Una vez hechos los pasos anteriores, le damos a Ejecutar en el Visual Studio y abrirá una documentación en Swagger donde podremos
      testear las funciones definididas para la API de este proyecto.


Para los test unitarios:
  1) La solución esta divida en dos proyectos, NetCore y NetCore.Units.Tests. Para los test nos centraremos en este último.
  2) Activamos en el Visual Studio, en el menú superior, Prueba -> ventanas -> Explorador de pruebas.
  3) Aparece un panel nuevo, boton derecho para ejecutar todos los test o elegir los test a ejecutar.
  
