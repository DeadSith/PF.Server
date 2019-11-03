using PF.Data;
using System.Linq;

namespace PF.Api.Services
{
    public class BaseSettings : IBaseSettings
    {
        private readonly ApplicationDbContext _context;

        public BaseSettings(ApplicationDbContext context)
        {
            _context = context;
        }

        public double BaseSalary
        {
            get
            {
                var setting = _context.Settings.First(s => s.Key == nameof(BaseSalary));
                return double.Parse(setting.Value);
            }
        }
    }
}
