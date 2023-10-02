using ProvaPub.Models;

namespace ProvaPub.Interfaces
{
    public interface IPayment
    {
        Task<Order> ProcessPayment(decimal paymentValue, int customerId);
    }
}
