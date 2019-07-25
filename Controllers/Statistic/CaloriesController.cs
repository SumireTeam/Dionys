using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dionys.Models;
using Dionys.Models.ViewModels;
using Dionys.Models.ViewModels.Statistic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;

namespace Dionys.Controllers.Statistic
{
    [Route("api/statistic/[controller]")]
    public class CaloriesController : ControllerBase
    {
        private readonly DionysContext _context;

        public CaloriesController(DionysContext context)
        {
            _context = context;
        }

        // GET: api/statistic/calories
        /// <summary>
        /// Calories entity list
        /// </summary>
        /// <param name="paging"></param>
        /// <returns></returns>
        [HttpGet]
        public ConsumedProductsTotal GetCaloriesTotal([FromQuery] PagingParameterModel paging)
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
        private ConsumedProductsTotal GetCaloriesFrom(IEnumerable<ConsumedProduct> products)
        {
            IEnumerable<ConsumedProduct> consumedProducts = products.ToList();

            var total = new ConsumedProductsTotal
            {
                Products = consumedProducts,
                TotalCalories =
                    Convert.ToInt64(consumedProducts.Select(x => x.Weight * x.Product.Calories / 100).FirstOr(0))
            };

            return total;
        }

        /// <summary>
        /// Convert DateTime to year, week Tuple
        /// </summary>
        /// <param name="datetime">DateTime</param>
        /// <returns>year, week Tuple</returns>
        private Tuple<int, int> GetWeekYear(DateTime datetime)
        {
            // TODO: culture info
            CultureInfo cultureInfo = new CultureInfo("ru-RU");
            Calendar calendar = cultureInfo.Calendar;

            int year = datetime.Year;
            int week = calendar.GetWeekOfYear(datetime, CalendarWeekRule.FirstDay, DayOfWeek.Sunday);

            return new Tuple<int, int>(year, week);
        }
    }
}
