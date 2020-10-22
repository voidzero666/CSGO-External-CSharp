using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Input;

namespace KaiserWahre
{
    class Triggerbot
    {
        [DllImport("user32.dll")]
        public static extern bool GetAsyncKeyState(int Keys);
        static int LocalPlayer;
        static int EntityList;
        static int LocalTeam;
        static int EntityHealth;
        static int CrosshairID;
        static int CrosshairIDTeam;
        static int EntityInCrosshair;
        [DllImport("user32.dll")]
        static extern void mouse_event(int dwFlags, int dx, int dy, int dwData, int dwExtraInfo);

        [Flags]
        public enum MouseEventFlags
        {
            LEFTDOWN = 0x00000002,
            LEFTUP = 0x00000004,
            MIDDLEDOWN = 0x00000020,
            MIDDLEUP = 0x00000040,
            MOVE = 0x00000001,
            ABSOLUTE = 0x00008000,
            RIGHTDOWN = 0x00000008,
            RIGHTUP = 0x00000010
        }

        public static void Trigger()
        {
            while (true)
            {
                Thread.Sleep(1);
                while (GetAsyncKeyState(0x04))
                {
                   LocalPlayer = KaiserMemory.ReadMemory<int>(Program.ClientPtr + hazedumper.signatures.dwLocalPlayer);
                   LocalTeam =  KaiserMemory.ReadMemory<int>(LocalPlayer + hazedumper.netvars.m_iTeamNum);
                   CrosshairID = KaiserMemory.ReadMemory<int>(LocalPlayer + hazedumper.netvars.m_iCrosshairId);
                   EntityInCrosshair = KaiserMemory.ReadMemory<int>(Program.ClientPtr + hazedumper.signatures.dwEntityList + (CrosshairID -1)*0x10);
                   CrosshairIDTeam = KaiserMemory.ReadMemory<int>(EntityInCrosshair + hazedumper.netvars.m_iTeamNum);
                   EntityHealth = KaiserMemory.ReadMemory<int>(EntityInCrosshair + hazedumper.netvars.m_iHealth);
                   if (EntityHealth > 0)
                    {
                        if (LocalTeam != CrosshairIDTeam)
                        {
                            mouse_event((int)(MouseEventFlags.LEFTDOWN), 0, 0, 0, 0);
                            mouse_event((int)(MouseEventFlags.LEFTUP), 0, 0, 0, 0);
                        }
                    }

                    Thread.Sleep(1);
                }
            }
        }

    }
}
