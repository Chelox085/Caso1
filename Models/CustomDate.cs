using System.Globalization;

namespace Caso1.Models
{
    /// <summary>
    /// Tipo personalizado para demostrar model binder.
    /// </summary>
    public class CustomDate
    {
        /// <summary>
        /// Valor fecha interpretado.
        /// </summary>
        public DateTime Value { get; set; }

        /// <summary>
        /// Texto original recibido.
        /// </summary>
        public string Raw { get; set; } = string.Empty;

        /// <summary>
        /// Convierte el valor en cadena con formato ISO.
        /// </summary>
        public override string ToString()
        {
            return Value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture);
        }
    }
}
