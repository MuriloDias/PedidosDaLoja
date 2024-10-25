using System;

namespace PedidosDaLoja.Domain.Entities
{
    /// <summary>
    /// Representação de um produto.
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
    }
}
