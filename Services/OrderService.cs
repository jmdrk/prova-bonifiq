using ProvaPub.Interfaces;
using ProvaPub.Models;

namespace ProvaPub.Services
{
	public class OrderService
    {
        private readonly Dictionary<string, IPayment> _paymentStrategies;

        public OrderService(Dictionary<string, IPayment> paymentStrategies)
        {
            _paymentStrategies = paymentStrategies;
        }
        public async Task<Order> PayOrder(string paymentMethod, decimal paymentValue, int customerId)
        {
            if (!_paymentStrategies.TryGetValue(paymentMethod, out var paymentStrategy))
            {
                throw new ArgumentException("Método de pagamento não permitido.");
            }

            return await paymentStrategy.ProcessPayment(paymentValue, customerId);
        }
    }
    public class PixPayment : IPayment
    {
        public async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order() 
            { 
                Value = paymentValue 
            });
        }
    }
    public class CreditCardPayment : IPayment
    {
        public async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order() 
            { 
                Value = paymentValue 
            });
        }
    }

    public class PayPalPayment : IPayment
    {
        public async Task<Order> ProcessPayment(decimal paymentValue, int customerId)
        {
            return await Task.FromResult(new Order() 
            {
                Value = paymentValue 
            });
        }
    }
}
