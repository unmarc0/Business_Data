using Business_Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Business_Data.Data
{
    public class DatosEmpresasContext : DbContext
    {
        public DatosEmpresasContext(DbContextOptions<DatosEmpresasContext> options)
            : base(options)
        {
        }

        // DbSet para la tabla 'Empresas'
        public DbSet<Empresa> Empresas { get; set; }
    }
}
