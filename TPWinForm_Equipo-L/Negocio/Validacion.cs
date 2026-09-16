using System;
using System.Globalization;

namespace Negocio
{
    // Helpers de validación compartidos por todos los formularios.
    // Solo responden si el dato es válido: el mensaje al usuario lo arma cada formulario.
    public static class Validacion
    {
        // true si el texto tiene algo más que espacios.
        public static bool TieneTexto(string texto)
        {
            return !string.IsNullOrWhiteSpace(texto);
        }

        // true si el texto es un número decimal sin signo, con el separador decimal de la
        // configuración regional de la máquina. No acepta separador de miles: en una máquina
        // donde la coma es de miles, "69,999" se rechaza en vez de leerse como 69999.
        public static bool EsPrecio(string texto)
        {
            decimal valor;
            NumberStyles estilo = NumberStyles.AllowDecimalPoint
                                | NumberStyles.AllowLeadingWhite
                                | NumberStyles.AllowTrailingWhite;

            return decimal.TryParse(texto, estilo, CultureInfo.CurrentCulture, out valor);
        }

        // true si el texto es una URL absoluta que empieza con http o https.
        public static bool EsUrl(string texto)
        {
            Uri uri;

            if (!Uri.TryCreate(texto, UriKind.Absolute, out uri))
                return false;

            return uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps;
        }
    }
}
