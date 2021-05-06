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

        public BrandDTO GetBrandById(int id) => Get<BrandDTO>($"{Address}/brands/{id}");
        public IEnumerable<BrandDTO> GetBrands() => Get<IEnumerable<BrandDTO>>($"{Address}/brands");

        public ProductDTO GetProductById(int id) => Get<ProductDTO>($"{Address}/{id}");

        public PageProductsDTO GetProducts(ProductFilter Filter = null) =>
            Post(Address, Filter ?? new ProductFilter())
                .Content
                .ReadAsAsync<PageProductsDTO>()
                .Result;
        public SectionDTO GetSectionById(int id) => Get<SectionDTO>($"{Address}/sections/{id}");
        public IEnumerable<SectionDTO> GetSections() => Get<IEnumerable<SectionDTO>>($"{Address}/sections");
    }
}
