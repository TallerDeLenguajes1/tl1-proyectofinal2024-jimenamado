using System.Text.Json;
using FabricaDePreguntas;

public class ConsumoCategoriaJson
{
    private string rutaCategoria;

    public ConsumoCategoriaJson()
    {
        rutaCategoria = "CategoriaJson/categoria.json";
    }

    private Root ObtenerRootJson(){

        Root objeto = new Root();

        string cadenaJson = File.ReadAllText(rutaCategoria);
        
        if (!string.IsNullOrEmpty(cadenaJson))
        {
            objeto = JsonSerializer.Deserialize<Root>(cadenaJson);
        }

        return objeto;
    }

    public List<Category> ObtenerListadoCategorias(){

        Root root = ObtenerRootJson();
        
        var categoria = root.categories;

        return categoria;
    }
}