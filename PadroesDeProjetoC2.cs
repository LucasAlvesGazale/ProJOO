using System;

namespace CafeteriaDecorator
{
    // Interface comum
    public interface IBebida
    {
        string GetDescricao();
        double GetCusto();
    }

    public class CafeExpresso : IBebida
    {
        public string GetDescricao() => "Café expresso";
        public double GetCusto() => 5.00;
    }

    public class Cappuccino : IBebida
    {
        public string GetDescricao() => "Cappuccino";
        public double GetCusto() => 8.50;
    }

    public class Cha : IBebida
    {
        public string GetDescricao() => "Chá";
        public double GetCusto() => 4.00;
    }

    // Decorator base
    public abstract class AdicionalDecorator : IBebida
    {
        protected IBebida bebida;

        protected AdicionalDecorator(IBebida bebida)
        {
            this.bebida = bebida;
        }

        public abstract string GetDescricao();
        public abstract double GetCusto();
    }

    public class Leite : AdicionalDecorator
    {
        public Leite(IBebida bebida) : base(bebida) { }

        public override string GetDescricao() => bebida.GetDescricao() + ", leite";
        public override double GetCusto() => bebida.GetCusto() + 1.50;
    }

    public class Chantilly : AdicionalDecorator
    {
        public Chantilly(IBebida bebida) : base(bebida) { }

        public override string GetDescricao() => bebida.GetDescricao() + ", chantilly";
        public override double GetCusto() => bebida.GetCusto() + 2.00;
    }

    public class Canela : AdicionalDecorator
    {
        public Canela(IBebida bebida) : base(bebida) { }

        public override string GetDescricao() => bebida.GetDescricao() + ", canela";
        public override double GetCusto() => bebida.GetCusto() + 0.75;
    }

    public class CaldaChocolate : AdicionalDecorator
    {
        public CaldaChocolate(IBebida bebida) : base(bebida) { }

        public override string GetDescricao() => bebida.GetDescricao() + ", calda de chocolate";
        public override double GetCusto() => bebida.GetCusto() + 2.50;
    }

    class Program
    {
        static void MostrarPedido(IBebida bebida)
        {
            Console.WriteLine("Pedido:");
            Console.WriteLine(" - " + bebida.GetDescricao());
            Console.WriteLine($" - Total: R$ {bebida.GetCusto():0.00}");
            Console.WriteLine();
        }

        static void Main(string[] args)
        {
            IBebida pedido1 = new CafeExpresso();
            pedido1 = new Leite(pedido1);
            pedido1 = new Canela(pedido1);

            IBebida pedido2 = new Cappuccino();
            pedido2 = new Chantilly(pedido2);
            pedido2 = new CaldaChocolate(pedido2);

            IBebida pedido3 = new Cha();
            pedido3 = new Leite(pedido3);
            pedido3 = new Chantilly(pedido3);
            pedido3 = new Canela(pedido3);

            MostrarPedido(pedido1);
            MostrarPedido(pedido2);
            MostrarPedido(pedido3);

            Console.WriteLine("Pressione ENTER para sair...");
            Console.ReadLine();
        }
    }
}
