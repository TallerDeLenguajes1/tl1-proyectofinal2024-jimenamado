using System.Runtime.CompilerServices;
namespace Usuario;

public enum Nivel{

    facil,
    normal,
    dificil
}
public class Player
{
    private string nombre;
    private int puntaje;
    private int vidas;
    private int cantRespuestaCorrecta;
    private int cantRespuestaIncorrecta;

    private Nivel nivel;

    private int cantidadPreguntas;

    public Player(string nombre)
    {
        this.nombre = nombre;
        puntaje = 0;
        vidas = 3;
        cantRespuestaCorrecta = 0;
        cantRespuestaIncorrecta = 0;
        nivel = Nivel.facil;
        cantidadPreguntas = 5;
    }

    public void DisminuirVidas()
    {

        vidas--;
    }
    public void CalcularPuntaje()
    {

        puntaje = cantRespuestaCorrecta * 5 - cantRespuestaIncorrecta * 3;

    }
    public void SumarRespuestaCorrecta()
    {

        cantRespuestaCorrecta++;
    }
    public void SumarRespuestaIncorrecta()
    {

        cantRespuestaIncorrecta++;
    }

    //retornar datos finales
    public int CantidadVidas()
    {

        return vidas;
    }
    public int CantidadPreguntasCorrectas()
    {

        return cantRespuestaCorrecta;

    }
    public int CantidadPreguntasIncorrectas()
    {
        return cantRespuestaIncorrecta;
    }

    public string NombrePlayer(){

        return nombre;
    }
    
/****/
    public string ObtenerNivel(){

        return nivel.ToString();
    }
    public void CambiarNivelMedio(){

        nivel = Nivel.normal;
        cantidadPreguntas = 5;
    }
    public void CambiarNivelDificil(){

        nivel = Nivel.dificil;
        cantidadPreguntas = 5;
    }

    public int ObtenerCantidadPreguntas(){

        return cantidadPreguntas;
    }



}