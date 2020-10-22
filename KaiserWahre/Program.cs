using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using hazedumper;

namespace KaiserWahre
{
    class Program
    {
        public static int ClientPtr;

        static void Main(string[] args)
        {
            StartKaiserreich();
        }

        static void StartKaiserreich()
        {
            Console.Title = "KaiserWahre, Heil, Kaiser, Dir";


            KaiserMemory.initProc("csgo"); //init process handle on CSGO process.
            ClientPtr = KaiserMemory.GetModuleBaseAdress("client");

<<<<<<< Updated upstream
            Thread t = new Thread(new ThreadStart(Glow.GlowThread));
            Thread Bh = new Thread(new ThreadStart(Bunnyhop.Bhop));
            Thread TB = new Thread(new ThreadStart(Triggerbot.Trigger));
            t.Start();
            Bh.Start();
            TB.Start();
        } 
=======
            Thread glow = new Thread(new ThreadStart(Glow.GlowThread));
            Thread bhop = new Thread(new ThreadStart(Bhops.Bhop));
            Thread Trigger = new Thread(new ThreadStart(Triggerbot.Trigger));
            glow.Start();
            bhop.Start();
            Trigger.Start();

        }
>>>>>>> Stashed changes
    }
}
