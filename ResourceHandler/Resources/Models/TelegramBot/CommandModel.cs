using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ResourceHandler.Resources.Models.TelegramBot
{
    public class CommandModel
    {
        public string CommandText { get; set; }
        public string[] CommandSections { get; set; }
        public bool CommandIsAvailable { get; set; }
        public string Command { get; set; }
        public bool CommandHasParameter { get; set; }
        public string CommandParameter { get; set; }
        public DateTime Date { get; set; }

        public CommandModel()
        {
            this.Date = DateTime.Now;
        }
    }
}
