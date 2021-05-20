namespace Moonpig.PostOffice.Service.Implementations
{
    using Moonpig.PostOffice.Data;
    using Moonpig.PostOffice.Service.Abstractions;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// The implementation of IProductService.
    /// </summary>
    public class ProductService : IProductService
    {
        /// <summary>
        /// The dbcontext class that contains all models.
        /// </summary>
        private IDbContext _dbContext { get; set; }

        public ProductService(IDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        /// <summary>
        /// Gets the highest lead time from the suppliers in a given list of products.
        /// </summary>
        /// <param name="productIds">The products in an order represented by Ids</param>
        /// <returns>The highest lead time</returns>
        private int GetHighestLeadTime(List<int> productIds)
        {
            var highestLd = from product in _dbContext.Products
                            join supplier in _dbContext.Suppliers
                            on product.SupplierId equals supplier.SupplierId
                            where productIds.Contains(product.ProductId)
                            select supplier.LeadTime;
            return highestLd.Max();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productIds"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public DateTime GetDespatchdate(List<int> productIds, DateTime date)
        {
            var highestLeadTime = GetHighestLeadTime(productIds);
           
            while (highestLeadTime > 0)
            {
                date = date.AddDays(1);

                if (date.DayOfWeek != DayOfWeek.Saturday && date.DayOfWeek != DayOfWeek.Sunday)
                {
                    highestLeadTime -= 1;
                }
            }
            return date;
        }

    }
}
