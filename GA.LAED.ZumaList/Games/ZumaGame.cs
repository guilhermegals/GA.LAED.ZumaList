using GA.LAED.ZumaList.Balls;
using GA.LAED.ZumaList.Values;
using System;

namespace GA.LAED.ZumaList.Games {
    public class ZumaGame : Game {

        public const int MAX_LIST_BALLS = 20;
        public const int START_LIST_BALLS = 8;
        public const int MAX_PLAYER_BALLS = 3;
        public const int START_PLAYER_BALLS = 3;

        private GameState State { get; set; }
        private int Score { get; set; }

        #region [ Constructor ]

        public ZumaGame() {
            this.State = GameState.Menu;
            this.Score = 0;
        }

        #endregion

        #region [ Game ]

        protected override void GameConfiguration() {
            Console.Title = Text.GameText.TITLE;
        }

        protected override void GameLoop() {
            while (true) {
                switch (this.State) {
                    case GameState.Menu:
                        this.Menu();
                        break;

                    case GameState.InGame:
                        this.InGame();
                        break;

                    case GameState.GameOver:
                        this.GameOver();
                        break;

                    case GameState.Credits:
                        this.Credits();
                        break;

                    case GameState.Exit:
                        this.Exit();
                        break;
                }
            }
        }

        #endregion

        #region [ Game States ]

        private void Menu() {
            int option;

            do {
                Console.Clear();
                ConsoleGame.PrintCenterLine(Text.GameText.TITLE);
                ConsoleGame.PrintCenterLine(Text.GameText.LINE);
                ConsoleGame.NewLine();
                ConsoleGame.PrintCenterLine($"1 - {Text.MenuText.PLAY}");
                ConsoleGame.PrintCenterLine($"2 - {Text.MenuText.CREDITS}");
                ConsoleGame.PrintCenterLine($"0 - {Text.MenuText.EXIT}");

                ConsoleGame.NewLine();
                ConsoleGame.PrintCenterLine(Text.GameText.LINE);
                ConsoleGame.PrintCenter(Text.GameText.WRITE);
                option = Convert.ToInt32(Console.ReadLine());

                switch (option) {
                    case 0:
                        this.State = GameState.Exit;
                        break;

                    case 2:
                        this.State = GameState.Credits;
                        break;

                    case 1:
                        this.State = GameState.InGame;
                        break;
                }
            } while (option > 2 || option < 0);
        }

        private void InGame() {
            Console.Clear();

            ListBall listBall = new ListBall(MAX_LIST_BALLS, START_LIST_BALLS);
            ListBall playerBalls = new ListBall(MAX_PLAYER_BALLS, START_PLAYER_BALLS);
            this.Score = 0;
            do {
                ConsoleGame.PrintCenterLine(Text.GameText.TITLE);
                ConsoleGame.PrintCenterLine(Text.GameText.LINE);

                ConsoleGame.PrintCenter($"{this.Score} ");
                ConsoleGame.NewLine();
                ConsoleGame.PrintCenterLine(Text.GameText.LINE);

                Console.WriteLine(Text.InGameText.LIST_BALL);
                listBall.Print();
                ConsoleGame.NewLine();
                ConsoleGame.NewLine();

                Console.WriteLine(Text.InGameText.PLAYER_LIST_BALL);
                playerBalls.Print();

                ConsoleGame.NewLine();
                ConsoleGame.NewLine();
                ConsoleGame.NewLine();

                Console.Write(Text.InGameText.SELECT_BALL);
                if (!int.TryParse(Console.ReadLine(), out int ballPosition))
                    continue;

                Console.Write(Text.InGameText.SELECT_POSITION);
                if (!int.TryParse(Console.ReadLine(), out int position))
                    continue;

                Ball ball = playerBalls.RemoveAt(ballPosition);

                if (ball != null) {
                    listBall.InsertAt(ball, position);
                    int itensRemoved = listBall.RemoveGroup(position, ball.Color);
                    if (itensRemoved > 0)
                        this.Score += itensRemoved - 1;
                    listBall.AddNewBall();
                    playerBalls.AddNewBall();
                }

                Console.Clear();

            } while (!listBall.Empty() && !listBall.Full());

            if (listBall.Full())
                this.State = GameState.GameOver;
        }

        private void GameOver() {
            Console.Clear();
            ConsoleGame.PrintCenterLine(Text.GameText.TITLE);
            ConsoleGame.PrintCenterLine(Text.GameText.LINE);
            ConsoleGame.NewLine();

            ConsoleGame.PrintCenterLine(Text.GameOverText.GAME_OVER);
            ConsoleGame.PrintCenter($"{Text.GameOverText.FINAL_SCORE} {this.Score}");

            ConsoleGame.NewLine();
            ConsoleGame.PrintCenterLine(Text.GameText.LINE);
            ConsoleGame.PrintCenter(Text.GameText.ENTER);
            Console.ReadKey();
            this.State = GameState.Menu;
        }

        private void Credits() {
            Console.Clear();
            ConsoleGame.PrintCenterLine(Text.GameText.TITLE);
            ConsoleGame.PrintCenterLine(Text.GameText.LINE);
            ConsoleGame.NewLine();

            ConsoleGame.PrintCenterLine(Text.GameText.CREDITS);

            ConsoleGame.NewLine();
            ConsoleGame.PrintCenterLine(Text.GameText.LINE);
            ConsoleGame.PrintCenter(Text.GameText.ENTER);
            Console.ReadKey();
            this.State = GameState.Menu;
        }

        private void Exit() {
            Console.Clear();
            ConsoleGame.NewLine();
            ConsoleGame.NewLine();
            ConsoleGame.PrintCenterLine(Text.GameText.BYE);
            ConsoleGame.NewLine();
            ConsoleGame.NewLine();
            ConsoleGame.Wait(1000);
            ConsoleGame.Exit();
        }

        #endregion
    }
}
