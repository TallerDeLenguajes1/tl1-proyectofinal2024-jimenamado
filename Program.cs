using FabricaDePreguntas;
using Usuario;
internal class Programs
{
    private static async Task Main(string[] args)
    {
        System.Console.WriteLine("Ingrese su nombre:");
        string nombre = Console.ReadLine();

        Player usuario = new Player(nombre);

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

        while (usuario.CantidadVidas() > 0)
        {

            string nivel = usuario.ObtenerNivel(); 
            int limite = usuario.ObtenerCantidadPreguntas(); 
            List<Pregunta> preguntas = await ConsumoApiPregunta.GetPreguntasAPI2(categoriaSeleccionada, nivel, limite);
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
                int index2 = respuesta - 1;

                if (preguntaX.correct_answer.ToString() == respuestas[index2])
                {
                    System.Console.WriteLine("Respuesta Correcta");
                    usuario.SumarRespuestaCorrecta();
                }
                else
                {
                    System.Console.WriteLine("Respuesta Incorrecta");
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
                if(usuario.ObtenerNivel() == Nivel.facil.ToString())
                {
                    usuario.CambiarNivelMedio();

                }else
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




}