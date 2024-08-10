using System.Net;
using System.Text.Json;
using System.Runtime.CompilerServices;

namespace FabricaDePreguntas;

//Consumir Preguntas

public static class ConsumoApiPregunta
{

    private static readonly HttpClient client = new HttpClient();

    public static async Task<List<Pregunta>> GetPreguntasAPI(string categoria,string level, int limit)
    {
        try
        {
            //https://www.preguntapi.dev/api/categories/javascript?level=facil&limit=5
           var url = "https://www.preguntapi.dev/api/categories/"+categoria+"?level="+level+"&limit="+limit;
            HttpResponseMessage response = await client.GetAsync(url); //enviar solicitud GET
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync(); //leer la respuesta
            List<Pregunta> preguntas = JsonSerializer.Deserialize<List<Pregunta>>(responseBody);

            return (preguntas);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0}", ex.Message);
        }

        return null;
    }

       public static async Task<List<Pregunta>> GetPreguntasAPI2(Category categoria,string level, int limit)
    {
        try
        {
            //https://www.preguntapi.dev/api/categories/javascript?level=facil&limit=5
           //"http://www.preguntapi.dev/api/categories/javascript"
            var url = categoria.link+"?level="+level+"&limit="+limit;
            HttpResponseMessage response = await client.GetAsync(url); //enviar solicitud GET
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync(); //leer la respuesta
            List<Pregunta> preguntas = JsonSerializer.Deserialize<List<Pregunta>>(responseBody);

            return (preguntas);
        }
        catch (HttpRequestException ex)
        {
            Console.WriteLine("Problemas de acceso a la API");
            Console.WriteLine("Message :{0}", ex.Message);
        }

        return null;
    }

    


}