using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vann
{
    class Token
    {
        public Globals.TYPES type;
        public string value;

        public Token(Globals.TYPES _type,string _value)
        {
            this.type = _type;
            this.value = _value;
        }

    }
}
