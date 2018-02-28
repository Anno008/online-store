using System.Linq;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;
using Xunit;

namespace Backend.Tests.UnitTests.Repositories
{
    public class BrandRepositoryUnitTests : RepositoryUnitTestsBase
    {
        private readonly BrandRepository brandRepository;

        public BrandRepositoryUnitTests()
        {
            brandRepository = new BrandRepository(dbContext);
            dbContext.Brands.Add(new Brand { Name = "Intel" });
            dbContext.Brands.Add(new Brand { Name = "AMD" });
            dbContext.SaveChanges();
        }

        [Fact]
        public void GetAllBrands_ShouldReturnExpectedResults()
        {
            var brands = brandRepository.GetAll();

            Assert.Equal(2, brands.ToList().Count());
            Assert.Equal(1, brands.First().Id);
            Assert.Equal("Intel", brands.First().Name);
        }

        [Fact]
        public async void GetAllBrandsAsync_ShouldReturnExpectedResults()
        {
            var brands = await brandRepository.GetAllAsync();

            Assert.Equal(2, brands.ToList().Count());
            Assert.Equal(1, brands.First().Id);
            Assert.Equal("Intel", brands.First().Name);
        }

        [Fact]
        public void GetBrandById_BrandExists_ReturnBrand()
        {
            var brand = brandRepository.Get(1);

            Assert.NotNull(brand);
            Assert.Equal(1, brand.Id);
            Assert.Equal("Intel", brand.Name);
        }

        [Fact]
        public async void GetBrandByIdAsync_BrandExists_ReturnBrand()
        {
            var brand = await brandRepository.GetAsync(1);

            Assert.NotNull(brand);
            Assert.Equal(1, brand.Id);
            Assert.Equal("Intel", brand.Name);
        }

        [Fact]
        public void GetBrandById_BrandDoesntExists_ReturnBrand()
        {
            var brand = brandRepository.Get(3);

            Assert.Null(brand);
        }

        [Fact]
        public async void GetBrandByIdAsync_BrandDoesntExists_ReturnBrand()
        {
            var brand = await brandRepository.GetAsync(3);

            Assert.Null(brand);
        }

        [Fact]
        public void CreateBrand_ShouldReturnTheCreatedBrand_AndIncreaseTheBrandsCount()
        {
            var newBrand = new Brand { Name = "Apple" };
            var expectedNumberOfBrands = dbContext.Brands.Count() + 1;

            var createdBrand = brandRepository.Create(newBrand);

            Assert.NotNull(createdBrand);
            Assert.Equal(newBrand.Name, createdBrand.Name);
            Assert.True(newBrand.Id != 0);
            Assert.Equal(expectedNumberOfBrands, dbContext.Brands.Count());
        }

        [Fact]
        public async void CreateBrandAsync_ShouldReturnTheCreatedBrand_AndIncreaseTheBrandsCount()
        {
            var newBrand = new Brand { Name = "Apple" };
            var expectedNumberOfBrands = dbContext.Brands.Count() + 1;

            var createdBrand = await brandRepository.CreateAsync(newBrand);

            Assert.NotNull(createdBrand);
            Assert.Equal(newBrand.Name, createdBrand.Name);
            Assert.True(newBrand.Id != 0);
            Assert.Equal(expectedNumberOfBrands, dbContext.Brands.Count());
        }

        [Fact]
        public void IfBrandExists_UpdateBrand_AndReturnUpdatedBrand()
        {
            var brand = new Brand { Id = 1, Name = "Intel123" };

            var updatedBrand = brandRepository.Update(brand);

            Assert.NotNull(updatedBrand);
            Assert.Equal(brand.Name, updatedBrand.Name);
            Assert.Equal(brand.Id, updatedBrand.Id);
        }

        [Fact]
        public void IfTheBrandToUpdate_DoesntExist_ReturnNull()
        {
            var brand = new Brand { Id = 3, Name = "Intel123" };

            var updatedBrand = brandRepository.Update(brand);

            Assert.Null(updatedBrand);
        }

        [Fact]
        public async void IfBrandExists_UpdateBrandAsync_AndReturnUpdatedBrand()
        {
            var brand = new Brand { Id = 1, Name = "Intel123" };
            var updatedBrand = await brandRepository.UpdateAsync(brand);

            Assert.NotNull(updatedBrand);
            Assert.Equal(brand.Name, updatedBrand.Name);
            Assert.Equal(brand.Id, updatedBrand.Id);
        }

        [Fact]
        public async void IfTheBrandToUpdate_DoesntExistAsync_ReturnNull()
        {
            var brand = new Brand { Id = 3, Name = "Intel123" };

            var updatedBrand = await brandRepository.UpdateAsync(brand);

            Assert.Null(updatedBrand);
        }

        [Fact]
        public void Delete_ShouldDeleteTheBrandFromTheDatabase()
        {
            var brandId = 1;

            brandRepository.Delete(brandId);
            Assert.Equal(1, dbContext.Brands.Count());
        }

        [Fact]
        public void DeleteAsync_ShouldDeleteTheBrandFromTheDatabase()
        {
            var brandId = 1;

            brandRepository.DeleteAsync(brandId);
            Assert.Equal(1, dbContext.Brands.Count());
        }
    }
}

