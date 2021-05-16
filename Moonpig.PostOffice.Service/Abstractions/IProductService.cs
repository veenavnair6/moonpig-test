namespace Moonpig.PostOffice.Service.Abstractions
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The interface containing product model related service methods.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Gets the despatch date for a given set of product ids and the order date.
        /// </summary>
        /// <param name="productIds">The products in an order represented by Ids</param>
        /// <param name="OrderDate">The date of order</param>
        /// <returns>The despatch date for the order.</returns>
        DateTime GetDespatchdate(List<int> productIds, DateTime OrderDate);
    }
}
