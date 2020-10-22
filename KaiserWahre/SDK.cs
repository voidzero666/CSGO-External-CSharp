using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace KaiserWahre
{
    class SDK
    {
        [StructLayout(LayoutKind.Explicit)]
        public struct GlowObjectDefinition
        {
            [FieldOffset(0x00)]
            public int Entity;
            [FieldOffset(0x4)]
            public float r;
            [FieldOffset(0x8)]
            public float g;
            [FieldOffset(0xC)]
            public float b;
            [FieldOffset(0x10)]
            public float a;
            [FieldOffset(0x14)]
            public int Space1;
            [FieldOffset(0x18)]
            public int Space2;
            [FieldOffset(0x1C)]
            public float BloomAmount;
            [FieldOffset(0x20)]
            public int Space3;

            [FieldOffset(0x24)]
            public bool m_bRenderWhenOccluded;
            [FieldOffset(0x25)]
            public bool m_bRenderWhenUnoccluded;
            [FieldOffset(0x2C)]
            public bool m_bFullBloom;
        };
    }
}
