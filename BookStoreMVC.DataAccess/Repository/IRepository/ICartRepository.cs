﻿using BookStoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.DataAccess.Repository.IRepository
{
    public interface ICartRepository
    {
        void ClearCart(string userId);
        public Cart GetCart(string userId);

    }
}