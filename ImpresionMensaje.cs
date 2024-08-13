using System.Drawing;

public class ImpresionMensaje
{
    private string rutaTitulo;
    private string rutaLogo;
    private string rutaFinJuego;

    public ImpresionMensaje()
    {
        rutaTitulo = "AsciiArt/titulo.txt";
        rutaLogo = "AsciiArt/logo.txt";
        rutaFinJuego = "AsciiArt/finJuego.txt";
    }

    private void Imprimir(string ruta){

        if (File.Exists(ruta))
        {
            using (var stream = new StreamReader(ruta))
            {
                while (!stream.EndOfStream)
                {
                    string linea = stream.ReadLine();
                    System.Console.WriteLine(linea);
                }

                stream.Close();
            }
        }
    }

    public void ImprimirTitulo(){

        Imprimir(rutaTitulo);
        System.Console.WriteLine();
    }
    public void ImprimirLogo(){
        Console.ForegroundColor = ConsoleColor.DarkBlue;
        Imprimir(rutaLogo);
        Console.ResetColor();
        System.Console.WriteLine();
        
    }
    public void ImprimirFinDelJuego(){
        Console.ForegroundColor = ConsoleColor.DarkMagenta;
        Imprimir(rutaFinJuego);
        Console.ResetColor();
        System.Console.WriteLine();
    }
    
}