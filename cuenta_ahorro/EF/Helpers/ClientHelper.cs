using cuenta_ahorro.EF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cuenta_ahorro.EF.Helpers
{
    public class ClientHelper
    {
        private readonly ApplicationDbContext _dbContext;

        public ClientHelper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener Cliente por Id
        public async Task<ActionResult<Client>> GetBy(int IdClient)
        {
            return await _dbContext.Client.Where(element => element.Id == IdClient).FirstOrDefaultAsync();
        }

        // Obtener Clientes
        public async Task<ActionResult<List<Client>>> Get()
        {
            return await _dbContext.Client.ToListAsync();
        }
    }
}
