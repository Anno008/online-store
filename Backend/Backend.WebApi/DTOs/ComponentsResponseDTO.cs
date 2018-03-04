using System.Collections.Generic;
using Backend.WebApi.Models;

namespace Backend.WebApi.DTOs
{
    public class ComponentsResponseDTO
    {
        public ComponentsResponseDTO((List<Component> components, int totalPages, int totalItems, int itemsOnPage, int currentPage) pageableParams)
        {
            Components = pageableParams.components;
            TotalItems = pageableParams.totalItems;
            Pages = pageableParams.totalPages;
            ItemsOnPage = pageableParams.itemsOnPage;
            CurrentPage = pageableParams.currentPage;
        }
     
        public List<Component> Components { get; set; }
        public int TotalItems { get; set; }
        public int Pages { get; set; }
        public int ItemsOnPage { get; set; }
        public int CurrentPage { get; set; }
    }
}
