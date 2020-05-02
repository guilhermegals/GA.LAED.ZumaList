using System;

namespace GA.LAED.ZumaList.Balls {
    public enum Color {
        Green = 0,
        Blue = 1,
        Red = 2,
        Yellow = 3
    }

    public static class ColorConfiguration {
        
        public static ConsoleColor GetConsoleColor(this Color color) {
            switch (color) {
                case Color.Green: return ConsoleColor.Green;
                case Color.Blue: return ConsoleColor.Blue;
                case Color.Red: return ConsoleColor.Red;
                case Color.Yellow: return ConsoleColor.Yellow;
                default: return ConsoleColor.White;
            }
        }
    }
}
