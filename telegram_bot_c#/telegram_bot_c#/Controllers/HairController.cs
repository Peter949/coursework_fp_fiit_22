using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot_c_.Models;

namespace AdminForm.Controllers
{
    internal class HairController
    {
        public static HairType getById(int id)
        {
            using (var context = new Context())
            {
                var hair = context.hair_types.Find(id);
                return hair;
            }
        }
        public static List<HairType> getHairs()
        {
            using (var context = new Context())
            {
                var hairs = context.hair_types.ToList();
                return hairs;
            }
        }
        public static void addHair(string name, int price, byte[] img)
        {
            using (var context = new Context())
            {
                var hair = new HairType();
                hair.Name = name;
                hair.Price = price;
                hair.Image = img;
                context.hair_types.Add(hair);
                context.SaveChanges();
            }
        }
        public static void deleteHair(int id)
        {
            using (var context = new Context())
            {
                var hair = getById(id);
                context.Entry(hair).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }
    }
}
