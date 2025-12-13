using AutoMapper;
using eCommerce.Application.DTOs;
using eCommerce.Application.DTOs.Category;
using eCommerce.Application.Services.Interfaces;
using eCommerce.Domain.Entities;
using eCommerce.Domain.Interfaces;

namespace eCommerce.Application.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IGeneric<Category> _categoryInterface;
        private readonly IMapper _mapper;

        public CategoryService(IGeneric<Category> categoryInterface, IMapper mapper)
        {
            _categoryInterface = categoryInterface;
            _mapper = mapper;
        }

        public async Task<IEnumerable<GetCategory>> GetAllAsync()
        {
            var result = await _categoryInterface.GetAllAsync();
            return _mapper.Map<IEnumerable<GetCategory>>(result);
        }

        public async Task<GetCategory> GetByIdAsync(Guid id)
        {
            var result = await _categoryInterface.GetByIdAsync(id);
            return result == null ? null : _mapper.Map<GetCategory>(result);
        }

        public async Task<ServiceResponse> AddAsync(CreateCategory category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            int result = await _categoryInterface.AddAsync(mappedCategory);

            return new ServiceResponse(
                result > 0,
                result > 0 ? "Category was added successfully" : "Failed to add category"
            );
        }

        public async Task<ServiceResponse> UpdateAsync(UpdateCategory category)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            int result = await _categoryInterface.UpdateAsync(mappedCategory);

            return new ServiceResponse(
                result > 0,
                result > 0 ? "Category was updated successfully" : "Failed to update category"
            );
        }

        public async Task<ServiceResponse> DeleteAsync(Guid id)
        {
            int result = await _categoryInterface.DeleteAsync(id);

            return new ServiceResponse(
                result > 0,
                result > 0 ? "Category was deleted successfully" : "Category not Found or failed to delete"
            );
        }
    }
}