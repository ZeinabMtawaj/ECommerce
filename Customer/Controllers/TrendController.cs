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


namespace Customer.Controllers
{
    public class TrendController : BaseController
    {
        public TrendController(IUnitOfWork uow, IMapper mapper) : base(uow, mapper)
        {

        }

        public IActionResult Index()
        {
            return View();
        }


        [HttpGet]
        public IEnumerable<Product> GetAll()
        {
            var items = _uow.TrendRepo.GetAll();
            ProductController cont = new ProductController(_uow, _mapper);
            var trends = new List<Product>();
            var i = 0;
            Product trend;
            string name = "";
            foreach(var item in items)
            {
                if (i == 9)
                {
                    break;
                }
                trend = cont.getCategory(item.ProductId);


               /* if (trend.Category != null)
                {
                    name = trend.Category.Name;
                }
                trend.Category = null;*/


            /*    var viewObj = _mapper.Map<ProductViewModel>(trend);
                viewObj.IsTrend = name;*/




/*
                trend.Sku = name;   */

                trends.Add(trend);

                i++;
                
            }
            return trends;
        }

    }
}