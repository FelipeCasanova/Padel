namespace Padel.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum SexoEnum : byte { Sin_Definir = 0, Hombre = 1, Mujer = 2 }

    public enum TipoEquipoEnum : byte { Sin_Definir = 0, Hombre = 1, Mujer = 2, Mixto = 3 }

    public enum EstadoUsuarioTorneEnum : byte { Sin_Definir = 0, Pendiente = 1, Pagado = 2 }

    public enum EstadoTorneoEnum : byte { Sin_Definir = 0, Pendiente = 1, Progreso = 2, Finalizado = 3 }

}
