namespace Moonpig.PostOffice.Api.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Model;
    using Moonpig.PostOffice.Service.Abstractions;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// The depatch date controller class.
    /// </summary>
    [Route("api/[controller]")]
    public class DespatchDateController : Controller
    {
        /// <summary>
        /// The service methods related to product model.
        /// </summary>
        private IProductService _productService;

        /// <summary>
        /// Initializes a new instance of the <see cref="DespatchDateController"/> class.
        /// </summary>
        /// <param name="productService">The product service.</param>
        public DespatchDateController(IProductService productService)
        {
            _productService = productService;
        }

        /// <summary>
        /// The get method for finding despatch date of order.
        /// </summary>
        /// <param name="productIds">The products in an order represented by Ids</param>
        /// <param name="orderDate">The date of order</param>
        /// <returns>The despatch date.</returns>
        [HttpGet]
        public IActionResult Get(List<int> productIds, DateTime orderDate)
        {
            var _mlt = _productService.GetDespatchdate(productIds, orderDate);
            return Ok(new DespatchDate { Date = _mlt });
        }
    }
}
