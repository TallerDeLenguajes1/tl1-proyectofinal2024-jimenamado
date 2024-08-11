namespace Usuario;
public class Historico
{
    private string nombre;
    private int puntaje;
    private int cantRespuestaCorrecta;
    private int cantRespuestaIncorrecta;

    public string Nombre { get => nombre; set => nombre = value; }
    public int Puntaje { get => puntaje; set => puntaje = value; }
    public int CantRespuestaCorrecta { get => cantRespuestaCorrecta; set => cantRespuestaCorrecta = value; }
    public int CantRespuestaIncorrecta { get => cantRespuestaIncorrecta; set => cantRespuestaIncorrecta = value; }

    public Historico()
    {
    
    }
    public Historico(Player usuario)
    {
        nombre = usuario.NombrePlayer();
        puntaje = usuario.ObtenerPuntaje();
        cantRespuestaCorrecta = usuario.CantidadRespuestasCorrectas();
        cantRespuestaIncorrecta = usuario.CantidadRespuestasIncorrectas();
    }


}

