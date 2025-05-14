using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegram_bot_c_.Models
{
    public class HairType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }
        public int Price { get; set; }

        [NotMapped]
        public Image img
        {
            get 
            {
                if(Image == null || Image.Length == 0)
                {
                    return null;
                }
                using (var ms = new MemoryStream(Image))
                {
                    try
                    {
                        return System.Drawing.Image.FromStream(ms);
                    }
                    catch
                    {
                        return null;
                    }
                }
                
            }
        }
    }
}
