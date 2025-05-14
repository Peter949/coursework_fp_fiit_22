using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot_c_.Models;

namespace AdminForm.Controllers
{
    internal class NotesController
    {
        public static List<Note> getNotes()
        {
            using (var context = new Context())
            {
                var notes = context.notes.ToList();
                return notes;
            }
        }
    }
}
