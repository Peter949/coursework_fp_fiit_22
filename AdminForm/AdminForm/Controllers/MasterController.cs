using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot_c_.Models;

namespace AdminForm.Controllers
{
    internal class MasterController
    {
        public static Master getById(int id)
        {
            using (var context = new Context())
            {
                var master = context.masters.Find(id);
                return master;
            }
        }
        public static List<Master> getMasters()
        {
            using (var context = new Context())
            {
                var masters = context.masters.ToList();
                return masters;
            }
        }
        public static void addMaster(string fio, string phone)
        {
            using (var context = new Context())
            {
                var master = new Master();
                master.Name = fio;
                master.Phone = phone;
                context.masters.Add(master);
                context.SaveChanges();
            }
        }
        public static void deleteMaster(int id)
        {
            using (var context = new Context())
            {
                var master = getById(id);
                context.Entry(master).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
