﻿using ApplicationDbContext.UOW;
using Microsoft.AspNetCore.Mvc;
using ApplicationDbContext.Models;
using System.Linq;
using Customer.Models;
/*using ECommerce.Models.ViewModels;
*/
using Microsoft.AspNetCore.Mvc.Rendering;
using AutoMapper;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;
using System.Text.Json;

namespace Customer.Controllers
{
    public class ProductOrderController : BaseController
    {
        public ProductOrderController(IUnitOfWork uow, IMapper mapper, UserManager<User> userManager, SignInManager<User> signInManager) : base(uow, mapper, userManager, signInManager)
        {

        }


        public IActionResult Index()
        {
            return View();
        }

        public JsonResult AddProduct(int productId, int quantity, float price)
        {
            getUserFromSession();
            var res = "same";
            List<ProductOrder> cart = new List<ProductOrder>();
            List<Product> prods = new List<Product>();
            if (ViewBag.UserId != null)
            {
                var productOrder = new ProductOrder()
                {
                    ProductId = productId,
                    Quantity = quantity,
                    Price = Convert.ToDecimal(price)
                };
                cart = ViewBag.Cart;
                decimal? totalPrice = 0;
                var isHere = false;
                if (cart.Count != 0)
                {
                    int i = 0;
                    foreach (var item in cart.ToList())
                    {
                        if (item.ProductId == productId)
                        {
                            cart.RemoveAt(i);
                            isHere = true;
                            continue;
                        }
                        i++;
                        totalPrice += (item.Price)*(item.Quantity);
                    }
                   
                }
                //var cart = JsonSerializer.Deserialize<List<ProductOrder>>(cartContent);
                
                if (quantity > 0)
                {
                    if ((ViewBag.CartNumber != null)&&(isHere == false))
                    {
                        ViewBag.CartNumber = ViewBag.CartNumber + 1;
                        res = "add";
                    }
                    cart.Add(productOrder);
                   
                }
                if ((isHere) && (quantity<=0))
                {
                    res = "minus";
                    ViewBag.CartNumber = ViewBag.CartNumber - 1;
                }
                int e = ViewBag.CartNumber;

                string serializeCart = JsonSerializer.Serialize(cart);
                HttpContext.Session.SetString("Cart", serializeCart);
                ViewBag.Cart = cart;
                foreach(var item in cart)
                {
                    var prod = _uow.ProductRepo.Find(x => x.Id == item.ProductId);
                    prod.Price = item.Price*item.Quantity;  
                    prods.Add(prod);    
                }
                
                

            }

               

            return Json(prods);

        }
    }
}