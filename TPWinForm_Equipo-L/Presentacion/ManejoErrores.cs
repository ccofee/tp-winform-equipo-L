using System;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Forms;

namespace Presentacion
{
    // Punto único para avisarle al usuario de un error inesperado sin mostrarle el detalle técnico.
    // El detalle completo queda en la ventana de salida de Visual Studio para quien esté depurando.
    public static class ManejoErrores
    {
        // accion completa la frase "No se pudo ...", por ejemplo "cargar las marcas".
        public static void Mostrar(Exception ex, string accion)
        {
            Debug.WriteLine(ex);

            string mensaje;

            if (ex is SqlException)
                mensaje = "No se pudo " + accion + " por un problema con la base de datos. " +
                          "Verificá que SQL Server esté funcionando e intentá de nuevo.";
            else
                mensaje = "No se pudo " + accion + ". Intentá de nuevo y, si el problema sigue, " +
                          "avisale al equipo.";

            MessageBox.Show(mensaje, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
