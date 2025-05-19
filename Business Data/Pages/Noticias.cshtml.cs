using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace Business_Data.Pages
{
    public class NoticiasModel : PageModel
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string ApiKey = "c3fb96766e67241d9272f579443f6193"; // ⬅️ Cambia aquí por tu API key real

        public List<Noticia> NoticiasEncontradas { get; set; } = new();
        public string EmpresaSeleccionada { get; set; }

        public NoticiasModel(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [BindProperty(SupportsGet = true)]
        public string Nombre { get; set; }

        public async Task<IActionResult> OnGetBuscarAsync(string empresa)
        {
            if (string.IsNullOrWhiteSpace(empresa))
            {
                if (!string.IsNullOrWhiteSpace(Nombre))
                    empresa = Nombre;
                else
                    return Page();
            }

            EmpresaSeleccionada = empresa;

            var httpClient = _clientFactory.CreateClient();
            var url = $"https://gnews.io/api/v4/search?q={empresa}&lang=es&token={ApiKey}";
            var respuesta = await httpClient.GetAsync(url);

            if (respuesta.IsSuccessStatusCode)
            {
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var datos = JsonSerializer.Deserialize<GNewsResponse>(contenido);

                if (datos?.articles != null)
                {
                    NoticiasEncontradas = datos.articles.Select(a => new Noticia
                    {
                        Titulo = a.title,
                        Enlace = a.url,
                        Descripcion = a.description
                    }).ToList();
                }
            }

            return Page();
        }


        public class GNewsResponse
        {
            public List<GNewsArticle> articles { get; set; }
        }

        public class GNewsArticle
        {
            public string title { get; set; }
            public string url { get; set; }
            public string description { get; set; }
        }

        public class Noticia
        {
            public string Titulo { get; set; }
            public string Enlace { get; set; }
            public string Descripcion { get; set; }
        }
    }
}
