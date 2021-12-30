using cuenta_ahorro.EF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cuenta_ahorro.EF.Helpers
{
    public class ManagementAccountHelper
    {
        private readonly ApplicationDbContext _dbContext;

        public ManagementAccountHelper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener transacciones por id cuenta ahorro
        public async Task<ActionResult<List<ManagementAccount>>> Get(int _idOpeningSavingAccount)
        {
            return await _dbContext.ManagementAccount.Where(element => element.IdOpeningSavingAccount == _idOpeningSavingAccount).ToListAsync();
        }
    }
}
