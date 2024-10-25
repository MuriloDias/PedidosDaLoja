using PedidosDaLoja.Domain.Entities;

namespace PedidosDaLoja.Infrastructure.Interfaces
{
    /// <summary>
    /// Clsse interface que é implementada pela OrderRepository, obrigando a classe que a implementa a ter todos os metodos desta
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    public interface IOrderRepository
    {
        Task<Order> CreateOrder();
        Task<List<Order>> GetAllOrders(bool? status, int pageNumber, int pageSize);
        Task<Order> GetOrderById(int id);
        Task UpdateOrder(Order order);
        Task<int> GetTotalOrdersCount(bool? status);
    }
}
