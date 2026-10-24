using System;

namespace PLANAZO.Modelos
{
    public class Concierto : Evento
    {
        public Concierto(string id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase * 1.15m;
        }
    }
}
