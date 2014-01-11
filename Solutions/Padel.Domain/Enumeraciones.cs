namespace Padel.Domain
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public enum SexoEnum : byte { Sin_Definir = 0, Hombre = 1, Mujer = 2 }

    public enum TipoEquipoEnum : byte { Sin_Definir = 0, Hombre = 1, Mujer = 2, Mixto = 3 }

    public enum TipoTorneoEnum : byte { Estandar = 0, Especial = 1, Mini = 2, Privado = 3 }

    public enum EstadoCategoriaEnum : byte { Pendiente = 0, Progreso = 2, Finalizado = 3 }

}
