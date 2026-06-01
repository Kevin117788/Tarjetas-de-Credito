using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Tarjetas_de_Credito
{
    internal class Conexion
    {
        public static string ObtenerCadena()
        {
            string ruta = @"C:\conexion\Criptografia.txt";
            if (File.Exists(ruta))
            {
                // Leer todo el contenido y quitar espacios en blanco de los extremos
                return File.ReadAllText(ruta).Trim();
            }
            else
            {
                // Devolvemos el predeterminado si el txt no existe (como respaldo temporal)
                return Properties.Settings.Default.TarjetaDeCreditoConnectionString;
            }
        }
    }
}
