using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace KaiserWahre
{
    class Glow
    {
        static int ClientPtr = Program.ClientPtr;

        static int GlowEntityIdx;
        static int Glower;
        static int EntityGlowIndex;
        static int GlowBasePtr;
        static int LocalPlayer;
        static int entityHealth;
        static int LocalTeam;
        static int GLLocalTeam;

        static float R = 1.0f; //red
        static float G = 0.0f; //green
        static float B = 0.0f; //blue
        static float ALFA = 1.0f; //alpha

        static bool t = true;
        static bool f = false;

        public static void GlowThread()
        {
            while (true)
            {
                for (int i = 0; i <= 64; i++)
                {
                    GlowEntityIdx = i; //glow entity index is at iterator

                    LocalPlayer = KaiserMemory.ReadMemory<int>(ClientPtr + hazedumper.signatures.dwLocalPlayer);
                    GlowBasePtr = KaiserMemory.ReadMemory<int>(ClientPtr + hazedumper.signatures.dwGlowObjectManager);
                    LocalTeam = KaiserMemory.ReadMemory<int>(LocalPlayer + hazedumper.netvars.m_iTeamNum);
                    Glower = KaiserMemory.ReadMemory<int>(ClientPtr + hazedumper.signatures.dwEntityList + (GlowEntityIdx - 1) * 0x10);
                    EntityGlowIndex = KaiserMemory.ReadMemory<int>(Glower + hazedumper.netvars.m_iGlowIndex);
                    GLLocalTeam = KaiserMemory.ReadMemory<int>(Glower + 0xF4);
                    entityHealth = KaiserMemory.ReadMemory<int>(Glower + 0x100);

                    if (GLLocalTeam != LocalTeam)
                    {

                        KaiserMemory.WriteMemory<float>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0x4), R);
                        KaiserMemory.WriteMemory<float>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0x8), G);
                        KaiserMemory.WriteMemory<float>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0xC), B);
                        KaiserMemory.WriteMemory<float>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0x10), ALFA);
                        KaiserMemory.WriteMemory<bool>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0x24), t);
                        KaiserMemory.WriteMemory<bool>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0x25), f);
                        KaiserMemory.WriteMemory<bool>(GlowBasePtr + ((EntityGlowIndex * 0x38) + 0x2C), f);
                    }

                    Thread.Sleep(1);
                }
            }
        }
    }
}
