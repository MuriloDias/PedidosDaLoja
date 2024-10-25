using Microsoft.AspNetCore.Mvc;
using PedidosDaLoja.Domain.Entities;
using PedidosDaLoja.Infrastructure.Interfaces;

namespace PedidosDaLoja.Controllers
{
    /// <summary>
    /// Clsse Controller que recebe as rotas, faz o controle de regras e invoca operações com o DB
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    [ApiController]
    [Route("api/[controller]")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        /// <summary>
        /// Inicia um novo pedido.
        /// </summary>
        [HttpPost("new-order")]
        public async Task<ActionResult<Order>> CreateNewOrder()
        {
            var order = await _orderRepository.CreateOrder();
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        /// <summary>
        /// Obtém um pedido pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<Order>> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
                return NotFound("Erro! Pedido não encontrado.");

            return Ok(order);
        }

        /// <summary>
        /// Adiciona um produto a um pedido.
        /// </summary>
        [HttpPost("{orderId}/add-product")]
        public async Task<IActionResult> AddProductToOrder(int orderId, [FromBody] Product product)
        {
            if (product == null || string.IsNullOrWhiteSpace(product.Name) || product.Price <= 0)
                return BadRequest("Erro! Dados do produto inválidos.");

            var order = await _orderRepository.GetOrderById(orderId);

            if (order == null)
                return NotFound("Erro! Pedido não encontrado.");

            try
            {
                order.AddProduct(product);
                await _orderRepository.UpdateOrder(order);

                return CreatedAtAction(nameof(GetOrderById), new { id = orderId }, order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest($"Erro! Erro ao adicionar produto: {ex.Message}");
            }
        }

        /// <summary>
        /// Remove produto do pedido
        /// </summary>
        [HttpDelete("{orderId}/remove-product/{productId}")]
        public async Task<IActionResult> RemoveProductFromOrder(int orderId, int productId)
        {
            var order = await _orderRepository.GetOrderById(orderId);
            if (order == null)
                return NotFound("Erro! Pedido não encontrado.");

            var product = order.Products.FirstOrDefault(p => p.Id == productId);
            if (product == null)
                return NotFound("Erro! Produto não encontrado no pedido.");

            try
            {
                order.RemoveProduct(product);
                await _orderRepository.UpdateOrder(order);
                return Ok(order);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Encerra pedido
        /// </summary>
        [HttpPost("{id}/close")]
        public async Task<IActionResult> CloseOrder(int id)
        {
            var order = await _orderRepository.GetOrderById(id);
            if (order == null)
                return NotFound("Erro! Pedido não encontrado.");

            if (order.CompletedAt != null)
                return BadRequest("Erro! O pedido já está fechado.");

            if (!order.CanBeClosed())
                return BadRequest("Erro! O pedido não pode ser fechado porque não contém produtos.");

            order.CompletedAt = DateTime.Now;

            await _orderRepository.UpdateOrder(order);

            return Ok(order);
        }

        /// <summary>
        /// Consulta todos os pedidos.
        /// </summary>
        [HttpGet("get-all-orders")]
        public async Task<ActionResult<IEnumerable<Order>>> GetAllOrders([FromQuery] bool? status = null, int pageNumber = 1, int pageSize = 10)
        {
            var order = await _orderRepository.GetAllOrders(status, pageNumber, pageSize);
            var totalCount = await _orderRepository.GetTotalOrdersCount(status);

            return Ok(new
            {
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize,
                Orders = order
            });
        }

    }
}
