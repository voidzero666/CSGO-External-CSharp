using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Runtime.InteropServices;

namespace KaiserWahre
{
    class KaiserMemory
    {
        #region DllImports

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);

        [DllImport("kernel32.dll")]
        private static extern bool ReadProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, ref int lpNumberOfBytesRead);

        [DllImport("kernel32.dll")]
        private static extern bool WriteProcessMemory(int hProcess, int lpBaseAddress, byte[] buffer, int size, out int lpNumberOfBytesWritten);

        #endregion DllImports

        //proc all access define
        private const int PROCESS_ALL_ACCESS = 0xFFFF;

        public static Process TargetProcess = null;
        public static IntPtr procHandle;

        public static int m_iNumberOfBytesRead = 0;
        public static int m_iNumberOfBytesWritten = 0;

        //get a handle on csgo proc
        public static void initProc(string procName)
        {
            do
            {
                if (Process.GetProcessesByName(procName).Length > 0)
                {
                    TargetProcess = Process.GetProcessesByName(procName)[0];
                    Console.WriteLine("Found Process!");
                    Thread.Sleep(100);
                }
            }
            while (TargetProcess == null);

            procHandle = OpenProcess(PROCESS_ALL_ACCESS, false, TargetProcess.Id); // Sets Our ProcessHandle
        }

        //Getmodulebaseaddress C++ to C# rofl
        public static int GetModuleBaseAdress(string modName)
        {
            try
            {
                foreach (ProcessModule ProcMod in TargetProcess.Modules)
                {
                    if (!modName.Contains(".dll"))
                        modName = modName.Insert(modName.Length, ".dll");

                    if (modName == ProcMod.ModuleName)
                    {
                        return (int)ProcMod.BaseAddress;
                    }
                }
            }
            catch { }
            return -1;
        }


        public static T ReadMemory<T>(int address) where T : struct
        {
            int ByteSize = Marshal.SizeOf(typeof(T));

            byte[] buffer = new byte[ByteSize];

            ReadProcessMemory((int)procHandle, address, buffer, buffer.Length, ref m_iNumberOfBytesRead);

            return ByteArrayToStructure<T>(buffer);
        }


        public static byte[] ReadMemory(int offset, int size)
        {
            byte[] buffer = new byte[size];

            ReadProcessMemory((int)procHandle, offset, buffer, size, ref m_iNumberOfBytesRead);

            return buffer;
        }


        public static void WriteMemory<T>(int Adress, object Value)
        {
            byte[] buffer = StructureToByteArray(Value); // Transform Data To ByteArray

            WriteProcessMemory((int)procHandle, Adress, buffer, buffer.Length, out m_iNumberOfBytesWritten);
        }

        public static void WriteMemory<T>(int Adress, char[] Value)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(Value);

            WriteProcessMemory((int)procHandle, Adress, buffer, buffer.Length, out m_iNumberOfBytesWritten);
        }















        #region Transformation
        public static float[] ConvertToFloatArray(byte[] bytes)
        {
            if (bytes.Length % 4 != 0)
                throw new ArgumentException();

            float[] floats = new float[bytes.Length / 4];

            for (int i = 0; i < floats.Length; i++)
                floats[i] = BitConverter.ToSingle(bytes, i * 4);

            return floats;
        }

        private static T ByteArrayToStructure<T>(byte[] bytes) where T : struct
        {
            var handle = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            try
            {
                return (T)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(T));
            }
            finally
            {
                handle.Free();
            }
        }

        public static byte[] StructureToByteArray(object obj)
        {
            int len = Marshal.SizeOf(obj);

            byte[] arr = new byte[len];

            IntPtr ptr = Marshal.AllocHGlobal(len);

            Marshal.StructureToPtr(obj, ptr, true);
            Marshal.Copy(ptr, arr, 0, len);
            Marshal.FreeHGlobal(ptr);

            return arr;
        }
        #endregion


    }

}
