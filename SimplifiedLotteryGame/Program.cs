namespace SimplifiedLotteryGame
{
    internal class Program
    {
        static void Main(string[] args)
        {
          
            // Create and run the lottery game
            var lotteryGame = new LotteryGame();
            lotteryGame.InitializePlayers();
            lotteryGame.PlayLottery();
        }
    }
}
