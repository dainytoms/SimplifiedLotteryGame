using SimplifiedLotteryGame.Factories;
using SimplifiedLotteryGame.Interfaces;
using SimplifiedLotteryGame.Models;


namespace SLGUnitTest
{
    public class PlayerFactoryTests
    {
        [Theory(DisplayName = "PlayerFactory => Valid Player Creation Cases")]
        [InlineData("Human", "Player 1", 10, typeof(HumanPlayer), 10)]
        [InlineData("CPU", "CPU Player", 20, typeof(CPUPlayer), 20)]
        [InlineData("Human", "Player 3", 0, typeof(HumanPlayer), 0)]
        [InlineData("CPU", "CPU Player 2", 5, typeof(CPUPlayer), 5)]
        public void CreatePlayer_ValidPlayerCreation_ReturnsCorrectPlayer(
            string playerType, string playerName, int balance, Type expectedPlayerType, int expectedBalance)
        {
            // Act
            IPlayer player = PlayerFactory.CreatePlayer(playerType, playerName, balance);

            // Assert
            Assert.IsType(expectedPlayerType, player);
            Assert.Equal(playerName, player.Name);
            Assert.Equal(expectedBalance, player.DigitalBalance);
        }

        [Theory(DisplayName = "PlayerFactory => Invalid Player Type Cases")]
        [InlineData(null, "Player 1", 10)] // null type
        [InlineData("", "Player 2", 20)]   // empty string as type
        [InlineData("InvalidType", "Player 3", 10)] // invalid type
        public void CreatePlayer_InvalidPlayerType_ThrowsArgumentException(
            string playerType, string playerName, int balance)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => PlayerFactory.CreatePlayer(playerType, playerName, balance));
        }

        [Theory(DisplayName = "PlayerFactory => Negative Balance Throws Exception")]
        [InlineData("Human", "Player 1", -10)]
        [InlineData("CPU", "CPU Player", -20)]
        public void CreatePlayer_NegativeBalance_ThrowsArgumentException(
            string playerType, string playerName, int balance)
        {
            // Act & Assert
            Assert.Throws<ArgumentException>(() => PlayerFactory.CreatePlayer(playerType, playerName, balance));
        }
    }
}
