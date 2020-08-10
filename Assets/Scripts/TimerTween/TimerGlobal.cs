using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZGame.TimerTween
{
    class TimerGlobal
    {
        private static int counter = 0;
        public static int Counter
        {
            get
            {
                return counter;
            }
            set
            {
                if (value > 1000000)
                {
                    value = 1;
                }
                counter = value;
            }
        }
    }
}
