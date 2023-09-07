using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsStore.Data
{
    public interface IOrderRepository
    {
        IList<Order> GetOrders();

        void SaveOrder(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _dbContext;
        public OrderRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IList<Order> GetOrders()
        {
            var orders = _dbContext.Orders
                .Include(o => o.CartItems)
                .ThenInclude(c => c.Product)
                .ToList();

            return orders;
        }

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.CartItems.Select(c => c.Product));

            if(order.Id == 0)
            {
                _dbContext.Orders.Add(order);
            }

            _dbContext.SaveChanges();
        }
    }
}
