using System;
using System.Runtime.InteropServices;

namespace ConsoleApp1
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var a = new AClass();
            var b = a;
            Console.WriteLine(a);
            Console.WriteLine(a.GetAddress());
            Console.WriteLine(b.GetAddress());
        }
    }

    public static class VariableHelper
    {
        public static string GetAddress(this object obj)
        {
            GCHandle handle = GCHandle.Alloc(obj, GCHandleType.Pinned);
            IntPtr pointer = GCHandle.ToIntPtr(handle);
            string pointerDisplay = pointer.ToString();
            return pointerDisplay;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public class AClass
    {
        public int MyProperty { get; set; }
    }
}