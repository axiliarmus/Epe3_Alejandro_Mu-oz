namespace WebApplication6.Model
{
    public class Medicos
    {
        public int id { get; set; }
        public String? NombreMed { get; set; }
        public String? ApellidoMed { get; set; }
        public String? RunMed { get; set; }
        public String? Eunacom { get; set; }
        public String? NacionalidadMed { get; set; }
        public String? Especialidad { get; set; }

        public DateTime Horarios { get; set; }
        public int TarifaHr { get; set; }

    }
}
