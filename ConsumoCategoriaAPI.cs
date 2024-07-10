using System.Net;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace FabricaDePreguntas;

//consumir Categorias

public static class ConsumoApiCategoria
{

    private static readonly HttpClient client = new HttpClient(); //creamos una instancia

    private static async Task<Root> GetRootCategoria()
    {
        
        try
        {
            var url = "https://www.preguntapi.dev/api/categories/";
            HttpResponseMessage response = await client.GetAsync(url); //enviar solicitud GET
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync(); //leer la respuesta
            Root root = JsonSerializer.Deserialize<Root>(responseBody);

            return (root);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0}", ex.Message);
        }

        return null;

    }

    public static async Task<List<Category>> GetCategorias()
    {
        var root = await GetRootCategoria();
        if (root != null)
        {
            var categorias = root.categories;
            return categorias;
        }else
        {
            return null;
        }
    }

    
}