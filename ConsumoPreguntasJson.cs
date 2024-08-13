namespace FabricaDePreguntas;

using System.Runtime.Intrinsics.X86;
using System.Text.Json;
using Usuario;

public class ConsumoPreguntasJson
{
    private string rutaPreguntas;

    public ConsumoPreguntasJson(Category categoria)
    {
        rutaPreguntas = "CategoriaJson/" + categoria.name + ".json";
    }

    private List<Pregunta> ObtenerPreguntasJson()
    {

        List<Pregunta> preguntas = new List<Pregunta>();
        string cadenaJson = File.ReadAllText(rutaPreguntas);

        if (!string.IsNullOrEmpty(cadenaJson))
        {
            preguntas = JsonSerializer.Deserialize<List<Pregunta>>(cadenaJson);
        }
        return preguntas;
    }

    public List<Pregunta> ObtenerListadoPreguntas(string nivel, int limit)
    {

        List<Pregunta> preguntas = ObtenerPreguntasJson();
        List<Pregunta> preguntasFiltradas = new List<Pregunta>();

        for (int i = 0; i < limit; i++)
        {
            Pregunta preg = preguntas.Find(p => p.level == nivel);
            preguntasFiltradas.Add(preg);
            preguntas.Remove(preg);
        }

        return preguntasFiltradas;

    }






}