using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace KaiserWahre
{
    class Bhops
    {
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(int Keys);
        static int LocalPlayer;
        static int Flag;
        static int DwForceJump;
        static int Jump = 5;
        static int deJump = 4;

        public static void Bhop()
        {
            while (true)
            {

                while (GetAsyncKeyState(0x20))
                {
                    LocalPlayer = KaiserMemory.ReadMemory<int>(Program.ClientPtr + hazedumper.signatures.dwLocalPlayer);
                    Flag = KaiserMemory.ReadMemory<int>(LocalPlayer + hazedumper.netvars.m_fFlags);
                    if (Flag == 257)
                    {
                        KaiserMemory.WriteMemory<int>(Program.ClientPtr + hazedumper.signatures.dwForceJump, Jump);

                    }
                    if (Flag == 256)
                    {
                        KaiserMemory.WriteMemory<int>(Program.ClientPtr + hazedumper.signatures.dwForceJump, deJump);

                    }
                }
            }
            Thread.Sleep(1);

        }

    }
}