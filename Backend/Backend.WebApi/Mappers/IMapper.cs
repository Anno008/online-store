namespace Backend.WebApi.Mappers
{
    public interface IMapper<TEntityModel, TDto>
    {
        TEntityModel ToEntityModel(TDto dto);
        TDto ToDto(TEntityModel model);
    }
}
