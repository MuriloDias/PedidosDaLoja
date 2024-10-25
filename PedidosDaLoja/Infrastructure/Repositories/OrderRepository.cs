using Microsoft.EntityFrameworkCore;
using PedidosDaLoja.Domain.Entities;
using PedidosDaLoja.Infrastructure.Interfaces;
using PedidosDaLoja.Infrastructure.Persistence;

namespace PedidosDaLoja.Infrastructure.Repositories
{
    /// <summary>
    /// Repositório para gerenciar pedidos.
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Order> GetOrderById(int id)
        {
            return await _context.Orders
                     .Include(o => o.Products)
                     .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<Order> CreateOrder()
        {
            var order = new Order { OrderDate = DateTime.Now };
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return order;
        }

        public async Task UpdateOrder(Order order)
        {
            _context.Orders.Update(order);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> GetAllOrders(bool? status, int pageNumber, int pageSize)
        {
            IQueryable<Order> query = _context.Orders.Include(o => o.Products);

            // Aplica o filtro de status, se informado
            if (status.HasValue)
            {
                    query = status.Value
                        ? query.Where(o => o.CompletedAt == null)
                        : query.Where(o => o.CompletedAt != null);
            }

            return await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<int> GetTotalOrdersCount(bool? status)
        {
            IQueryable<Order> query = _context.Orders;

            // Aplica o filtro de status, se informado
            if (status.HasValue)
            {
                    query = status.Value
                        ? query.Where(o => o.CompletedAt == null)
                        : query.Where(o => o.CompletedAt != null);
            }

            return await query.CountAsync();
        }


    }
}
