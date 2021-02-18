using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using challenge.Data;
using challenge.Models;
using Microsoft.Extensions.Logging;

namespace challenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly CompensationContext _context;
        private readonly ILogger<CompensationRepository> _logger;

        public CompensationRepository(CompensationContext context, ILogger<CompensationRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public Compensation Add(Compensation compensation)
        {
            if(compensation != null)
            {
                _context.Add(compensation);
            }

            return compensation;
        }

        public Compensation GetCompensationByEmployeeId(string id)
        {
            return _context.Compensations.SingleOrDefault(c => c.EmployeeId == id);
        }

        public Task SaveAsync()
        {
            return _context.SaveChangesAsync();
        }
    }
}
