using SimplifiedLotteryGame.Interfaces;


namespace SimplifiedLotteryGame.Models
{
    public class CPUPlayer : IPlayer
    {
        public string Name { get; set; }
        public decimal DigitalBalance { get; set; }
        public int Tickets { get; set; }
        public List<int> TicketNumbers { get; set; } = new List<int>();

        private readonly Random random = new Random();

        public CPUPlayer(string name, decimal initialBalance)
        {
            Name = name;
            DigitalBalance = initialBalance;
        }

        public void PurchaseTickets()
        {
            Tickets = random.Next(1, (int)Math.Min(10, DigitalBalance) + 1);
            DigitalBalance -= Tickets;
        }

        public void AddTickets(int ticketCount, ref int ticketCounter)
        {
            for (int i = 0; i < ticketCount; i++)
            {
                TicketNumbers.Add(ticketCounter++);
            }
        }
    }
}
