using System;

namespace PLANAZO.Modelos
{
    public enum EstadoReserva
    {
        Activa,
        Cancelada
    }

    public class Reserva
    {
        public string IdReserva { get; set; }
        public string NombrePersona { get; set; }
        public Evento EventoReservado { get; set; }
        public int CantidadEntradas { get; set; }
        public decimal ValorTotal { get; set; }
        public EstadoReserva Estado { get; set; }

        public Reserva(string idReserva, string nombrePersona, Evento evento, int cantidadEntradas)
        {
            IdReserva = idReserva;
            NombrePersona = nombrePersona;
            EventoReservado = evento;
            CantidadEntradas = cantidadEntradas;
            ValorTotal = evento.CalcularPrecioFinal() * cantidadEntradas;
            Estado = EstadoReserva.Activa;
        }

        public void Cancelar()
        {
            if (Estado == EstadoReserva.Cancelada)
                throw new InvalidOperationException("La reserva ya ha sido cancelada previamente.");

            Estado = EstadoReserva.Cancelada;
            EventoReservado.AumentarCupos(CantidadEntradas);
        }

        public override string ToString()
        {
            return $"Código: {IdReserva,-8} | Cliente: {NombrePersona,-20} | Evento: {EventoReservado.Nombre,-25} " +
                   $"| Entradas: {CantidadEntradas,-2} | Total: {ValorTotal:C} | Estado: {Estado}";
        }
    }
}
