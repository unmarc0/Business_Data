using Business_Data.Data;
using Business_Data.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc;

namespace Business_Data.Pages
{
    public class IndexModel : PageModel
    {
        private readonly DatosEmpresasContext _context;

        public IndexModel(DatosEmpresasContext context)
        {
            _context = context;
        }

        // Lista de empresas a mostrar en la vista
        public List<Empresa> Empresas { get; set; } = new List<Empresa>();

        // Propiedad para recibir los filtros desde la query string
        [BindProperty(SupportsGet = true)]
        public EmpresaFiltro Filtro { get; set; } = new EmpresaFiltro();

        // Listas para rellenar los combobox
        public List<string> Comunidades { get; set; } = new List<string>();
        public List<string> Tipos { get; set; } = new List<string>();
        public List<string> Provincias { get; set; } = new List<string>();
        public List<string> Sectores { get; set; } = new List<string>();
        public List<string> Subsectors { get; set; } = new List<string>();
        public List<string> Estados { get; set; } = new List<string>();

        // Datos para las gráficas
        public Dictionary<string, int> EmpresasPorComunidad { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> EmpresasPorSector { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> EmpresasPorTipo { get; set; } = new Dictionary<string, int>();
        public Dictionary<string, int> EmpresasPorEstado { get; set; } = new Dictionary<string, int>();

        // Datos para las nuevas gráficas
        public Dictionary<string, int> EmpresasPorProvincia { get; set; } = new();
        public Dictionary<string, int> EmpresasPorSubsector { get; set; } = new();
        public Dictionary<string, int> EmpresasPorCiudad { get; set; } = new();
        public Dictionary<string, int> EmpresasPorTrabajadoresRango { get; set; } = new();

        // Datos para el gráfico de Top 15 Empresas por Capital
        public List<string> EmpresasNombres { get; set; } = new List<string>();
        public List<decimal> EmpresasCapital { get; set; } = new List<decimal>();

        // Paginación
        public int PaginaActual { get; set; }
        public int TotalPaginas { get; set; }
        private const int ResultadosPorPagina = 10;

        public void OnGet()
        {
            // Rellenar los combobox con datos únicos de la base de datos
            Comunidades = _context.Empresas.Select(e => e.ComunidadAutonoma).Distinct().ToList();
            Tipos = _context.Empresas.Select(e => e.Tipo).Distinct().ToList();
            Provincias = _context.Empresas.Select(e => e.Provincia)
                                           .Where(p => p != null)
                                           .Distinct().ToList();
            Sectores = _context.Empresas.Select(e => e.Sector)
                                         .Where(s => s != null)
                                         .Distinct().ToList();
            Subsectors = _context.Empresas.Select(e => e.Subsector)
                                           .Where(s => s != null)
                                           .Distinct().ToList();
            Estados = _context.Empresas.Select(e => e.Estado).Distinct().ToList();

            // Construir la consulta base
            var query = _context.Empresas.AsQueryable();

            // Aplicar los filtros
            if (!string.IsNullOrEmpty(Filtro.Nombre))
                query = query.Where(e => e.Nombre.Contains(Filtro.Nombre));
            if (!string.IsNullOrEmpty(Filtro.Tipo))
                query = query.Where(e => e.Tipo == Filtro.Tipo);
            if (!string.IsNullOrEmpty(Filtro.ComunidadAutonoma))
                query = query.Where(e => e.ComunidadAutonoma == Filtro.ComunidadAutonoma);
            if (!string.IsNullOrEmpty(Filtro.Provincia))
                query = query.Where(e => e.Provincia == Filtro.Provincia);
            if (!string.IsNullOrEmpty(Filtro.Ciudad))
                query = query.Where(e => e.Ciudad.Contains(Filtro.Ciudad));
            if (!string.IsNullOrEmpty(Filtro.Direccion))
                query = query.Where(e => e.Direccion.Contains(Filtro.Direccion));
            if (!string.IsNullOrEmpty(Filtro.CodigoPostal))
                query = query.Where(e => e.CodigoPostal.Contains(Filtro.CodigoPostal));
            if (Filtro.Capital.HasValue)
                query = query.Where(e => e.Capital >= Filtro.Capital.Value);
            if (Filtro.Beneficios.HasValue)
                query = query.Where(e => e.Beneficios.HasValue && e.Beneficios >= Filtro.Beneficios.Value);
            if (Filtro.Perdidas.HasValue)
                query = query.Where(e => e.Perdidas.HasValue && e.Perdidas >= Filtro.Perdidas.Value);
            if (Filtro.NumeroTrabajadores.HasValue)
                query = query.Where(e => e.NumeroTrabajadores.HasValue && e.NumeroTrabajadores >= Filtro.NumeroTrabajadores.Value);
            if (!string.IsNullOrEmpty(Filtro.Sector))
                query = query.Where(e => e.Sector == Filtro.Sector);
            if (!string.IsNullOrEmpty(Filtro.Subsector))
                query = query.Where(e => e.Subsector == Filtro.Subsector);
            if (Filtro.FechaCreacion.HasValue)
                query = query.Where(e => e.FechaCreacion.Date == Filtro.FechaCreacion.Value.Date);
            if (!string.IsNullOrEmpty(Filtro.Estado))
                query = query.Where(e => e.Estado == Filtro.Estado);

            // Obtener todas las empresas filtradas
            Empresas = query.ToList();

            // Gráfica: Empresas por Comunidad Autónoma
            EmpresasPorComunidad = Empresas
                .GroupBy(e => e.ComunidadAutonoma)
                .ToDictionary(g => string.IsNullOrWhiteSpace(g.Key) ? "Desconocido" : g.Key, g => g.Count());

            // Gráfica: Empresas por Sector
            EmpresasPorSector = Empresas
                .GroupBy(e => e.Sector)
                .ToDictionary(g => string.IsNullOrWhiteSpace(g.Key) ? "Desconocido" : g.Key, g => g.Count());

            // Gráfica: Empresas por Tipo
            EmpresasPorTipo = Empresas
                .GroupBy(e => e.Tipo)
                .ToDictionary(g => string.IsNullOrWhiteSpace(g.Key) ? "Desconocido" : g.Key, g => g.Count());

            // Gráfica: Empresas por Estado
            EmpresasPorEstado = Empresas
                .GroupBy(e => e.Estado)
                .ToDictionary(g => string.IsNullOrWhiteSpace(g.Key) ? "Desconocido" : g.Key, g => g.Count());

            // Gráficas adicionales
            EmpresasPorProvincia = query.GroupBy(e => e.Provincia)
                .ToDictionary(g => g.Key ?? "Desconocida", g => g.Count());
            EmpresasPorSubsector = query.GroupBy(e => e.Subsector)
                .ToDictionary(g => g.Key ?? "Desconocido", g => g.Count());
            EmpresasPorCiudad = query.GroupBy(e => e.Ciudad)
                .ToDictionary(g => g.Key ?? "Desconocida", g => g.Count());
            EmpresasPorTrabajadoresRango = query.AsEnumerable().GroupBy(e =>
            {
                int n = e.NumeroTrabajadores ?? 0;
                if (n < 10) return "1-9";
                if (n < 50) return "10-49";
                if (n < 250) return "50-249";
                return "250+";
            }).ToDictionary(g => g.Key, g => g.Count());

            // **NUEVO: Calcular Top 15 Empresas por Capital (antes de paginación)**
            var topEmpresas = query.OrderByDescending(e => e.Capital).Take(15).ToList();
            EmpresasNombres = topEmpresas.Select(e => e.Nombre).ToList();
            EmpresasCapital = topEmpresas.Select(e => e.Capital).ToList();

            // Paginación
            int totalEmpresas = query.Count();
            TotalPaginas = (int)Math.Ceiling(totalEmpresas / (double)ResultadosPorPagina);
            PaginaActual = int.TryParse(Request.Query["pagina"], out var p) && p > 0 ? p : 1;

            Empresas = query.Skip((PaginaActual - 1) * ResultadosPorPagina)
                            .Take(ResultadosPorPagina)
                            .ToList();
        }
    }

    // Clase para los filtros de búsqueda
    public class EmpresaFiltro
    {
        public string Nombre { get; set; }
        public string Tipo { get; set; }
        public string ComunidadAutonoma { get; set; }
        public string Provincia { get; set; }
        public string Ciudad { get; set; }
        public string Direccion { get; set; }
        public string CodigoPostal { get; set; }
        public decimal? Capital { get; set; }
        public decimal? Beneficios { get; set; }
        public decimal? Perdidas { get; set; }
        public int? NumeroTrabajadores { get; set; }
        public string Sector { get; set; }
        public string Subsector { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string Estado { get; set; }
    }
}
