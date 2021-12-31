using cuenta_ahorro.EF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cuenta_ahorro.EF.Helpers
{
    public class PersonHelper
    {
        private readonly ApplicationDbContext _dbContext;

        public PersonHelper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener Personas por Id
        public async Task<ActionResult<Person>> GetBy(int IdClient)
        {
            return await _dbContext.Person.Where(element => element.Id == IdClient).FirstOrDefaultAsync();
        }

        // Obtener Personas
        public async Task<ActionResult<List<Person>>> Get()
        {
            return await _dbContext.Person.Where(item => item.Type == 1).ToListAsync();
        }

        // Obtener Personas
        public async Task<ActionResult<Person>> ValidateUser(string Email, string Password)
        {
            return await _dbContext.Person.Where(item => item.Email == Email && item.Password == Password).FirstOrDefaultAsync();
        }
    }
}
