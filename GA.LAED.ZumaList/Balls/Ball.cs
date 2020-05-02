using GA.LAED.ZumaList.Values;
using System;

namespace GA.LAED.ZumaList.Balls {

    public class Ball {
        public int Id { get; private set; }
        public Color Color { get; private set; }

        public Ball(int id) {
            this.Color = (Color)new Random().Next(0, 4);
            this.Id = id;
        }

        public override string ToString() {
            return Text.BallText.BALL;
        }

        public void Print() {
            ConsoleGame.ChangeFontColor(this.Color.GetConsoleColor());
            Console.Write($"{this.ToString(),-3}");
            ConsoleGame.ResetColor();
        }
    }
}
