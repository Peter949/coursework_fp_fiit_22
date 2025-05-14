using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using telegram_bot_c_.Models;

namespace telegram_bot_c_.Controllers
{
    internal class NoteController
    {
        public static void createNote(int client_id, int hair_id, int master_id)
        {
            using (var context = new Context())
            {
                var note = new Note();
                note.Name = $"{client_id}, {hair_id}, {master_id}";
                note.Hair_id = hair_id;
                note.Client_id = client_id;
                note.Master_id = master_id;
                context.notes.Add(note);
                context.SaveChanges();
            }
        }
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
