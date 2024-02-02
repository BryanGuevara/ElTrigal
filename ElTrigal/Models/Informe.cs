using System.ComponentModel;

namespace ElTrigal.Models
{
    public class Informe
    {
        public Guid Id { get; set; }

        [DisplayName("Tipo")]
        public string? TipoInforme { get; set; }

        [DisplayName("Descripcion")]
        public string? DetallesInforme { get; set; }

        [DisplayName("Fecha que se hizo la acción")]
        public DateTime FechaGeneracion { get; set; }
    }
}
