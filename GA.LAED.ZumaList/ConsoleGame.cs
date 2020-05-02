using System;
using System.Threading;

namespace GA.LAED.ZumaList {
    public static class ConsoleGame {

        public static void PrintCenter(string text) {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.Write(text);
        }

        public static void PrintCenterLine(string text) {
            Console.SetCursorPosition((Console.WindowWidth - text.Length) / 2, Console.CursorTop);
            Console.WriteLine(text);
        }

        public static void ChangeFontColor(ConsoleColor color) {
            Console.ForegroundColor = color;
        }

        public static void ResetColor() {
            Console.ResetColor();
        }

        public static void Wait(int time) {
            Thread.Sleep(time);
        }

        public static void NewLine() {
            Console.WriteLine();
        }

        public static void Exit() {
            Environment.Exit(0);
        }
    }
}
