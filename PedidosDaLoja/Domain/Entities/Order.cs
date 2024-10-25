using System;
using System.Collections.Generic;

namespace PedidosDaLoja.Domain.Entities
{
    /// <summary>
    /// Representação de um pedido.
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    public class Order
    {
        public int Id { get; set; } 
        public List<Product> Products { get; set; } = new List<Product>();
        public DateTime OrderDate { get; set; }
        public DateTime? CompletedAt { get; set; }


        /// <summary>
        /// Verifica se o pedido pode ser fechado.
        /// </summary>
        /// <returns>True se o pedido pode ser fechado, caso contrário, False.</returns>
        public bool CanBeClosed()
        {
            return Products.Any();
        }

        /// <summary>
        /// Adiciona Produto ao pedido.
        /// </summary>
        public void AddProduct(Product product)
        {
            if (CompletedAt.HasValue)
                throw new InvalidOperationException("Erro! Pedido já está encerrado!");

            Products.Add(product);
        }

        /// <summary>
        /// Remove Produto ao pedido.
        /// </summary>
        public void RemoveProduct(Product product)
        {
            if (CompletedAt != null)
                throw new InvalidOperationException("Erro! Não é possível remover produtos de um pedido finalizado.");

            if (!Products.Remove(product))
                throw new InvalidOperationException("Erro! Produto não encontrado no pedido.");
        }
    }
}
