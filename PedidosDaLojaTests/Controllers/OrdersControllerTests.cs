using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using PedidosDaLoja.Controllers;
using PedidosDaLoja.Domain.Entities;
using PedidosDaLoja.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PedidosDaLoja.Controllers.Tests
{
    /// <summary>
    /// Clsse Teste gerada automaticamente.
    /// </summary>
    /// <author>Murilo Dias Firmino Felipin</author>
    [TestClass()]
    public class OrdersControllerTests
    {
        //[TestMethod()]
        //public void OrdersControllerTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CreateNewOrderTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GetOrderByIdTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void AddProductToOrderTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void RemoveProductFromOrderTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void CloseOrderTest()
        //{
        //    Assert.Fail();
        //}

        //[TestMethod()]
        //public void GetAllOrdersTest()
        //{
        //    Assert.Fail();
        //}

        [TestMethod()]
        public async void RemoveProductFromOrder_RetornosNaoEncontradosQuandoPedidoInexistente()
        {
            // Arrange
            var mockRepo = new Mock<IOrderRepository>();
            int orderId = 1, productId = 1;

            // Configura o repositório para retornar null (pedido não encontrado)
            mockRepo.Setup(repo => repo.GetOrderById(orderId))
                    .ReturnsAsync((Order)null);

            var controller = new OrdersController(mockRepo.Object);

            // Act
            var result = await controller.RemoveProductFromOrder(orderId, productId);

            // Assert
            //var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.IsInstanceOfType(result, typeof(NotFound));
            //Assert.Equal("Erro! Pedido não encontrado.", notFoundResult.Value);
        }
    }
}