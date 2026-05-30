using System;

namespace Tarjetas_de_Credito
{
    public static class SesionGlobal
    {
        public static string UsuarioActual { get; set; } = string.Empty;
        public static string ContrasenaActual { get; set; } = string.Empty;
        public static string RolActual { get; set; } = string.Empty;
    }
}
