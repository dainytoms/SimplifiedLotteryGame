using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;


namespace SimplifiedLotteryGame.Factories
{
    public class PlayerFactory
    {
        public static IPlayer CreatePlayer(string type, string name, int balance)
        {
            switch (type)
            {
                case "Human":
                    return new HumanPlayer(name, balance);
                case "CPU":
                    return new CPUPlayer(name, balance);
                default:
                    throw new ArgumentException("Invalid player type.");
            }
        }
    }
}
