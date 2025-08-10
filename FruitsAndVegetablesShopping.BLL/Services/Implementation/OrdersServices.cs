using Microsoft.EntityFrameworkCore.Design;
using Microsoft.IdentityModel.Tokens;
using MVC_Group7_demo_BLL.ModelVM;
using MVC_Group7_demo_BLL.Services.Abstraction;
using MVC_Group7_demo_DAL.Entities;
using MVC_Group7_demo_DAL.Repository.Abstraction;
using MVC_Group7_demo_DAL.Repository.Implementation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_Group7_demo_BLL.Services.Implementation
{
    public class OrdersServices : IOrdersServices
    {
        private readonly IOrdersRepo ordersRepo;
        private readonly ICustomerRepo customerRepo;
        private readonly IProductOrderRepo productOrderRepo;
        private readonly IProductRepo productRepo;

        public OrdersServices(IOrdersRepo ordersRepo, ICustomerRepo customerRepo, IProductRepo productRepo, IProductOrderRepo productOrderRepo)
        {
            this.ordersRepo = ordersRepo;
            this.customerRepo = customerRepo;
            this.productOrderRepo = productOrderRepo;
            this.productRepo = productRepo;
        }

        public async Task<(bool, string?)> AddProductOrderAsync(AddProductToOrderDTO readProduct, string customerId)
        {
            
            var customerResult = await customerRepo.GetById(customerId);
            if (customerResult.Item1 == null)
                return (false, "Customer not found");

            string createdBy = customerResult.Item1.Name;

            var currentOrder = customerResult.Item1.Orders.OrderByDescending(o => o.CreatedOn).FirstOrDefault(o => o.isDeleted == false);

            
            if (currentOrder == null)
            {
                Console.WriteLine("In Order Service : " + customerId);
                currentOrder = new Orders(0, 0, customerId, createdBy ,null, new List<ProductOrder>());

                var createRes = await ordersRepo.CreateAsync(currentOrder);
                if (!createRes.Item1)
                    return (false, "Failed to create pending order");

                customerResult.Item1.Orders.Add(currentOrder);
            }

            
            //Console.WriteLine($"Looking for ProductId: {readProduct.ProductId}");
            var productRes = await productRepo.GetByIdAsync(readProduct.ProductId);
            if (productRes.Item1 == null)
                return (false, "Product not found");

            var product = productRes.Item1;

            
            var existingPO = currentOrder.ProductOrders.FirstOrDefault(po => po.Productid == product.ProductId);

            if (existingPO == null)
            {
                
                var newPO = new ProductOrder(
                    currentOrder.Order_id,
                    product.ProductId,
                    readProduct.Quantity,
                    createdBy
                );
                await productOrderRepo.Create(newPO);

                currentOrder = (await ordersRepo.GetByIdAsync(currentOrder.Order_id)).Item1;

            } else if (existingPO.isDeleted)
            {
                var ReactRes = await productOrderRepo.Reactivate(existingPO.ProductOrderId);
                var ReactResUpdate = await productOrderRepo.Update(existingPO.ProductOrderId, readProduct.Quantity, createdBy);
            }
            else
            {


                var res = await productOrderRepo.Update(existingPO.ProductOrderId, existingPO.Quantity + readProduct.Quantity, createdBy);
            }


            
            int debugTotalItems = 0;
            double debugTotalPrice = 0;

            foreach (var po in currentOrder.ProductOrders)
            {
                Console.WriteLine($"PO ID: {po.ProductOrderId}, Product ID: {po.Productid}, Quantity: {po.Quantity}");

                if (po.Product != null)
                {
                    Console.WriteLine($"Product Name: {po.Product.Name}, Price: {po.Product.Price}");
                    debugTotalPrice += po.Quantity * po.Product.Price;
                }
                else
                {
                    Console.WriteLine($" Product with ID {po.Productid} not loaded");
                }

                debugTotalItems += po.Quantity;
            }

            Console.WriteLine($"[DEBUG] TOTAL ITEMS: {debugTotalItems}");
            Console.WriteLine($"[DEBUG] TOTAL PRICE: {debugTotalPrice}");

            await ordersRepo.EditAsync(currentOrder.Order_id, debugTotalPrice, debugTotalItems);

            return (true, null);
        }


        public async Task<(bool, string?)> ApplyChanges(OrderEditDTO orderEditDTO, string customerId)
        {
            var customerResult = await customerRepo.GetById(customerId);
            if (customerResult.Item1 == null)
            {
                Console.WriteLine("customer id bayez");
                return (false, "Customer not found");
            }
            string createdBy = customerResult.Item1.Name;

            /*
            var res = await ordersRepo.GetByIdAsync(orderEditDTO.OrderId);
            var order = res.Item1;

            if (order == null)
            {
                return (false, "Order ID does not exist");
            }
            */
            Console.WriteLine("wslna awel if");
            if (orderEditDTO.ProductOrders.All(po => po.Remove))
            {
                Console.WriteLine("d5lna el if");
                var resDelete = await ordersRepo.DeleteAsync(orderEditDTO.OrderId);
                return (true, null);
            }

            Console.WriteLine("e7na abl el loop");
            foreach (var po in orderEditDTO.ProductOrders)
            {
                if (po.Remove)
                {
                    var deleteResult = await productOrderRepo.Delete(po.ProductOrderId, createdBy);
                    if (!deleteResult.Item1)
                    {
                        Console.WriteLine("hena aho 1");
                        return (false, $"Failed to delete ProductOrder ID {po.ProductOrderId}");
                    }
                }
                else
                {
                    var updateResult = await productOrderRepo.Update(po.ProductOrderId, po.Quantity, createdBy);
                    if (!updateResult.Item1)
                    {
                        Console.WriteLine("hena aho 2");
                        return (false, $"Failed to update ProductOrder ID {po.ProductOrderId}");
                    }
                }
            }
            Console.WriteLine("5lst el loop");
            int numOfItems = orderEditDTO.ProductOrders.Where(po=>po.Remove == false).Sum(po=>po.Quantity);
            double totalPrice = orderEditDTO.ProductOrders.Where(po => po.Remove == false).Sum(po => po.Quantity * po.UnitPrice);

            var finalRes = await ordersRepo.EditAsync(orderEditDTO.OrderId, totalPrice, numOfItems);

            return (true, null);

        }


        public async Task<(OrdersDTO?, string?)> GetCurrentOrder(string customerId)
        {
            

            var customerResult = await customerRepo.GetById(customerId);
            if (customerResult.Item1 == null)
                return (null, "Customer not found");

            string createdBy = customerResult.Item1.Name;

            var currentOrder = customerResult.Item1.Orders.OrderByDescending(o => o.CreatedOn).FirstOrDefault(o => o.isDeleted == false);

            if (currentOrder == null)
                return (null, "No active order found");

            
            



            List<ProductOrderDTO> productOrderDTOs = new List<ProductOrderDTO>();

            foreach(var po in currentOrder.ProductOrders)
            {
                if (!po.isDeleted)
                {
                    productOrderDTOs.Add(new ProductOrderDTO()
                    {
                        ProductOrderId = po.ProductOrderId,
                        ProductId = po.Productid,
                        Quantity = po.Quantity,
                        ProductName = po.Product.Name,
                        UnitPrice = po.Product.Price,
                        ProductImage = po.Product.Image,
                        Stock = po.Product.Stock
                    });
                }
            }

            OrdersDTO ordersDTO = new OrdersDTO
            {
                OrderId = currentOrder.Order_id,
                TotalPrice = currentOrder.totalPrice,
                NumOfItems = currentOrder.numOfItems,
                CustomerId = customerId,
                DeliveryId = currentOrder.Delivery_id,
                ProductOrders = productOrderDTOs
            };

            return (ordersDTO, null);
        }



        public async Task<(bool, string?)> finalizeOrderAsync(OrdersDTO ordersDTO, string customerId)
        {
            try
            {
                var customerResult = await customerRepo.GetById(customerId);
                if (customerResult.Item1 == null)
                    return (false, "Customer not found");

                string createdBy = customerResult.Item1.Name;

                var currentOrder = customerResult.Item1.Orders.OrderByDescending(o => o.CreatedOn).FirstOrDefault(o => o.isDeleted == false);

                if (currentOrder == null)
                    return (false, "No active order found");

                foreach (var po in currentOrder.ProductOrders)
                {
                    if (!(await productRepo.CheckStockAsync(po.Productid, po.Quantity)).Item1)
                    {
                        return (false, "Item has not enough stock!!!");
                    }
                }

                foreach (var po in currentOrder.ProductOrders)
                {
                    await productRepo.UpdateStockAsync(po.Productid, po.Product.Stock - po.Quantity, createdBy);
                }

                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }


        public async Task<(bool, string?)> addProductOrderAsync(ProductOrderDTO productOrderDTO, OrdersDTO ordersDTO)
        {
            var res = await customerRepo.GetById(ordersDTO.CustomerId);
            if (res.Item1 == null)
                return (false, "Customer not found");

            string createdBy = res.Item1.Name;

            var res1 = await ordersRepo.GetByIdAsync(ordersDTO.OrderId);
            var res2 = await productRepo.GetByIdAsync(productOrderDTO.ProductId);
            var res3 = await productOrderRepo.GetById(productOrderDTO.ProductOrderId);

            if (res3.Item1 == null)
            {
                var newPO = new ProductOrder(ordersDTO.OrderId, productOrderDTO.ProductId, productOrderDTO.Quantity, createdBy);
                await productOrderRepo.Create(newPO);
            }
            else
            {
                await productOrderRepo.Update(productOrderDTO.ProductOrderId, res3.Item1.Quantity + productOrderDTO.Quantity, createdBy);
                await ordersRepo.EditAsync(
                    ordersDTO.OrderId,
                    ordersDTO.TotalPrice + (res2.Item1.Price * productOrderDTO.Quantity),
                    ordersDTO.NumOfItems + res3.Item1.Quantity + productOrderDTO.Quantity
                );
            }

            return (true, null);
        }

        

        public async Task<(bool, string?)> deleteProductOrderAsync(ProductOrderDTO productOrderDTO, OrdersDTO ordersDTO)
        {
            var res = await customerRepo.GetById(ordersDTO.CustomerId);
            if (res.Item1 == null)
                return (false, "Customer not found");

            string deletedBy = res.Item1.Name;
            var res1 = await productOrderRepo.Delete(productOrderDTO.ProductOrderId, deletedBy);

            return res1;
        }

        public async Task<(bool, string)> CreateAsync(OrdersDTO ordersDTO)
        {
            try
            {
                var res1 = await customerRepo.GetById(ordersDTO.CustomerId);
                if (res1.Item1 == null)
                    return (false, "Customer not found");

                string createdBy = res1.Item1.Name;
                List<ProductOrder> productOrders = new List<ProductOrder>();

                foreach (var po in ordersDTO.ProductOrders)
                {
                    productOrders.Add(new ProductOrder(
                        ordersDTO.OrderId,
                        po.ProductId,
                        po.Quantity,
                        createdBy
                    ));
                }

                var order = new Orders(
                    ordersDTO.TotalPrice,
                    ordersDTO.NumOfItems,
                    ordersDTO.CustomerId,
                    createdBy,
                    ordersDTO.DeliveryId,
                    productOrders
                );

                var res2 = await ordersRepo.CreateAsync(order);
                return (true, "Order created successfully");
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string?)> DeleteAsync(int id)
        {
            try
            {
                var res1 = await ordersRepo.DeleteAsync(id);
                return res1;
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(bool, string?)> EditAsync(OrdersDTO orderDTO)
        {
            try
            {
                var res = await ordersRepo.EditAsync(orderDTO.OrderId, orderDTO.TotalPrice, orderDTO.NumOfItems);
                return (true, null);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

        public async Task<(List<Orders>?, string?)> GetAllAsync()
        {
            try
            {
                var res = await ordersRepo.GetAllAsync();
                if (res.Item1 == null || res.Item1.Count == 0)
                {
                    return (new List<Orders>(), "Empty");
                }

                return res;
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(Orders?, string?)> GetByIdAsync(int id)
        {
            try
            {
                var res = await ordersRepo.GetByIdAsync(id);
                if (res.Item1 == null)
                {
                    return (null, "Services : Id not found");
                }

                return res;
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
