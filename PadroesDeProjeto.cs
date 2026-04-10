namespace PadroesDeProjeto
{
    enum CanalNotificacao
    {
        Email,
        Sms,
        Push
    }
    interface INotificacao
    {
        void Enviar(string destinatario, string mensagem);
    }
    class NotificacaoEmail : INotificacao
    {
        public void Enviar(string destinatario, string mensagem)
        {
            Console.WriteLine($"Email para {destinatario}: {mensagem}");
        }
    }

    class NotificacaoSms : INotificacao
    {
        public void Enviar(string destinatario, string mensagem)
        {
            Console.WriteLine($"SMS para {destinatario}: {mensagem}");
        }
    }

    class NotificacaoPush : INotificacao
    {
        public void Enviar(string destinatario, string mensagem)
        {
            Console.WriteLine($"Push para {destinatario}: {mensagem}");
        }
    }
    class Configuracao
    {
        private static Configuracao instancia;

        public string NomeApp = "Sistema";

        private Configuracao() { }

        public static Configuracao GetInstancia()
        {
            if (instancia == null)
            {
                instancia = new Configuracao();
            }
            return instancia;
        }
    }

    class FabricaNotificacao
    {
        public static INotificacao Criar(CanalNotificacao canal)
        {
            if (canal == CanalNotificacao.Email)
                return new NotificacaoEmail();

            if (canal == CanalNotificacao.Sms)
                return new NotificacaoSms();

            if (canal == CanalNotificacao.Push)
                return new NotificacaoPush();

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            // usando singleton
            var config = Configuracao.GetInstancia();
            Console.WriteLine("App: " + config.NomeApp);

            // usando factory
            INotificacao notificacao = FabricaNotificacao.Criar(CanalNotificacao.Email);

            notificacao.Enviar("lucas@email.com", "Mensagem de teste");

            Console.ReadLine();
        }
    }
}
