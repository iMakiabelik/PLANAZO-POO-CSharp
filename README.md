PLANAZO - Sistema de Gestión de Eventos y Reservas

Aplicación de consola desarrollada en C# y .NET basada en los principios de la Programación Orientada a Objetos (POO). El sistema permite gestionar eventos (Cine, Conciertos, Talleres), consultar disponibilidad, realizar reservas con cálculo dinámico de precios y cancelar reservas restableciendo cupos en tiempo real.

Estructura del Proyecto


PLANAZO-POO-CSharp/
├── Modelos/
│   ├── Evento.cs          # Clase abstracta base
│   ├── Cine.cs            # Subclase: Evento de tipo Cine (Sin recargo)
│   ├── Concierto.cs       # Subclase: Evento de tipo Concierto (+15% sobre precio base)
│   ├── Taller.cs          # Subclase: Evento de tipo Taller (-10% descuento)
│   └── Reserva.cs         # Modelo para la gestión de reservas
├── Servicios/
│   └── GestorReservas.cs  # Lógica de negocio, colecciones y excepciones
└── Program.cs             # Interfaz de consola e interacción con el usuario

Aplicación de Principios POO
​Abstracción: La clase abstracta Evento define el modelo conceptual base con atributos comunes y el método abstracto CalcularPrecioFinal().
​Encapsulamiento: Propiedades con modificadores adecuados (private set en CuposDisponibles) y validaciones internas en métodos como ReducirCupos y AumentarCupos.
​Herencia: Cine, Concierto y Taller heredan directamente de Evento, reutilizando atributos y comportamiento general.
​Polimorfismo: Sobrescritura (override) del método CalcularPrecioFinal() según las reglas de negocio específicas de cada tipo de evento:
​Cine: Precio base directo.
​Concierto: Precio base + 15%.
​Taller: Precio base - 10%.
​Manejo de Excepciones: Uso de bloques try-catch y excepciones estándar (KeyNotFoundException, ArgumentException, InvalidOperationException) para validar entradas del usuario y reglas de negocio.
​Colecciones Genéricas: Utilización de List<T> para el manejo dinámico de eventos y reservas en memoria.


Pregunta de Sustentación

​¿Por qué es conveniente usar una clase abstracta Evento en lugar de una interfaz para los tipos de evento?
​Es más conveniente usar la clase abstracta Evento porque los eventos comparten un estado base y lógica común reutilizable (propiedades como Id, Nombre, Fecha, PrecioBase, CapacidadMaxima y CuposDisponibles, así como los métodos concretos ReducirCupos y AumentarCupos). Una interfaz solo forzaría la firma de los métodos sin permitir compartir implementación de código ni variables de estado, lo que generaría duplicación innecesaria en cada clase de evento.
