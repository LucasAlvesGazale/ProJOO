using System;
using System.IO;

namespace HomeTheaterApp
{

    class Televisao
    {
        public void Ligar() => Console.WriteLine("TV ligada");
        public void Desligar() => Console.WriteLine("TV desligada");
        public void ModoCinema() => Console.WriteLine("TV em modo cinema");
    }

    class Projetor
    {
        public void Ligar() => Console.WriteLine("Projetor ligado");
        public void Desligar() => Console.WriteLine("Projetor desligado");
    }

    class Receiver
    {
        public void Ligar() => Console.WriteLine("Receiver ligado");
        public void Desligar() => Console.WriteLine("Receiver desligado");
        public void ConfigurarSurround() => Console.WriteLine("Som surround ativo");
    }

    class PlayerMidia
    {
        public void Ligar() => Console.WriteLine("Player ligado");
        public void Desligar() => Console.WriteLine("Player desligado");
        public void Reproduzir(string nome) => Console.WriteLine($"Reproduzindo: {nome}");
    }

    class SomAmbiente
    {
        public void Ligar() => Console.WriteLine("Som ligado");
        public void Desligar() => Console.WriteLine("Som desligado");
        public void AjustarVolume(int v) => Console.WriteLine($"Volume: {v}");
    }

    class LuzAmbiente
    {
        public void Atenuar() => Console.WriteLine("Luz reduzida");
        public void Desligar() => Console.WriteLine("Luz desligada");
    }

    class HomeTheaterFacade
    {
        private Televisao tv;
        private Projetor projetor;
        private Receiver receiver;
        private PlayerMidia player;
        private SomAmbiente som;
        private LuzAmbiente luz;

        public HomeTheaterFacade(
            Televisao tv,
            Projetor projetor,
            Receiver receiver,
            PlayerMidia player,
            SomAmbiente som,
            LuzAmbiente luz)
        {
            this.tv = tv;
            this.projetor = projetor;
            this.receiver = receiver;
            this.player = player;
            this.som = som;
            this.luz = luz;
        }

        public void AssistirFilme(string filme)
        {
            Console.WriteLine("=== Modo Cinema ===");

            luz.Atenuar();
            tv.Ligar();
            tv.ModoCinema();

            projetor.Ligar();
            receiver.Ligar();
            receiver.ConfigurarSurround();

            som.Ligar();
            som.AjustarVolume(70);

            player.Ligar();
            player.Reproduzir(filme);
        }

        public void OuvirMusica(string musica)
        {
            Console.WriteLine("=== Modo Música ===");

            tv.Desligar();
            projetor.Desligar();

            som.Ligar();
            som.AjustarVolume(50);

            player.Ligar();
            player.Reproduzir(musica);
        }

        public void DesligarTudo()
        {
            Console.WriteLine("=== Desligando ===");

            player.Desligar();
            som.Desligar();
            receiver.Desligar();
            projetor.Desligar();
            tv.Desligar();
            luz.Desligar();
        }
    }

    class Testes
    {
        public static void TesteModoCinema()
        {
            var facade = Criar();

            var sw = new StringWriter();
            Console.SetOut(sw);

            facade.AssistirFilme("Matrix");

            string output = sw.ToString();

            bool passou =
                output.Contains("Modo Cinema") &&
                output.Contains("Matrix");

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            Console.WriteLine(passou ? "TesteModoCinema OK" : "TesteModoCinema FALHOU");
        }

        public static void TesteModoMusica()
        {
            var facade = Criar();

            var sw = new StringWriter();
            Console.SetOut(sw);

            facade.OuvirMusica("Imagine");

            string output = sw.ToString();

            bool passou =
                output.Contains("Modo Música") &&
                output.Contains("Imagine");

            Console.SetOut(new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = true });

            Console.WriteLine(passou ? "TesteModoMusica OK" : "TesteModoMusica FALHOU");
        }

        private static HomeTheaterFacade Criar()
        {
            return new HomeTheaterFacade(
                new Televisao(),
                new Projetor(),
                new Receiver(),
                new PlayerMidia(),
                new SomAmbiente(),
                new LuzAmbiente()
            );
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var home = new HomeTheaterFacade(
                new Televisao(),
                new Projetor(),
                new Receiver(),
                new PlayerMidia(),
                new SomAmbiente(),
                new LuzAmbiente()
            );

            // Execução normal
            home.AssistirFilme("Matrix");
            Console.WriteLine();

            home.OuvirMusica("Imagine");
            Console.WriteLine();

            home.DesligarTudo();
            Console.WriteLine();

            // Testes
            Console.WriteLine("=== TESTES ===");
            Testes.TesteModoCinema();
            Testes.TesteModoMusica();

            Console.ReadLine();
        }
    }
}
