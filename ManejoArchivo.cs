
using System.Text.Json;
using System.Text.Json.Nodes;
using Usuario;

public class ManejoArchivo
{
    private string rutaRankingJson;

    public ManejoArchivo()
    {
        rutaRankingJson = "Ranking.json";
    }

    private List<Historico> ObtenerGanadoresJson()
    {
        List<Historico> lista = new List<Historico>();  //lista vacia

        string contenidoJson = File.ReadAllText(rutaRankingJson);

        if (!string.IsNullOrEmpty(contenidoJson))
        {
            lista = JsonSerializer.Deserialize<List<Historico>>(contenidoJson);
        }

        return lista;
    }

    private void GuardarGanadoresJson(List<Historico> lista)
    {
        string formatoJson = JsonSerializer.Serialize(lista);
        File.WriteAllText(rutaRankingJson, formatoJson);
    }

    public void GuardarGanadorJson(Player usuario)
    {
        Historico ganador = new Historico(usuario);

        List<Historico> ranking = new List<Historico>();

        if (File.Exists(rutaRankingJson))
        {
            ranking = ObtenerGanadoresJson();
        }

        ranking.Add(ganador);

        ranking = ranking.OrderByDescending(t => t.Puntaje).ToList();

        GuardarGanadoresJson(ranking);
    }

    public List<Historico> ObtenerListadoGanadores()
    {
        List<Historico> ganadores = new List<Historico>();

        if (File.Exists(rutaRankingJson))
        {
            ganadores = ObtenerGanadoresJson();
        }
        return ganadores;
    }


}