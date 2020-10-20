using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
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
            Console.Title = "KaiserWahre, Heil, Kaiser, Dir"; //b1g meme title

            KaiserMemory.initProc("csgo"); //init process handle on CSGO process.
            ClientPtr = KaiserMemory.GetModuleBaseAdress("client");

            Thread glow = new Thread(new ThreadStart(Glow.GlowThread));
            glow.Start();
        }


       
    }
}
