namespace Backend.WebApi.DTOs.RequestDTOs
{
    public class ComponentsRequestDTO
    {
        public string ComponentName { get; set; }
        public int[] BrandIds { get; set; }
        public int ComponentTypeId { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
        public OrderComponentsBy OrderBy { get; set; }
    }
}
