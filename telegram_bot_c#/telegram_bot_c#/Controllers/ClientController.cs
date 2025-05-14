using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot_c_.Models;

namespace telegram_bot_c_.Controllers
{
    internal class ClientController
    {
        public static List<Client> getClients()
        {
            using (var context = new Context())
            {
                return context.clients.ToList();
            }
        }
        public static void addClient(string name, long id)
        {
            using (var context = new Context())
            {
                Client client = new Client();
                client.Name = name;
                client.Telegram_id = id;
                context.clients.Add(client);
                context.SaveChanges();
            }
        }
    }
}
