using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegram_bot_c_.Models
{
    public class Context : DbContext
    {
        public Context() : base("myCS") { }
        public DbSet<Client> clients { get; set; }
        public DbSet<Master> masters { get; set; }
        public DbSet<Note> notes { get; set; }
        public DbSet<HairType> hair_types { get; set; }
    }
}
