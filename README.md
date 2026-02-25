# MiniGestorTareas

MiniGestorTareas es una aplicación web desarrollada en ASP.NET Core MVC que permite gestionar tareas mediante un CRUD completo con persistencia en SQLite utilizando Entity Framework Core.

El proyecto implementa filtrado dinámico, ordenación, paginación simple y validaciones siguiendo el patrón de arquitectura MVC.

Cómo ejecutar el proyecto desde cero
1️.Clonar el repositorio
git clone https://github.com/AlvaroGarrido10/MiniGestorTareas.git
cd MiniGestorTareas
2️. Abrir el proyecto en Visual Studio

Abrir el archivo .sln del proyecto.

3️. Crear la base de datos desde Visual Studio

Ir a:

Tools → NuGet Package Manager → Package Manager Console

Ejecutar:

Update-Database

Esto aplicará las migraciones y generará automáticamente el archivo MiniGestorTareas.db.

Decisiones tomadas

La fecha de creación (CreatedAt) no es editable desde la vista de edición. Se asigna automáticamente al crear la tarea para garantizar integridad de los datos.

La base de datos SQLite (.db) no se versiona en el repositorio. Solo se incluyen las migraciones, de forma que la base de datos se genera localmente mediante Update-Database.

Problemas encontrados

Comprender el funcionamiento de las migraciones y cómo se genera la base de datos a partir del modelo.

Entender por qué SQLite genera archivos auxiliares (.db-wal, .db-shm) mientras la aplicación está en ejecución.