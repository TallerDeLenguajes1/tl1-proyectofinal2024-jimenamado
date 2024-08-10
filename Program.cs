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

        int index = opcion - 1;

        Category categoriaSeleccionada = categorias[index];



        List<Pregunta> preguntas = await ConsumoApiPregunta.GetPreguntasAPI2(categoriaSeleccionada, "facil", 10);
        int i = 1;
        foreach (var preguntaX in preguntas)
        {
            System.Console.WriteLine("Pregunta {0} :{1}", i, preguntaX.question);
            System.Console.WriteLine("Elija la opcion correcta");
            System.Console.WriteLine(preguntaX.answers.answer_a);
            System.Console.WriteLine(preguntaX.answers.answer_b);
            System.Console.WriteLine(preguntaX.answers.answer_c);
            System.Console.WriteLine(preguntaX.answers.answer_d);

            List<string> respuestas = new List<string>();
            respuestas.Add("answer_a");
            respuestas.Add("answer_b");
            respuestas.Add("answer_c");
            respuestas.Add("answer_d");
            
            int respuesta;
            string cadena = Console.ReadLine();
            bool resultado = int.TryParse(cadena, out respuesta);
            int index2 = respuesta-1;

            if (preguntaX.correct_answer == respuestas[index2])
            {
                System.Console.WriteLine("Respuesta Correcta");
            }else
            {
                System.Console.WriteLine("Respuesta Incorrecta");
            }
            i++;
        }






    }




}