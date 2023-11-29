namespace WebApplication6.Model
{
    public class Reserva
    {
        public int id { get; set; }
        public string? Especialidad { get; set; }
        public DateTime? DiaReserva { get; set; }

        public int? Paciente_idPaciente { get; set; }


    }
}
