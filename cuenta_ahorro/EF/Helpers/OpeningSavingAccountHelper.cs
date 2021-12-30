using cuenta_ahorro.EF.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace cuenta_ahorro.EF.Helpers
{
    public class OpeningSavingAccountHelper
    {
        private readonly ApplicationDbContext _dbContext;

        public OpeningSavingAccountHelper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener cuentas de ahorro de acuerdo al cliente
        public async Task<ActionResult<List<OpeningSavingAccount>>> Get(int _idClient)
        {
            return await _dbContext.OpeningSavingAccount.Where(element => element.IdClient == _idClient).ToListAsync();
        }

        //Obtener cuenta de acuerdo a numero de cuenta
        public async Task<ActionResult<OpeningSavingAccount>> GetBy(string _accountNumber)
        {
            return await _dbContext.OpeningSavingAccount.FirstAsync(element => element.AccountNumber == _accountNumber);
        }

        public async Task<ActionResult<string>> Update(OpeningSavingAccount _openingSavingAccount)
        {
            try
            {
                _dbContext.Entry(_openingSavingAccount).State = EntityState.Modified;
                _dbContext.Entry(_openingSavingAccount).Property(x => x.Id).IsModified = false;
                _dbContext.Entry(_openingSavingAccount).Property(x => x.AccountNumber).IsModified = false;
                _dbContext.Entry(_openingSavingAccount).Property(x => x.IdClient).IsModified = false;
                await _dbContext.SaveChangesAsync();
                return "Saldo actualizado exitosamente!! ";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
