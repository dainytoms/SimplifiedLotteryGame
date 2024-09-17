

namespace SimplifiedLotteryGame.Interfaces
{
    public interface IPlayer
    {
        string Name { get; set; }
        decimal DigitalBalance { get; set; }
        int Tickets { get; set; }
        List<int> TicketNumbers { get; set; }

        void PurchaseTickets();
        void AddTickets(int ticketCount, ref int ticketCounter);
    }
}
