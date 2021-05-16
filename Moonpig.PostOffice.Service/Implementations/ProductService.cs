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

        public DateTime GetDespatchdate(List<int> productIds, DateTime orderDate)
        {
            var highestLeadTime = GetHighestLeadTime(productIds);
            if (highestLeadTime >= 0)
            {
                DateTime _mlt = orderDate.AddDays(highestLeadTime); // max lead time
                if (_mlt.DayOfWeek == DayOfWeek.Saturday)
                {
                    return _mlt.AddDays(2);
                }
                else if (_mlt.DayOfWeek == DayOfWeek.Sunday) return _mlt.AddDays(1);
                else return _mlt;
            }
            else
            {
                throw new ArgumentException();
            }
        }


    }
}
