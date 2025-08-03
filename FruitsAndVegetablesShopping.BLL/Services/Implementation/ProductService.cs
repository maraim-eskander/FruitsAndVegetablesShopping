using FruitsAndVegetablesShopping.BLL.ModelVm.Product;
using FruitsAndVegetablesShopping.BLL.Services.Abstraction;
using FruitsAndVegetablesShopping.DAL.Entities;
using FruitsAndVegetablesShopping.DAL.Repo.Abstraction;

namespace FruitsAndVegetablesShopping.BLL.Services.Implementation
{
    public class ProductService : IProductService
    {
        private readonly IProductRepo productRepo;

       
        public ProductService(IProductRepo productRepo)
        {
            this.productRepo = productRepo;
        }

     
        public async Task<(bool, string?)> CreateAsync(CreateProductDto dto)
        {
            try
            {
                var product = new Product(dto.Name, dto.Price, dto.Image, dto.Stock, dto.Description, dto.CategoryId, dto.CreatedBy);
                return await productRepo.CreateAsync(product);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

     
        public async Task<(List<ReadProductDto>, string?)> GetAllAsync()
        {
            try
            {
                var (products, error) = await productRepo.GetAllAsync();
                if (products == null || products.Count == 0)
                    return (new List<ReadProductDto>(), "No products found");

                var dtoList = products.Select(p => new ReadProductDto
                {
                    Name = p.Name,
                    Price = p.Price,
                    Image = p.Image,
                    Stock = p.Stock,
                    Description = p.Description,
                    CategoryName = p.Category?.Name ?? "",
                    CreatedOn = p.CreatedOn
                }).ToList();

                return (dtoList, null);
            }
            catch (Exception ex)
            {
                return (new List<ReadProductDto>(), ex.Message);
            }
        }

 
        public async Task<(ReadProductDto?, string?)> GetByIdAsync(int id)
        {
            try
            {
                var (product, error) = await productRepo.GetByIdAsync(id);
                if (product == null)
                    return (null, error);

                var dto = new ReadProductDto
                {
                    Name = product.Name,
                    Price = product.Price,
                    Image = product.Image,
                    Stock = product.Stock,
                    Description = product.Description,
                    CategoryName = product.Category?.Name ?? "",
                    CreatedOn = product.CreatedOn
                };

                return (dto, null);
            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        public async Task<(bool, string?)> UpdateAsync(int id, UpdateProductDto dto)
        {
            try
            {
                return await productRepo.UpdateAsync(id, dto.Name, dto.Price, dto.Image, dto.Stock, dto.Description, dto.CategoryId, dto.ModifiedBy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }

    
        public async Task<(bool, string?)> DeleteAsync(int id, string deletedBy)
        {
            try
            {
                return await productRepo.DeleteAsync(id, deletedBy);
            }
            catch (Exception ex)
            {
                return (false, ex.Message);
            }
        }
        public async Task<(List<ReadProductDto>, string?)> SearchByNameAsync(string name)
        {
            var (products, error) = await productRepo.SearchByNameAsync(name);
            if (products == null)
                return (new List<ReadProductDto>(), error);

            var dtoList = products.Select(p => new ReadProductDto
            {
                Name = p.Name,
                Price = p.Price,
                Image = p.Image,
                Stock = p.Stock,
                Description = p.Description,
                CategoryName = p.Category?.Name ?? "",
                CreatedOn = p.CreatedOn
            }).ToList();

            return (dtoList, null);
        }

    }
}
