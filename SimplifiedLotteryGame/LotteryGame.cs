using SimplifiedLotteryGame.Factories;
using SimplifiedLotteryGame.Interfaces;


namespace SimplifiedLotteryGame
{
    public class LotteryGame
    {
        private const int TicketCost = 1;
        private readonly int minPlayers;
        private readonly int maxPlayers;
        private readonly Random random = new Random();
        private readonly List<IPlayer> players = new List<IPlayer>();
        private readonly List<int> soldTickets = new List<int>();
        private int totalRevenue = 0;

        public LotteryGame(int minPlayers = 10, int maxPlayers = 15)
        {
            this.minPlayers = minPlayers;
            this.maxPlayers = maxPlayers;
        }

        public void InitializePlayers()
        {
            // Create Human Player (Player 1)
            IPlayer humanPlayer = PlayerFactory.CreatePlayer("Human", "Player 1", 10);
            humanPlayer.PurchaseTickets();
            AddPlayer(humanPlayer);

            // Create CPU Players
            int numPlayers = random.Next(minPlayers, maxPlayers + 1);
            for (int i = 2; i <= numPlayers; i++)
            {
                IPlayer cpuPlayer = PlayerFactory.CreatePlayer("CPU", $"Player {i}", 10);
                cpuPlayer.PurchaseTickets();
                AddPlayer(cpuPlayer);
            }
        }

        private void AddPlayer(IPlayer player)
        {
            int ticketCounter = soldTickets.Count + 1;
            player.AddTickets(player.Tickets, ref ticketCounter);
            players.Add(player);
            soldTickets.AddRange(player.TicketNumbers);
            totalRevenue += player.Tickets * TicketCost;
        }

        public void PlayLottery()
        {
            if (soldTickets.Count == 0) return;

            int totalTickets = soldTickets.Count;
            int grandPrize = totalRevenue / 2;
            int secondTierPrize = (int)(totalRevenue * 0.3);
            int thirdTierPrize = (int)(totalRevenue * 0.1);

            List<int> grandWinner = DrawWinners(1);
            List<int> secondTierWinners = DrawWinners((int)Math.Round(totalTickets * 0.1), grandWinner);
            List<int> thirdTierWinners = DrawWinners((int)Math.Round(totalTickets * 0.2), grandWinner.Concat(secondTierWinners).ToList());

            int secondPrizeAmount = secondTierPrize / secondTierWinners.Count;
            int thirdPrizeAmount = thirdTierPrize / thirdTierWinners.Count;

            Console.WriteLine("Lottery Results:");
            foreach (var player in players)
            {
                Console.WriteLine($"{player.Name} bought {player.Tickets} ticket(s).");
            }

            Console.WriteLine($"\nGrand Prize: {grandPrize}");
            Console.WriteLine($"Second Tier Prize: {secondPrizeAmount} per winner");
            Console.WriteLine($"Third Tier Prize: {thirdPrizeAmount} per winner");

            Console.WriteLine("\nWinners:");
            PrintWinners(grandWinner, "Grand Prize", grandPrize);
            PrintWinners(secondTierWinners, "Second Tier", secondPrizeAmount);
            PrintWinners(thirdTierWinners, "Third Tier", thirdPrizeAmount);

            int houseProfit = totalRevenue - (grandPrize + secondTierPrize + thirdTierPrize);
            Console.WriteLine($"\nTotal House Profit: {houseProfit}");
        }

        private List<int> DrawWinners(int count, List<int> exclude = null)
        {
            exclude ??= new List<int>();
            var eligibleTickets = soldTickets.Except(exclude).ToList();
            var winners = new List<int>();

            for (int i = 0; i < count && eligibleTickets.Count > 0; i++)
            {
                int winnerIndex = random.Next(eligibleTickets.Count);
                winners.Add(eligibleTickets[winnerIndex]);
                eligibleTickets.RemoveAt(winnerIndex);
            }

            return winners;
        }

        private void PrintWinners(List<int> winnerTickets, string prizeTier, int prizeAmount)
        {
            foreach (var ticket in winnerTickets)
            {
                var winningPlayer = players.FirstOrDefault(p => p.TicketNumbers.Contains(ticket));
                if (winningPlayer != null)
                {
                    Console.WriteLine($"{winningPlayer.Name} won {prizeTier} with Ticket #{ticket} and received ${prizeAmount}.");
                }
            }
        }
    }
}
