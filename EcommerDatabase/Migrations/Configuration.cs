namespace EcommerDatabase.Migrations
{
    using EcommerDatabase.Entities;
    using EcommerDatabase.Enums;
    using GenFu;
    using GenFu.ValueGenerators.Internet;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<EcommerDatabase.EcommerceDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(EcommerDatabase.EcommerceDbContext context)
        {
            ManufacturerSeeData(context);
            CategorySeedData(context);
            SupplierSeedData(context);
            ProductSeedData(context);
            ProductPriceSeedData(context);
            ProductImageSeedData(context);
            ProductStatusSeedData(context);
            context.SaveChanges();
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
        }

        private void ProductStatusSeedData(EcommerceDbContext context)
        {
            if (!context.ProductStatuses.Any())
            {
                var product = context.Products.Take(20).ToList();

                var i = 0;
                A.Configure<ProductStatus>()
                    .Fill(c => c.ProductId, () => { return product[i++].Id; });
                var productStatuses = A.ListOf<ProductStatus>(20);
                context.ProductStatuses.AddRange(productStatuses);
            }
        }
        private void ProductPriceSeedData(EcommerceDbContext context)
        {
            if (!context.ProductPrices.Any())
            {
                var product = context.Products.Take(20).ToList();

                var i = 0;
                A.Configure<ProductPrice>()
                    .Fill(c => c.MinQuantity).WithinRange(1000, 100000)
                    .Fill(c => c.MaxQuantity).WithinRange(100000, 10000000)
                    .Fill(c => c.Value).WithinRange(1000, 10000000)
                    .Fill(c => c.ProductId, () => { return product[i++].Id; });
                var productPrices = A.ListOf<ProductPrice>(20);
                context.ProductPrices.AddRange(productPrices);
            }
        }
        private void ProductImageSeedData(EcommerceDbContext context)
        {
            if (!context.ProductImages.Any())
            {
                var Company = new string[]
                {
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola"
                };
                var product = context.Products.Take(20).ToList();
                int i = 0, j = 0;

                A.Configure<ProductImage>()
                    .Fill(c => c.ImageLink, c => $"{Company[j++]}.com")
                    .Fill(c => c.ProductId, () => { return product[i++].Id; });
                var productImages = A.ListOf<ProductImage>(20);
                context.ProductImages.AddRange(productImages);
            }
        }
        private void ProductSeedData(EcommerceDbContext context)
        {
            if (!context.Products.Any())
            {
#if false

                var ProductList = new List<Product>();
                var product1 = new Product
                {
                    Name = "I Phone 10",
                    UrlName = "iphone10.vn",
                    Sku = "19020",
                    PublicationDate = DateTime.Parse("1/2/2014"),
                    Price = 200000,
                    View = 5,
                    Description = "New",
                    ShortDescription = "NEW",
                    Status = Status.Active,
                    CategoryId = Guid.Parse("e68f133f-2e72-41d8-8895-01f5da79efe3"),
                    SupplierId = Guid.Parse("1ab16182-a6ab-4405-b346-0d366c85f9a4"),
                    ManufactureId = Guid.Parse("8adfc347-044c-489b-ac90-1a75c7599355")
                };
                ProductList.Add(product1);

                var product2 = new Product
                {
                    Name = "I Phone 20",
                    UrlName = "iphone20.vn",
                    Sku = "4500",
                    PublicationDate = DateTime.Parse("1/12/2014"),
                    Price = 80000,
                    View = 6,
                    Description = "New",
                    ShortDescription = "NEW",
                    Status = Status.Active,
                    CategoryId = Guid.Parse("5fe59d9d-6cde-4e36-a66d-1eaaab097a44"),
                    SupplierId = Guid.Parse("a152ce1f-511d-475b-a856-2fc73e6aa321"),
                    ManufactureId = Guid.Parse("715558bf-6fce-4b80-8e26-431c2ea8d730")
                };
                ProductList.Add(product2);

                context.Products.AddRange(ProductList);
#else
                var category = context.Categories.Take(10).ToList();
                var categoryFirst = category.OrderBy(c => c.Sort).Skip(1).Take(1).FirstOrDefault();
                var categorySecond = category.OrderBy(c => c.Sort).Skip(3).Take(1).FirstOrDefault();

                var manufacturers = context.Manufacturers.Take(5).ToList();
                var manufacturersFirst = manufacturers.OrderBy(c => c.CodeName).Skip(1).Take(1).FirstOrDefault();
                var manufacturersSecond = manufacturers.OrderBy(c => c.CodeName).Skip(3).Take(1).FirstOrDefault();

                var supplier = context.Suppliers.Take(10).ToList();
                var supplierFirst = supplier.OrderBy(c => c.CodeName).Skip(1).Take(1).FirstOrDefault();
                var supplierSecond = supplier.OrderBy(c => c.CodeName).Skip(3).Take(1).FirstOrDefault();

                A.Configure<Product>()
                .Fill(c => c.Category, () => { return categoryFirst; })
                .Fill(c => c.Manufacturer, () => { return manufacturersFirst; })
                .Fill(c => c.Supplier, () => { return supplierFirst; });
                var product1 = A.ListOf<Product>(20);
                context.Products.AddRange(product1);

                A.Configure<Product>()
                .Fill(c => c.Price).WithinRange(1000, 1000000)
                .Fill(c => c.View).WithinRange(1000, 1000000)
                .Fill(c => c.Category, () => { return categorySecond; })
                .Fill(c => c.Manufacturer, () => { return manufacturersSecond; })
                .Fill(c => c.Supplier, () => { return supplierSecond; });
                var product2 = A.ListOf<Product>(20);
                context.Products.AddRange(product2);
#endif
            }
        }
        private void ManufacturerSeeData(EcommerceDbContext context)
        {
            if (!context.Manufacturers.Any())
            {
#if false
                var ManufacturersList = new List<Manufacturer>();
                var Manufacturers1 = new Manufacturer
                {
                    Name = "Trung Quoc",
                    CodeName = "TQ",
                    Description = "IPhone X",
                    Website = "TQ.com",
                    Logo = "China",
                    SiteId = null,
                    Status = Status.Active
                };
                ManufacturersList.Add(Manufacturers1);
                var Manufacturers2 = new Manufacturer
                {
                    Name = "My",
                    CodeName = "My",
                    Description = "Mac X",
                    Website = "My.com",
                    Logo = "My",
                    SiteId = Manufacturers1.Id,
                    Status = Status.Active
                };
                ManufacturersList.Add(Manufacturers2);
                var Manufacturers3 = new Manufacturer
                {
                    Name = "Han Quoc",
                    CodeName = "HQ",
                    Description = "Posc",
                    Website = "HQ.com",
                    Logo = "Korean",
                    SiteId = Manufacturers1.Id,
                    Status = Status.Active
                };
                ManufacturersList.Add(Manufacturers3);

                context.Manufacturers.AddRange(ManufacturersList);
#else
                var domain = Domains.DomainName();
                A.Configure<Manufacturer>()
                .Fill(c => c.SiteId,() =>{ return Guid.NewGuid(); })
                .Fill(c => c.Website, c => $"http://www.{domain}.com");
                var manufacturers = A.ListOf<Manufacturer>(40);
                context.Manufacturers.AddRange(manufacturers);
#endif
            }
        }
        private void CategorySeedData(EcommerceDbContext context)
        {
            if (!context.Categories.Any())
            {
#if false
                var categoryList = new List<Category>();
                var category1 = new Category
                {
                    Name = "Dien Thoai",
                    Description = "IPhone X",
                    Sort = 1,
                    IsDisplayHomePage = true,
                    ParentId = null,
                    Status = Status.Active
                };
                categoryList.Add(category1);
                var category2 = new Category
                {
                    Name = "May Tinh",
                    Description = "Mac Os",
                    Sort = 2,
                    IsDisplayHomePage = true,
                    Status = Status.Active,
                    ParentId = category1.Id
                };
                categoryList.Add(category2);
                var category3 = new Category
                {
                    Name = "Xe Hoi",
                    Description = "Poscher",
                    Sort = 3,
                    IsDisplayHomePage = true,
                    Status = Status.Active,
                    ParentId = category1.Id
                };
                categoryList.Add(category3);

                context.Categories.AddRange(categoryList);
#else
                var i = 1;
                A.Configure<Category>()
                    .Fill(c => c.Sort, () => { return i++; })
                    .Fill(c => c.IsDisplayHomePage, true)
                    .Fill(c => c.Status, Status.Active);
                var categories = A.ListOf<Category>(40);
                context.Categories.AddRange(categories);
#endif
            }
        }
        private void SupplierSeedData(EcommerceDbContext context)
        {
            if (!context.Suppliers.Any())
            {
#if false
                var Supplier1 = new Supplier
                {
                    Name = "The Gioi Di Dong",
                    CodeName = "C1",
                    Email = "tgdd@email.com",
                    Phone = "113",
                    Status = Status.Active
                };
                var Supplier2 = new Supplier
                {
                    Name = "Hoang Hai",
                    CodeName = "C2",
                    Email = "hh@email.com",
                    Phone = "112",
                    Status = Status.Active
                };
                var Supplier3 = new Supplier
                {
                    Name = "LG",
                    CodeName = "Lg",
                    Email = "hh@email.com",
                    Phone = "112",
                    Status = Status.Active
                };

                var supplierList = new List<Supplier> { Supplier1, Supplier2, Supplier3 };

                context.Suppliers.AddRange(supplierList);
            }
#else
                var Company = new string[] {
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",
                    "AGT", "KIO", "GGR", "Smith", "Orion", "GMC",
                    "Ford", "Hundai", "PacificAtlantic", "Mother's", "Frank's",
                    "Berkshire Hathaway", "Pepsi", "Coca-cola",};
                int i = 0;
                A.Configure<Supplier>()
                    .Fill(c => c.Email, c => $"{Company[i++]}@gmail.com");
                var suppliers = A.ListOf<Supplier>(40);
                context.Suppliers.AddRange(suppliers);
#endif
            }
        }
    }
}
