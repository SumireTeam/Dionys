using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Dionys.Models;
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
        public EatenProductsTotal GetCaloriesTotal()
        {
            var eatenProductsByWeek = _context.EatenProducts.Local;

            return GetCaloriesFrom(eatenProductsByWeek);
        }

        // GET: api/statistic/calories/2019/1
        [HttpGet("{year}/{weekNumber}")]
        public EatenProductsTotal GetCalories(int year, int weekNumber)
        {
            var eatenProductsByWeek = _context.EatenProducts.Where(x => Equals(GetWeekYear(x.Time), new Tuple<int, int>(year, weekNumber)));
            return GetCaloriesFrom(eatenProductsByWeek);
        }

        /// <summary>
        /// Get eaten products report from eatenProducts list
        /// </summary>
        /// <param name="products">products</param>
        /// <returns>report</returns>
        private EatenProductsTotal GetCaloriesFrom(IEnumerable<EatenProduct> products)
        {
            IEnumerable<EatenProduct> eatenProducts = products.ToList();

            var total = new EatenProductsTotal
            {
                Products = eatenProducts,
                TotalCalories =
                    Convert.ToInt64(eatenProducts.Select(x => x.Weight * x.Product.Energy / 100).FirstOr(0))
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
