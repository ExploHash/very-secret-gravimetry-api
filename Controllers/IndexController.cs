using gravimetry_api.Data;
// using gravimetry_api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace gravimetry_api.Controllers
{
    public class IndexController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IndexController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public List<int> test()
        {
            return new List<int> {1,2,4};
        }
    }
}