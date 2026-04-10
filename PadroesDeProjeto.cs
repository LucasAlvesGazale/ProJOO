using System;

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
            Console.WriteLine($"[EMAIL] Para {destinatario}: {mensagem}");
        }
    }

    class NotificacaoPush : INotificacao
    {
        public void Enviar(string destinatario, string mensagem)
        {
            Console.WriteLine($"[PUSH] Para {destinatario}: {mensagem}");
        }
    }
    class Configuracao
    {
        private static Configuracao instancia;

        public string NomeApp = "Sistema de Notificações";
        public bool PermitirEnvio = true;
        public int MaximoTentativas = 3;

        private Configuracao() { }

        public static Configuracao GetInstancia()
        {
            if (instancia == null)
                instancia = new Configuracao();

            return instancia;
        }
    }

    class ApiSmsExterna
    {
        public void MandarSms(string numero, string texto)
        {
            Console.WriteLine($"[API EXTERNA SMS] Para {numero}: {texto}");
        }
    }

    class AdaptadorSmsExterno : INotificacao
    {
        private ApiSmsExterna apiSmsExterna;

        public AdaptadorSmsExterno(ApiSmsExterna apiSmsExterna)
        {
            this.apiSmsExterna = apiSmsExterna;
        }

        public void Enviar(string destinatario, string mensagem)
        {
            apiSmsExterna.MandarSms(destinatario, mensagem);
        }
    }
    class ProxyNotificacao : INotificacao
    {
        private INotificacao notificacaoReal;

        public ProxyNotificacao(INotificacao notificacaoReal)
        {
            this.notificacaoReal = notificacaoReal;
        }

        public void Enviar(string destinatario, string mensagem)
        {
            var config = Configuracao.GetInstancia();

            if (!config.PermitirEnvio)
            {
                Console.WriteLine("Envio bloqueado pela configuração.");
                return;
            }

            if (string.IsNullOrWhiteSpace(destinatario))
            {
                Console.WriteLine("Destinatário inválido.");
                return;
            }

            Console.WriteLine($"[LOG] Enviando mensagem para {destinatario}...");

            notificacaoReal.Enviar(destinatario, mensagem);

            Console.WriteLine("[LOG] Envio concluído.");
        }
    }

    class FabricaNotificacao
    {
        public static INotificacao Criar(CanalNotificacao canal)
        {
            if (canal == CanalNotificacao.Email)
            {
                return new ProxyNotificacao(new NotificacaoEmail());
            }

            if (canal == CanalNotificacao.Push)
            {
                return new ProxyNotificacao(new NotificacaoPush());
            }

            if (canal == CanalNotificacao.Sms)
            {
                ApiSmsExterna apiExterna = new ApiSmsExterna();
                INotificacao adaptador = new AdaptadorSmsExterno(apiExterna);
                return new ProxyNotificacao(adaptador);
            }

            return null;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var config = Configuracao.GetInstancia();
            config.NomeApp = "Sistema de Notificações";
            config.PermitirEnvio = true;
            config.MaximoTentativas = 3;

            Console.WriteLine(config.NomeApp);
            Console.WriteLine();

            INotificacao notificacaoEmail = FabricaNotificacao.Criar(CanalNotificacao.Email);
            notificacaoEmail.Enviar("lucas@email.com", "Olá, esta é uma mensagem por email.");

            Console.WriteLine();

            INotificacao notificacaoSms = FabricaNotificacao.Criar(CanalNotificacao.Sms);
            notificacaoSms.Enviar("11999999999", "Olá, esta é uma mensagem por SMS.");

            Console.WriteLine();

            INotificacao notificacaoPush = FabricaNotificacao.Criar(CanalNotificacao.Push);
            notificacaoPush.Enviar("usuario123", "Olá, esta é uma notificação push.");

            Console.ReadLine();
        }
    }
}
