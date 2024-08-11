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
    private int cantidadPreguntasPorNivel;

    public Player(string nombre)
    {
        this.nombre = nombre;
        puntaje = 0;
        vidas = 3;
        cantRespuestaCorrecta = 0;
        cantRespuestaIncorrecta = 0;
        nivel = Nivel.facil;
        cantidadPreguntasPorNivel = 1;
    }
    public void DisminuirVidas()
    {
        vidas--;
    }
    public void CalcularPuntaje()
    {
        puntaje = cantRespuestaCorrecta * 5 - cantRespuestaIncorrecta * 3;
    }
    public int ObtenerPuntaje(){
        return puntaje;
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
    public int CantidadRespuestasCorrectas()
    {
        return cantRespuestaCorrecta;
    }
    public int CantidadRespuestasIncorrectas()
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
        cantidadPreguntasPorNivel = 1;
    }
    public void CambiarNivelDificil(){

        nivel = Nivel.dificil;
        cantidadPreguntasPorNivel = 1;
    }

    public int ObtenerCantidadPreguntas(){

        return cantidadPreguntasPorNivel;
    }



}