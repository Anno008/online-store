﻿using System.Collections.Generic;
using Backend.WebApi.Models;

namespace Backend.WebApi.DTOs.ResponseDTOs
{
    public class ShoppingCartResponseDTO
    {
        public string Username { get; set; }
        public List<ShoppingCartItem> Items { get; set; }
        public double TotalPrice { get; set; }

        public ShoppingCartResponseDTO(ShoppingCart cart)
        {
            Username = cart.User.Username;
            Items = cart.Items;
            TotalPrice = cart.TotalPrice;
        }
    }
}
