using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.WebApi.DTOs;
using Backend.WebApi.Mappers;
using Backend.WebApi.Models;
using Backend.WebApi.Repositories;

namespace Backend.WebApi.Services
{
    abstract public class ServiceBase<TModel, TDto>
            where TModel : BaseEntity
            where TDto : BaseDto
    {
        protected readonly IMapper<TModel, TDto> Mapper;
        private readonly BaseRepository<TModel> _repository;

        protected ServiceBase(BaseRepository<TModel> repository, IMapper<TModel, TDto> mapper)
        {
            Mapper = mapper;
            _repository = repository;
        }

        public virtual List<TModel> GetAll() =>
            _repository.GetAll().ToList();

        public virtual Task<List<TModel>> GetAllAsync() =>
            _repository.GetAllAsync();

        public virtual TDto Get(long id)
        {
            return Mapper.ToDto(_repository.Get(id));
        }

        public virtual async Task<TDto> GetAsync(long id)
        {
            return Mapper.ToDto(await _repository.GetAsync(id));
        }

        public virtual TDto Create(TDto entity)
        {
            return Mapper.ToDto(_repository.Create(Mapper.ToEntityModel(entity)));
        }

        public virtual async Task<TDto> CreateAsync(TDto entity)
        {
            return Mapper.ToDto(await _repository.CreateAsync(Mapper.ToEntityModel(entity)));
        }

        public virtual TDto Update(TDto entity)
        {
            return Mapper.ToDto(_repository.Update(Mapper.ToEntityModel(entity)));
        }

        public virtual async Task<TDto> UpdateAsync(TDto entity)
        {
            return Mapper.ToDto(await _repository.UpdateAsync(Mapper.ToEntityModel(entity)));
        }

        public virtual void Delete(int id)
        {
            _repository.Delete(id);
        }

        public virtual void DeleteAsync(int id)
        {
            _repository.DeleteAsync(id);
        }
    }
}
