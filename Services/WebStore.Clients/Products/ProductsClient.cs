﻿using System.Collections.Generic;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using WebStore.Clients.Base;
using WebStore.Domain;
using WebStore.Domain.DTO;
using WebStore.Interfaces;
using WebStore.Interfaces.Services;

namespace WebStore.Clients.Products
{
    public class ProductsClient : BaseClient, IProductData
    {
        public ProductsClient(IConfiguration configuration) : base(configuration, WebApi.Products) { }

        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");

        public IEnumerable<ProductDTO> GetProducts(ProductFilter Filter = null) => 
            Post(Address, Filter ?? new ProductFilter()).Content.ReadAsAsync<IEnumerable<ProductDTO>>().Result;

        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");
    }
}