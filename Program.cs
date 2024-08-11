using System.Text.Json;
using System.Text.Json.Nodes;
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

        int nivel = 1;

        while (usuario.CantidadVidas() > 0 && nivel <= 3)
        {
            List<Pregunta> preguntas = await ObtenerPreguntasPorCategoria(usuario, categoria);

            int nroPregunta = 1;

            foreach (var preguntaX in preguntas)
            {
                MostrarPregunta(nroPregunta, preguntaX);

                int index2 = RespuestaUsuario();

                if (preguntaX.correct_answer == respuestas[index2])
                {
                    ImprimirMensajeColor("¡Muy bien, Respuesta Correcta!", ConsoleColor.Green);
                    usuario.SumarRespuestaCorrecta();
                }
                else
                {
                    ImprimirMensajeColor("¡Ups, Respuesta Incorrecta! :(", ConsoleColor.Red);
                    usuario.SumarRespuestaIncorrecta();
                    usuario.DisminuirVidas();
                }

                if (usuario.CantidadVidas() == 0)
                {
                    break;
                }

                nroPregunta++;
            }

            if (usuario.CantidadVidas() > 0)
            {
                if (nivel < 3)
                {
                    if (usuario.ObtenerNivel() == Nivel.facil.ToString())
                    {
                        ImprimirMensajeColor("Felicidades, Pasaste al nivel 2", ConsoleColor.DarkYellow);
                        usuario.CambiarNivelMedio();
                    }
                    else
                    {
                        ImprimirMensajeColor("Felicidades, Pasaste al nivel 3", ConsoleColor.DarkYellow);
                        usuario.CambiarNivelDificil();
                    }
                }
                nivel++;
            }
        }

        if (usuario.CantidadVidas() > 0)
        {
            ImprimirMensajeColor("¡FELICIDADES, GANASTE!", ConsoleColor.DarkGreen);
            usuario.CalcularPuntaje();
            GuardarGanadorJson(usuario);
        }
        else
        {
            ImprimirMensajeColor("¡PERDISTE!", ConsoleColor.DarkRed);
        }

        System.Console.WriteLine("GAME OVER");

        List<Historico> rankingGanadores = ObtenerListadoGanadores();

        MostrarRanking(rankingGanadores);
    }

    private static void MostrarRanking(List<Historico> rankingGanadores)
    {
        if (rankingGanadores.Count() > 0)
        {
            ImprimirMensajeColor("---RANKING MEJORES JUGADORES---", ConsoleColor.Magenta);

            foreach (var ganador in rankingGanadores)
            {
                System.Console.WriteLine("Nombre:" + ganador.Nombre);
                System.Console.WriteLine("Puntaje:" + ganador.Puntaje);
                System.Console.WriteLine("Cantidad Respuestas Correctas:" + ganador.CantRespuestaCorrecta);
                System.Console.WriteLine("Cantidad Respuestas Incorrectas:" + ganador.CantRespuestaIncorrecta);
                System.Console.WriteLine();
            }
        }
        else
        {
            System.Console.WriteLine("No hay Ganadores aun");
        }
    }

    private static List<Historico> ObtenerListadoGanadores()
    {
        ManejoArchivo helperArchivo = new ManejoArchivo();

        List<Historico> rankingGanadores = helperArchivo.ObtenerListadoGanadores();
        return rankingGanadores;
    }

    private static void GuardarGanadorJson(Player usuario)
    {
        ManejoArchivo helperArchivo = new ManejoArchivo();
        helperArchivo.GuardarGanadorJson(usuario);
    }

    private static void ImprimirMensajeColor(string mensaje, ConsoleColor color)
    {
        Console.ForegroundColor = color;
        System.Console.WriteLine(mensaje);
        Console.ResetColor();
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
                System.Console.WriteLine("Opcion invalida, ingrese nuevamente");
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
        System.Console.WriteLine("Pregunta Nro {0}", i);
        System.Console.WriteLine(preguntaX.question);

        if (!string.IsNullOrEmpty(preguntaX.answers.answer_a))
        {
            System.Console.WriteLine("\t1) " + preguntaX.answers.answer_a);
        }

        if (!string.IsNullOrEmpty(preguntaX.answers.answer_b))
        {
            System.Console.WriteLine("\t2) " + preguntaX.answers.answer_b);
        }

        if (!string.IsNullOrEmpty(preguntaX.answers.answer_c))
        {
            System.Console.WriteLine("\t3) " + preguntaX.answers.answer_c);
        }

        if (!string.IsNullOrEmpty(preguntaX.answers.answer_d))
        {
            System.Console.WriteLine("\t4) " + preguntaX.answers.answer_d);
        }

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
        ImprimirMensajeColor("LISTADO DE CATEGORIAS:", ConsoleColor.Magenta);

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