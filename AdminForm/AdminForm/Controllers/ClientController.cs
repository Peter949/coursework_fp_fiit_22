using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot_c_.Models;

namespace AdminForm.Controllers
{
    internal class ClientController
    {
        public static Client getById(int id)
        {
            using (var context = new Context())
            {
                var client = context.clients.Find(id);
                return client;
            }
        }
        public static List<Client> getClients()
        {
            using (var context = new Context())
            {
                var clients = context.clients.ToList();
                return clients;
            }
        }
        public static void deleteClient(int id)
        {
            using (var context = new Context())
            {
                var client = getById(id);
                context.Entry(client).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}



