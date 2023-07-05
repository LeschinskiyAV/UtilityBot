using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UtilityBot.Models
{
    public class Session
    {
        public string BotMode { get; set; }

        public Session()
        {
            BotMode = "none";
        }
    }

}
