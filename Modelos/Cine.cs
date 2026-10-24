using System;

namespace PLANAZO.Modelos
{
    public class Cine : Evento
    {
        public Cine(string id, string nombre, DateTime fecha, decimal precioBase, int capacidad)
            : base(id, nombre, fecha, precioBase, capacidad) { }

        public override decimal CalcularPrecioFinal()
        {
            return PrecioBase;
        }
    }
}
