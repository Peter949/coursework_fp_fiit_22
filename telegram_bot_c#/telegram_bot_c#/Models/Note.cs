using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegram_bot_c_.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Master_id { get; set; }
        public int Client_id { get; set; }
        public int Hair_id { get; set; }
    }
}
