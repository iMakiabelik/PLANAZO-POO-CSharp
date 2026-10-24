using System;
using PLANAZO.Servicios;

namespace PLANAZO
{
    internal class Program
    {
        private static readonly GestorReservas gestor = new GestorReservas();

        static void Main(string[] args)
        {
            Console.Title = "PLANAZO - Gestión de Eventos y Reservas";
            bool salir = false;

            while (!salir)
            {
                MostrarEncabezado();
                Console.WriteLine(" 1. Ver eventos disponibles");
                Console.WriteLine(" 2. Consultar evento");
                Console.WriteLine(" 3. Reservar entradas");
                Console.WriteLine(" 4. Ver reservas realizadas");
                Console.WriteLine(" 5. Cancelar una reserva");
                Console.WriteLine(" 6. Ver resumen de reservas");
                Console.WriteLine(" 0. Salir");
                Console.WriteLine("==================================================");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine()?.Trim();
                Console.WriteLine();

                switch (opcion)
                {
                    case "1":
                        VerEventosDisponibles();
                        break;
                    case "2":
                        ConsultarEvento();
                        break;
                    case "3":
                        ReservarEntradas();
                        break;
                    case "4":
                        VerReservasRealizadas();
                        break;
                    case "5":
                        CancelarReserva();
                        break;
                    case "6":
                        VerResumen();
                        break;
                    case "0":
                        salir = true;
                        MostrarMensaje("¡Gracias por utilizar PLANAZO!", ConsoleColor.Cyan);
                        break;
                    default:
                        MostrarError("Opción no válida. Por favor, seleccione una opción entre 0 y 6.");
                        break;
                }

                if (!salir)
                {
                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                    Console.ReadKey();
                }
            }
        }

        private static void MostrarEncabezado()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("==================================================");
            Console.WriteLine("                PLANAZO - EVENTOS                 ");
            Console.WriteLine("==================================================");
            Console.ResetColor();
        }

        private static void VerEventosDisponibles()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- LISTA DE EVENTOS REGISTRADOS ---");
            Console.ResetColor();

            var eventos = gestor.ObtenerEventos();
            foreach (var evento in eventos)
            {
                Console.WriteLine(evento);
            }
        }

        private static void ConsultarEvento()
        {
            Console.Write("Ingrese el ID del evento a consultar (ej. CIN01, CON01, TAL01): ");
            string id = Console.ReadLine();

            try
            {
                var evento = gestor.ConsultarEvento(id);
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n[Detalle del Evento Encontrado]");
                Console.ResetColor();
                Console.WriteLine(evento);
                Console.WriteLine($"Precio Base: {evento.PrecioBase:C}");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private static void ReservarEntradas()
        {
            try
            {
                Console.Write("Ingrese el ID del evento: ");
                string idEvento = Console.ReadLine();

                Console.Write("Ingrese su nombre completo: ");
                string nombre = Console.ReadLine();

                Console.Write("Ingrese la cantidad de entradas a reservar: ");
                if (!int.TryParse(Console.ReadLine(), out int cantidad))
                {
                    MostrarError("Debe ingresar un número entero válido.");
                    return;
                }

                var reserva = gestor.RealizarReserva(idEvento, nombre, cantidad);
                MostrarExito($"\n¡Reserva exitosa! {reserva}");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private static void VerReservasRealizadas()
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("--- HISTORIAL DE RESERVAS ---");
            Console.ResetColor();

            var reservas = gestor.ObtenerReservas();
            if (reservas.Count == 0)
            {
                Console.WriteLine("No hay reservas registradas en el sistema.");
                return;
            }

            foreach (var reserva in reservas)
            {
                Console.WriteLine(reserva);
            }
        }

        private static void CancelarReserva()
        {
            try
            {
                Console.Write("Ingrese el código de la reserva a cancelar (ej. RES-001): ");
                string idReserva = Console.ReadLine();

                gestor.CancelarReserva(idReserva);
                MostrarExito("\n¡Reserva cancelada correctamente y cupos restituidos!");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }
        }

        private static void VerResumen()
        {
            var (activas, entradas, total) = gestor.ObtenerResumen();

            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.WriteLine("--- RESUMEN GENERAL DE RESERVAS ACTIVAS ---");
            Console.ResetColor();
            Console.WriteLine($"• Total de Reservas Activas : {activas}");
            Console.WriteLine($"• Total Entradas Reservadas  : {entradas}");
            Console.WriteLine($"• Valor Total Recaudado     : {total:C}");
        }

        private static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n[ERROR] {mensaje}");
            Console.ResetColor();
        }

        private static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }

        private static void MostrarMensaje(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ResetColor();
        }
    }
}
