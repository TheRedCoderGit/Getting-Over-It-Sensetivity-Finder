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

        static void Main(string[] args) {
            const double min = 0.1d;
            const double max = 2.4d;

            long gameSensetivityRegistry = (long)Registry.GetValue("HKEY_CURRENT_USER\\Software\\Bennett Foddy\\Getting Over It", "MouseSensitivity_h2921938665", null);

            byte[] temp = BitConverter.GetBytes(gameSensetivityRegistry);
            Array.Reverse(temp);
            long reversed = BitConverter.ToInt64(temp, 0);
            string hex = string.Format("{0:X}", reversed);
            while (hex.Length < 16) hex = "0" + hex;

            Console.WriteLine("Hex Value:            0x" + hex);
            Double2ulong d2ul = new Double2ulong();
            d2ul.ul = (ulong)gameSensetivityRegistry;
            double gamePrecent = d2ul.d;
            Console.WriteLine("Double Value:         " + gamePrecent);
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
