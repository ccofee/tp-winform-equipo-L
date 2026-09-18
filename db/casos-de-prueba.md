# Casos de prueba manuales

Planilla para verificar la aplicación antes de la entrega.

**Antes de empezar:** ejecutar [`CATALOGO_DB_v3.sql`](CATALOGO_DB_v3.sql) para dejar la base en su
estado inicial (5 marcas, 4 categorías, 5 artículos y 6 imágenes). Los casos usan esos datos, así
que conviene volver a correr el script si ya se hicieron pruebas.

Completar **Resultado** con OK o Falla, y usar **Observaciones** para anotar qué pasó cuando falla.

| Probado por | Fecha | Versión (commit) |
|---|---|---|
|  |  |  |

## Listado, búsqueda y detalle

| # | Caso | Pasos | Resultado esperado | Resultado | Observaciones |
|---|---|---|---|---|---|
| 1 | Listado inicial | Abrir la aplicación | La grilla muestra los 5 artículos |  |  |
| 2 | Búsqueda por código | Escribir `S01` en el filtro rápido | Queda solo el Galaxy S10 |  |  |
| 3 | Búsqueda por nombre | Escribir `play` | Quedan Moto G Play y Play 4 |  |  |
| 4 | Búsqueda por descripción | Escribir `tele` | Queda el Bravia 55 ("Alta tele") |  |  |
| 5 | Filtro por marca | Elegir marca Sony y buscar | Quedan Play 4 y Bravia 55 |  |  |
| 6 | Filtro por rango de precio | Precio mínimo 30000, máximo 50000 | Quedan Play 4 y Bravia 55 |  |  |
| 7 | Limpiar filtro | Tocar Limpiar | Vuelven los 5 artículos |  |  |
| 8 | Ver detalle | Seleccionar Moto G Play y tocar Ver Detalle | Se abre el detalle con sus datos y su imagen |  |  |
| 9 | Carrusel de imágenes | En el detalle del Moto G Play, tocar siguiente y anterior | Alterna entre sus 2 imágenes |  |  |
| 10 | Imagen rota | Ver el detalle de un artículo con una URL que no carga | Muestra la imagen por defecto, sin error |  |  |

## Alta, modificación y baja de artículos

| # | Caso | Pasos | Resultado esperado | Resultado | Observaciones |
|---|---|---|---|---|---|
| 11 | Campos obligatorios | Tocar Aceptar con el formulario vacío | Avisa que falta el código, y así con cada campo |  |  |
| 12 | Precio inválido | Escribir `abc` en precio y aceptar | Avisa que el precio no es válido |  |  |
| 13 | Alta completa | Cargar código `T01`, nombre, descripción, marca, categoría y precio | Se guarda y avisa el Id asignado |  |  |
| 14 | Alta con 3 imágenes | En el alta anterior, cargar 3 URLs y aceptar | Las 3 quedan en la lista |  |  |
| 15 | **Persistencia** | Cerrar la aplicación, volver a abrirla y modificar `T01` | Siguen las 3 imágenes y todos los datos |  |  |
| 16 | Código repetido | Dar de alta otro artículo con código `T01` | Avisa que ya existe y no guarda |  |  |
| 17 | Modificar sin cambiar el código | Abrir `T01`, cambiar el precio y aceptar | Guarda sin avisar de código repetido |  |  |
| 18 | Combos precargados | Abrir un artículo en modificación | Marca y categoría vienen con las del artículo |  |  |
| 19 | Cancelar la baja | Seleccionar `T01`, Eliminar y responder No | No se borra |  |  |
| 20 | Baja confirmada | Repetir y responder Sí | Desaparece de la grilla |  |  |
| 21 | **Sin imágenes huérfanas** | Después de borrar `T01`, correr la consulta de abajo | Devuelve 0 filas |  |  |

```sql
-- Imágenes que quedaron apuntando a un artículo inexistente
SELECT * FROM IMAGENES WHERE IdArticulo NOT IN (SELECT Id FROM ARTICULOS);
```

## Imágenes

| # | Caso | Pasos | Resultado esperado | Resultado | Observaciones |
|---|---|---|---|---|---|
| 22 | URL vacía | Tocar Agregar con el campo vacío | Avisa que la URL tiene que empezar con http o https |  |  |
| 23 | URL inválida | Escribir `hola` y tocar Agregar | Mismo aviso, no se agrega nada a la lista |  |  |
| 24 | Previsualización | Pegar una URL de imagen válida | Se ve la imagen antes de agregarla |  |  |
| 25 | Navegación | Con 3 imágenes cargadas, usar anterior y siguiente | Recorre las 3 y vuelve a empezar |  |  |
| 26 | Quitar una imagen | Seleccionar una de la lista y tocar Eliminar | Sale de la lista |  |  |

## Marcas y categorías

| # | Caso | Pasos | Resultado esperado | Resultado | Observaciones |
|---|---|---|---|---|---|
| 27 | Listado de marcas | Abrir el menú Marcas | Se ven las 5 marcas |  |  |
| 28 | Descripción vacía | Tocar Agregar sin escribir nada | Avisa que falta la descripción |  |  |
| 29 | Alta de marca | Agregar la marca `Prueba` | Aparece en el listado |  |  |
| 30 | Modificar marca | Seleccionar `Prueba`, cambiarle el nombre y modificar | Se actualiza en el listado |  |  |
| 31 | Baja de marca sin uso | Eliminar la marca `Prueba` | Pide confirmación y la borra |  |  |
| 32 | **Marca en uso** | Intentar eliminar Samsung | Avisa que la usan 2 artículos y no la borra |  |  |
| 33 | **Categoría en uso** | Intentar eliminar Media | Avisa que la usan 2 artículos y no la borra |  |  |
| 34 | Categoría sin uso | Intentar eliminar Audio | Pide confirmación y la borra |  |  |
| 35 | Se refleja en el alta | Agregar una marca y abrir el alta de artículos | La marca nueva está en el combo |  |  |

## Manejo de errores

Detener el servicio **SQL Server (SQLEXPRESS)** desde `services.msc` antes de estos casos, y volver
a iniciarlo al terminar.

| # | Caso | Pasos | Resultado esperado | Resultado | Observaciones |
|---|---|---|---|---|---|
| 36 | Base caída al abrir | Abrir la aplicación | Mensaje entendible sobre la base de datos, sin excepción de .NET |  |  |
| 37 | Base caída en marcas | Abrir el menú Marcas | Mismo tipo de mensaje |  |  |
| 38 | Base caída al guardar | Intentar dar de alta un artículo | Avisa que no se pudo agregar y el formulario queda abierto |  |  |
| 39 | Base caída en imágenes | Aceptar en la ventana de imágenes | Avisa que no se pudieron guardar |  |  |

## Datos de la cátedra

| # | Caso | Pasos | Resultado esperado | Resultado | Observaciones |
|---|---|---|---|---|---|
| 40 | Artículo con categoría inexistente | Ver el Moto G Play en la grilla | Aparece igual, con la categoría vacía. El script lo carga apuntando a la categoría 5, que no existe |  |  |
