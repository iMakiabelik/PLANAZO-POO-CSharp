using System;
using System.Collections.Generic;
using System.Linq;
using PLANAZO.Modelos;

namespace PLANAZO.Servicios
{
    public class GestorReservas
    {
        private readonly List<Evento> _eventos;
        private readonly List<Reserva> _reservas;
        private int _contadorReserva;

        public GestorReservas()
        {
            _eventos = new List<Evento>();
            _reservas = new List<Reserva>();
            _contadorReserva = 1;
            CargarEventosIniciales();
        }

        private void CargarEventosIniciales()
        {
            // 2 Cine
            _eventos.Add(new Cine("CIN01", "Avatar: El Camino del Agua", DateTime.Now.AddDays(2), 15000m, 50));
            _eventos.Add(new Cine("CIN02", "Oppenheimer", DateTime.Now.AddDays(4), 18000m, 40));

            // 2 Conciertos
            _eventos.Add(new Concierto("CON01", "Silvestre Dangond en Vivo", DateTime.Now.AddDays(10), 80000m, 100));
            _eventos.Add(new Concierto("CON02", "Bad Bunny World Tour", DateTime.Now.AddDays(15), 150000m, 200));

            // 2 Talleres
            _eventos.Add(new Taller("TAL01", "Taller C# y .NET Avanzado", DateTime.Now.AddDays(5), 50000m, 25));
            _eventos.Add(new Taller("TAL02", "Masterclass de Fotografía", DateTime.Now.AddDays(8), 40000m, 20));
        }

        public List<Evento> ObtenerEventos() => _eventos;

        public Evento ConsultarEvento(string id)
        {
            var evento = _eventos.FirstOrDefault(e => e.Id.Equals(id, StringComparison.OrdinalIgnoreCase));
            if (evento == null)
                throw new KeyNotFoundException($"No se encontró ningún evento con el ID '{id}'.");
            return evento;
        }

        public Reserva RealizarReserva(string idEvento, string nombrePersona, int cantidad)
        {
            if (string.IsNullOrWhiteSpace(nombrePersona))
                throw new ArgumentException("El nombre de la persona no puede estar vacío.");

            if (cantidad <= 0)
                throw new ArgumentException("La cantidad de entradas debe ser mayor a cero.");

            var evento = ConsultarEvento(idEvento);

            if (cantidad > evento.CuposDisponibles)
                throw new InvalidOperationException($"No hay suficientes cupos disponibles. Cupos actuales: {evento.CuposDisponibles}.");

            evento.ReducirCupos(cantidad);

            string idReserva = $"RES-{_contadorReserva++:D3}";
            var nuevaReserva = new Reserva(idReserva, nombrePersona, evento, cantidad);
            _reservas.Add(nuevaReserva);

            return nuevaReserva;
        }

        public List<Reserva> ObtenerReservas() => _reservas;

        public void CancelarReserva(string idReserva)
        {
            var reserva = _reservas.FirstOrDefault(r => r.IdReserva.Equals(idReserva, StringComparison.OrdinalIgnoreCase));
            if (reserva == null)
                throw new KeyNotFoundException($"No existe ninguna reserva con el código '{idReserva}'.");

            reserva.Cancelar();
        }

        public (int Activas, int EntradasReservadas, decimal ValorTotal) ObtenerResumen()
        {
            var reservasActivas = _reservas.Where(r => r.Estado == EstadoReserva.Activa).ToList();
            int totalActivas = reservasActivas.Count;
            int totalEntradas = reservasActivas.Sum(r => r.CantidadEntradas);
            decimal totalValor = reservasActivas.Sum(r => r.ValorTotal);

            return (totalActivas, totalEntradas, totalValor);
        }
    }
}
