using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;

namespace GettingOverItSensetivityFinder {
    class MainClass {
        [StructLayout(LayoutKind.Explicit)]
        struct Double2ulong {
            [FieldOffset(0)]
            public double d;
            [FieldOffset(0)]
            public ulong ul;
        }

        static string hexValue(long l) {
            byte[] bytes = BitConverter.GetBytes(l);
            StringBuilder hex = new StringBuilder(bytes.Length * 2);
            foreach (byte b in bytes)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString().ToUpper();
        }

        static void Main(string[] args) {
            const double min = 0.1d;
            const double max = 2.4d;

            long gameSensetivityRegistry = (long)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Bennett Foddy\\Getting Over It", "MouseSensitivity_h2921938665", null);
            Console.WriteLine("Hex Value:            0x" + hexValue(gameSensetivityRegistry));
            Double2ulong d2ul = new Double2ulong();
            d2ul.ul = (ulong)gameSensetivityRegistry;
            double gamePrecent = d2ul.d;
            double doublePrecent = (gamePrecent - min) / (max - min);
            double precent = (int)(doublePrecent * 10000) / 100d;
            Console.WriteLine("Game Sensetivity:     " + precent + "%");
            Console.Write("Enter your mouse DPI: ");
            try {
                int dpi = int.Parse(Console.ReadLine());
                int edpi = (int)(dpi * gamePrecent);
                Console.WriteLine("eDIP:                 " + edpi);
            } catch {
                Console.WriteLine("Invalid DPI!");
            }
            Console.WriteLine();
            Console.WriteLine("Press any key to continue . . .");
            Console.ReadKey();
        }
    }
}
