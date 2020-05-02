namespace GA.LAED.ZumaList {
    public abstract class Game {

        public void Start() {
            GameConfiguration();
            GameLoop();
        }

        protected abstract void GameConfiguration();
        protected abstract void GameLoop();
    }
}
