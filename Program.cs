using FabricaDePreguntas;
using Usuario;
internal class Programs
{
    private static async Task Main(string[] args)
    {
        Player usuario = CrearUsuario();

        List<Category> categorias = await ConsumoApiCategoria.GetCategorias();

        Category categoria = CategoriaSeleccionada(categorias);

        List<string> respuestas = ObtenerListadoDeOpciones();

        while (usuario.CantidadVidas() > 0)
        {
            List<Pregunta> preguntas = await ObtenerPreguntasPorCategoria(usuario, categoria);

            int i = 1;
            foreach (var preguntaX in preguntas)
            {
                MostrarPregunta(i, preguntaX);
                int index2 = RespuestaUsuario();

                if (preguntaX.correct_answer.ToString() == respuestas[index2])
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("¡Muy bien, Respuesta Correcta!");
                    Console.ResetColor();
                    usuario.SumarRespuestaCorrecta();
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("¡Ups, Respuesta Incorrecta! :(");
                    Console.ResetColor();
                    usuario.SumarRespuestaIncorrecta();
                    usuario.DisminuirVidas();
                }

                if (usuario.CantidadVidas() == 0)
                {
                    break;
                }

                i++;
            }
            if (usuario.CantidadVidas() > 0)
            {
                System.Console.WriteLine("Felicidades, Pasaste al siguiente nivel");
                if (usuario.ObtenerNivel() == Nivel.facil.ToString())
                {
                    usuario.CambiarNivelMedio();
                }
                else
                {
                    usuario.CambiarNivelDificil();
                }
            }
            else
            {
                Console.WriteLine("Perdiste");
            }
        }
    }

    private static int RespuestaUsuario()
    {
        System.Console.WriteLine("Elija la opcion correcta");
        int respuesta;
        do
        {
            string cadena = Console.ReadLine();
            bool resultado = int.TryParse(cadena, out respuesta);
            if (!resultado || respuesta <= 0 || respuesta > 4)
            {
                System.Console.WriteLine("opcion no valida, ingrese nuevamente");
            }

        } while (respuesta <= 0 || respuesta > 4);

        return (respuesta - 1);
    }

    private static List<string> ObtenerListadoDeOpciones()
    {
        List<string> respuestas = new List<string>();
        respuestas.Add("answer_a");
        respuestas.Add("answer_b");
        respuestas.Add("answer_c");
        respuestas.Add("answer_d");
        return respuestas;
    }

    private static void MostrarPregunta(int i, Pregunta preguntaX)
    {
        System.Console.WriteLine("Pregunta Nro {0}",i);
        System.Console.WriteLine(preguntaX.question);
        System.Console.WriteLine("\t1) " + preguntaX.answers.answer_a);
        System.Console.WriteLine("\t2) " + preguntaX.answers.answer_b);
        System.Console.WriteLine("\t3) " + preguntaX.answers.answer_c);
        System.Console.WriteLine("\t4) " + preguntaX.answers.answer_d);
    }

    private static async Task<List<Pregunta>> ObtenerPreguntasPorCategoria(Player usuario, Category categoriaSeleccionada)
    {
        string nivel = usuario.ObtenerNivel();
        int limite = usuario.ObtenerCantidadPreguntas();
        List<Pregunta> preguntas = await ConsumoApiPregunta.GetPreguntasAPI2(categoriaSeleccionada, nivel, limite);
        return preguntas;
    }

    private static Player CrearUsuario()
    {
        string nombre;
        System.Console.WriteLine("Ingrese su nombre:");
        do
        {
            nombre = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(nombre))
            {
                System.Console.WriteLine("Ingrese nuevamente");
            }

        } while (string.IsNullOrWhiteSpace(nombre));

        Player usuario = new Player(nombre);
        return usuario;

    }

    private static Category CategoriaSeleccionada(List<Category> categorias)
    {
        Console.WriteLine("Listado de Categorias:");

        int num = 1;
        foreach (var categoriaX in categorias)
        {
            Console.WriteLine("{0} - {1}", num, categoriaX.name);
            num++;
        }

        Console.WriteLine("Seleccione una categoria:");
        int opcion;
        do
        {
            string cad = Console.ReadLine();
            bool result = int.TryParse(cad, out opcion);

            if (!result || opcion <= 0 || opcion > categorias.Count)
            {
                System.Console.WriteLine("Ingrese nuevamente la categoria");
            }

        } while (opcion <= 0 || opcion > categorias.Count);

        int index = opcion - 1;

        return categorias[index];

    }
}