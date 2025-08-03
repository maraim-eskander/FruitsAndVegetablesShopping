using FruitsAndVegetablesShopping.BLL.ModelVm.Category;
using FruitsAndVegetablesShopping.BLL.Services.Abstraction;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;

namespace FruitsAndVegetablesShopping.BLL.Services.Implementation
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


        public async Task<(bool, string?)>addProductOrderAsync(ProductOrderDTO productOrderDTO, OrdersDTO ordersDTO)
        {

            var res = customerRepo.GetById(ordersDTO.CustomerId);
            if (res.Item1 == null)
                return (false, "Customer not found");

            string createdBy = res.Item1.Name;

            var res1 = await ordersRepo.GetByIdAsync(ordersDTO.OrderId);


            var res2 =  productRepo.GetById(productOrderDTO.ProductId);

            var res3 = productOrderRepo.GetById(productOrderDTO.ProductOrderId);

            if(res3.Item1 == null)
            {
                var newPO = new ProductOrder(ordersDTO.OrderId, productOrderDTO.ProductId, productOrderDTO.Quantity, createdBy);
                res1.Item1.ProductOrders.Add(newPO);
            }
            else
            {
                productOrderRepo.Update(productOrderDTO.ProductOrderId, res3.Item1.Quantity + productOrderDTO.Quantity, createdBy);
            }

            await ordersRepo.save();

            return (true, null);

        }

        public async Task<(bool, string?)> deleteProductOrderAsync(ProductOrderDTO productOrderDTO, OrdersDTO ordersDTO)
        {
            var res = customerRepo.GetById(ordersDTO.CustomerId);
            if (res.Item1 == null)
                return (false, "Customer not found");

            string deletedBy = res.Item1.Name;



            var res1 = productOrderRepo.Delete(productOrderDTO.ProductOrderId, deletedBy);

            return res1;
        }

        



        public async Task<(bool, string)> CreateAsync(OrdersDTO ordersDTO)
        {
            
            try
            {
                var res1 = customerRepo.GetById(ordersDTO.CustomerId);
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
            }catch(Exception ex)
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
            }catch(Exception ex)
            {
                return (false, ex.Message);
            }


        }

        public async Task<(List<Orders>?, string?)> GetAllAsync()
        {
            try
            {
                var res = await ordersRepo.GetAllAsync();

                if(res.Item1 == null || res.Item1.Count == 0)
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
            }catch(Exception ex)
            {
                return (null, ex.Message);
            }
        }
    }
}
