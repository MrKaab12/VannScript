using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vann
{
    class Globals
    {
        public static bool anyError = false;
        public static string errorValue = "";



        public enum TYPES
        {
           
            QUETE,
            STRING,
            VARIABLE,
            PARAN_START,
            PARAN_END,
            PARAN_SUS_START,
            PARAN_SUS_END,
            COMMAND,
            COMMENT
        }

    }

  
}
