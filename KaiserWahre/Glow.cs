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

                    SDK.GlowObjectDefinition obj = new SDK.GlowObjectDefinition();

                    if (GLLocalTeam != LocalTeam)
                    {
                        obj = KaiserMemory.ReadMemory<SDK.GlowObjectDefinition>(GlowBasePtr + (EntityGlowIndex * 0x38));

                        obj.r = 1.0f;
                        obj.g = 0.0f;
                        obj.b = 0.0f;
                        obj.a = 1.0f;
                        obj.m_bRenderWhenOccluded = true;
                        obj.m_bRenderWhenUnoccluded = false;
                        obj.m_bFullBloom = false;

                        KaiserMemory.WriteMemory<SDK.GlowObjectDefinition>(GlowBasePtr + (EntityGlowIndex * 0x38), obj);
                    }

                    Thread.Sleep(1);
                }
            }
        }
    }
}
