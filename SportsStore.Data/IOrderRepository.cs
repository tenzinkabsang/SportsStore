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
        Task<IList<Order>> GetOrdersAsync();

        Task<Order> GetByIdAsync(int orderId);

        Task SaveOrderAsync(Order order);

        void SaveOrder(Order order);
    }

    public class OrderRepository : IOrderRepository
    {
        private readonly StoreDbContext _dbContext;
        public OrderRepository(StoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Order> GetByIdAsync(int orderId)
        {
            Order? order = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .FirstOrDefaultAsync(o => o.Id == orderId);

            if(order == null)
                throw new Exception($"Order with id={orderId} not found.");

            return order;
        }


        public async Task<IList<Order>> GetOrdersAsync()
        {
            var orders = await _dbContext.Orders
                .Include(o => o.OrderItems)
                .ThenInclude(c => c.Product)
                .ToListAsync();

            return orders;
        }

        public void SaveOrder(Order order)
        {
            _dbContext.AttachRange(order.OrderItems.Select(c => c.Product));

            if (order.Id == 0)
            {
                _dbContext.Orders.Add(order);
            }

             _dbContext.SaveChanges();
        }

        public async Task SaveOrderAsync(Order order)
        {
            _dbContext.AttachRange(order.OrderItems.Select(c => c.Product));

            if (order.Id == 0)
            {
                _dbContext.Orders.Add(order);
            }

            //_dbContext.Entry(order).State = EntityState.Modified;

            _dbContext.Update(order);

            await _dbContext.SaveChangesAsync();
        }
    }
}
