using System;

namespace PLANAZO.Modelos
{
    public class Taller : Evento
    {
        public Taller(string id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase * 0.90m;
        }
    }
}
