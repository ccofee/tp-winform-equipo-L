# TPWinForm_Equipo-L

Aplicación de escritorio para la gestión de un catálogo de artículos, desarrollada como trabajo
práctico de **Programación III** (TUP 2026 2C, comisión 121, Equipo L).

Permite administrar artículos con sus imágenes, y las marcas y categorías disponibles, con toda la
información persistida en SQL Server.

## Integrantes

| Integrante | GitHub | Responsabilidad |
|---|---|---|
| Enzo Sander | `enzosanderr` | Pantalla principal, listado, búsqueda, detalle y acceso a datos |
| Agustín Trejo | `agustintrejo52-ux` | Alta, modificación y baja de artículos, y galería de imágenes |
| Agustín Parada | `ccofee` | ABM de marcas y categorías, validaciones, manejo de errores y documentación |

## Requisitos

- **Visual Studio 2022** con la carga de trabajo de escritorio de .NET.
- **.NET Framework 4.8** (viene con Visual Studio 2022).
- **SQL Server Express** y **SQL Server Management Studio (SSMS)**. Probado con SQL Server 2022
  Express.

## Instalación

### 1. Restaurar la base de datos

El script está en [`db/CATALOGO_DB_v3.sql`](db/CATALOGO_DB_v3.sql).

1. Abrir SSMS y conectarse a la instancia local de SQL Server.
2. Abrir el script y ejecutarlo completo (`F5`).
3. Crea la base **`CATALOGO_P3_DB`** con las tablas `ARTICULOS`, `MARCAS`, `CATEGORIAS` e
   `IMAGENES`, y carga datos de prueba.

Para verificar que quedó bien:

```sql
USE CATALOGO_P3_DB;
SELECT COUNT(*) FROM ARTICULOS;   -- 5
SELECT COUNT(*) FROM IMAGENES;    -- 6
```

### 2. Configurar la cadena de conexión

Está en `TPWinForm_Equipo-L/Presentacion/App.config`, bajo la clave `cadena-conexion`:

```xml
<add key="cadena-conexion" value="server=.\SQLEXPRESS; database=CATALOGO_P3_DB; integrated security=true" />
```

`server=.\SQLEXPRESS` significa "la instancia SQLEXPRESS de esta computadora", e
`integrated security=true` que entra con el usuario de Windows, sin usuario ni contraseña de SQL.

**Si tu instancia se llama distinto**, hay que cambiar el `server=`. La regla es simple: lo que
escribís en *Server name* al conectarte con SSMS es exactamente lo que va ahí.

| Instalación | Valor de `server=` |
|---|---|
| SQL Server Express con el nombre por defecto | `.\SQLEXPRESS` |
| Instancia predeterminada (Developer, Standard) | `.` o `localhost` |
| SQL Server LocalDB | `(localdb)\MSSQLLocalDB` |
| Instancia con nombre propio | `.\NOMBRE` |

Para ver qué instancias tenés instaladas y si están corriendo, desde PowerShell:

```powershell
(Get-ItemProperty 'HKLM:\SOFTWARE\Microsoft\Microsoft SQL Server\Instance Names\SQL').PSObject.Properties |
  Where-Object { $_.Name -notlike 'PS*' } | ForEach-Object { $_.Name }
Get-Service | Where-Object { $_.Name -like 'MSSQL*' } | Select-Object Name, Status
```

Advertencia:

- Después de editar `App.config` hay que **recompilar**. En ejecución la aplicación no lee ese
  archivo, sino la copia que Visual Studio genera en `bin\Debug\Presentacion.exe.config`.

### 3. Ejecutar la aplicación

1. Abrir `TPWinForm_Equipo-L/TPWinForm_Equipo-L.slnx` con Visual Studio 2022.
2. Verificar que **Presentacion** sea el proyecto de inicio.
3. Compilar y ejecutar con `F5`.

## Funcionalidades

| Funcionalidad | Dónde está |
|---|---|
| Listado de artículos | Pantalla principal |
| Búsqueda por código, nombre o descripción | Filtro rápido de la pantalla principal |
| Filtro por marca, categoría y rango de precio | Búsqueda avanzada de la pantalla principal |
| Ver el detalle de un artículo con sus imágenes | Botón *Ver Detalle* |
| Agregar, modificar y eliminar artículos | Botones *Agregar*, *Modificar* y *Eliminar*, y menú *Artículos* |
| Varias imágenes por artículo, sin límite | Ventana de imágenes, desde el alta o la modificación |
| Administrar marcas y categorías | Menú *Marcas* y *Categorías* |

## Estructura de la solución

La solución tiene tres proyectos, uno por capa:

| Proyecto | Contenido |
|---|---|
| `Dominio` | Las clases del modelo: `Articulo`, `Marca`, `Categoria` e `Imagen`. |
| `Negocio` | `AccesoDatos` (conexión y consultas con ADO.NET), las clases `ArticuloNegocio`, `MarcaNegocio`, `CategoriaNegocio` e `ImagenNegocio`, y `Validacion` con los helpers de validación. |
| `Presentacion` | Los formularios de Windows Forms y `ManejoErrores`, que unifica los mensajes de error al usuario. |

El acceso a datos es **ADO.NET puro** (`SqlConnection`, `SqlCommand`, `SqlDataReader`), sin ORM, y
todas las consultas usan parámetros.

## Pruebas

La planilla de casos de prueba manuales está en
[`db/casos-de-prueba.md`](db/casos-de-prueba.md).
