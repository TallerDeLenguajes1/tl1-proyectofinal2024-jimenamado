using FabricaDePreguntas;
internal class Program
{
    private static async Task Main(string[] args)
    {
        List<Category> categorias = await ConsumoApiCategoria.GetCategorias();
        System.Console.WriteLine("Seleccione una Categoria:");

        int num = 1;
        foreach (var categoriaX in categorias)
        {
            System.Console.WriteLine("{0} - {1}", num, categoriaX.name);
            num++;
        }

        int opcion;
        string cad = Console.ReadLine();
        bool result = int.TryParse(cad, out opcion);

        List<Pregunta> preguntas = await ConsumoApiPregunta.GetPreguntasAPI("html","facil",10);
        int i = 1;
        foreach (var preguntaX in preguntas)
        {
            System.Console.WriteLine("Pregunta {0} :{1}",i,preguntaX.question);
            i++;
        }

    }
}