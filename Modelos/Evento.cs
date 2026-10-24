using System;

namespace PLANAZO.Modelos
{
    public abstract class Evento
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public DateTime Fecha { get; set; }
        public decimal PrecioBase { get; set; }
        public int CapacidadMaxima { get; set; }
        public int CuposDisponibles { get; private set; }

        protected Evento(string id, string nombre, DateTime fecha, decimal precioBase, int capacidadMaxima)
        {
            Id = id;
            Nombre = nombre;
            Fecha = fecha;
            PrecioBase = precioBase;
            CapacidadMaxima = capacidadMaxima;
            CuposDisponibles = capacidadMaxima;
        }

        public abstract decimal CalcularPrecioFinal();

        public void ReducirCupos(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a cero.");
            if (cantidad > CuposDisponibles)
                throw new InvalidOperationException("No hay suficientes cupos disponibles.");

            CuposDisponibles -= cantidad;
        }

        public void AumentarCupos(int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a devolver debe ser mayor a cero.");

            CuposDisponibles += cantidad;
            if (CuposDisponibles > CapacidadMaxima)
                CuposDisponibles = CapacidadMaxima;
        }

        public override string ToString()
        {
            return $"[{Id}] {Nombre,-30} | Fecha: {Fecha:dd/MM/yyyy HH:mm} | Tipo: {GetType().Name,-10} | " +
                   $"Precio Final: {CalcularPrecioFinal():C} | Cupos: {CuposDisponibles}/{CapacidadMaxima}";
        }
    }
}
