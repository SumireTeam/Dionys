using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dionys.Infrastructure.Models;
using Dionys.Web.Models.ViewModels.Statistic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace Dionys.Web.Controllers.Statistic
{
    [Route("api/statistic/[controller]")]
    public class CaloriesController : ControllerBase
    {
        private readonly IDionysContext _context;

        public CaloriesController(IDionysContext context)
        {
            _context = context;
        }

        // GET: api/statistic/calories
        /// <summary>
        /// Calories entity list
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ConsumedProductsTotal GetCaloriesTotal()
        {
            var consumedProductsByWeek = _context.ConsumedProducts.Local;

            return GetCaloriesFrom(consumedProductsByWeek);
        }

        // GET: api/statistic/calories/2019/1
        [HttpGet("{year}/{weekNumber}")]
        public ConsumedProductsTotal GetCalories(int year, int weekNumber)
        {
            var consumedProductsByWeek = _context.ConsumedProducts.Where(
                x => Equals(GetWeekYear(x.Timestamp), new Tuple<int, int>(year, weekNumber))
                );

            return GetCaloriesFrom(consumedProductsByWeek);
        }

        /// <summary>
        /// Get consumed products report from ConsumedProducts list
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>report</returns>
        private static ConsumedProductsTotal GetCaloriesFrom(IEnumerable<ConsumedProduct> products)
        {
            IEnumerable<ConsumedProduct> consumedProducts = products.ToList();

            var total = new ConsumedProductsTotal
            {
                Products = consumedProducts,
                TotalCalories = Convert.ToInt64(consumedProducts.Select(x => x.Weight * x.Product.Calories / 100).FirstOrDefault())
            };

            return total;
        }

        /// <summary>
        /// Convert DateTime to year, week Tuple
        /// </summary>
        /// <param name="datetime">DateTime</param>
        /// <returns>year, week Tuple</returns>
        private static Tuple<int, int> GetWeekYear(DateTimeOffset datetime)
        {
            // TODO: culture info
            var cultureInfo = new CultureInfo("ru-RU");
            var calendar = cultureInfo.Calendar;

            var year = datetime.Year;
            var week = calendar.GetWeekOfYear(datetime.DateTime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            return new Tuple<int, int>(year, week);
        }
    }
}
