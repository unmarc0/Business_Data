using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Business_Data.Models
{
    public class Empresa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // No generar automáticamente el UniqueIdentifier
        public Guid EmpresaId { get; set; } = Guid.NewGuid(); // Generar el GUID automáticamente

        [Required] // Marca como obligatorio
        [StringLength(255)] // Longitud máxima para el campo Nombre
        public string Nombre { get; set; } = string.Empty; // Valor predeterminado vacío

        [Required] // Marca como obligatorio
        [StringLength(50)] // Longitud máxima para el campo Tipo
        public string Tipo { get; set; } = string.Empty; // Valor predeterminado vacío

        [Required] // Marca como obligatorio
        [StringLength(100)] // Longitud máxima para ComunidadAutonoma
        public string ComunidadAutonoma { get; set; } = string.Empty; // Valor predeterminado vacío

        [StringLength(100)] // Longitud máxima para Provincia
        public string? Provincia { get; set; } // Permite valores nulos

        [StringLength(100)] // Longitud máxima para Ciudad
        public string? Ciudad { get; set; } // Permite valores nulos

        [StringLength(255)] // Longitud máxima para Direccion
        public string? Direccion { get; set; } // Permite valores nulos

        [StringLength(10)] // Longitud máxima para CodigoPostal
        public string? CodigoPostal { get; set; } // Permite valores nulos

        [Required] // Marca como obligatorio
        public decimal Capital { get; set; } = 0; // Valor predeterminado

        public decimal? Beneficios { get; set; } // Nullable en caso de que no haya datos

        public decimal? Perdidas { get; set; } // Nullable en caso de que no haya datos

        public int? NumeroTrabajadores { get; set; } // Nullable en caso de que no haya datos

        [StringLength(100)] // Longitud máxima para Sector
        public string? Sector { get; set; } // Permite valores nulos

        [StringLength(100)] // Longitud máxima para Subsector
        public string? Subsector { get; set; } // Permite valores nulos

        [Required] // Marca como obligatorio
        public DateTime FechaCreacion { get; set; } = DateTime.Now; // Valor predeterminado con fecha actual

        [StringLength(50)] // Longitud máxima para Estado
        public string Estado { get; set; } = "Activa"; // Valor por defecto
    }
}
