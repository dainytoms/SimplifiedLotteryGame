using SimplifiedLotteryGame.Interfaces;


namespace SimplifiedLotteryGame.Models
{
    public class HumanPlayer : IPlayer
    {
        public string Name { get; set; }
        public decimal DigitalBalance { get; set; }
        public int Tickets { get; set; }
        public List<int> TicketNumbers { get; set; } = new List<int>();

        public HumanPlayer(string name, decimal initialBalance)
        {
            Name = name;
            DigitalBalance = initialBalance;
        }

        public void PurchaseTickets()
        {
            Console.Write($"Welcome to the Bede Lottery, {Name},! \n * Your Digital Balance: {String.Format("{0:C}", DigitalBalance)} \n * Ticket Price: {String.Format("{0:C}", 1)} each \n How many tickets do you want to buy, {Name}? ");
            int userTickets = int.Parse(Console.ReadLine());
            Tickets = (int)Math.Min(userTickets, DigitalBalance);
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
