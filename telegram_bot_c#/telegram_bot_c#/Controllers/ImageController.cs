using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Telegram.Bot.Types;
using telegram_bot_c_.Models;
using System.Xml.Linq;

namespace telegram_bot_c_.Controllers
{
    internal class ImageController
    {
        public byte[] getImage(string name)
        {
            using (var context = new Context())
            {
                HairType hair = context.hair_types.FirstOrDefault(h => h.Name.ToLower() == name.ToLower());
                return hair.Image;
            }
        }
        
        public List<HairType> getHairAll()
        {
            using (var context = new Context())
            {
                return context.hair_types.ToList();
            }
        }
    }
}
