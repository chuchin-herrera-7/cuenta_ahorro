using cuenta_ahorro.EF.Entities;
using Microsoft.AspNetCore.Http;
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

        // Obtener cuentas de ahorro de acuerdo al cliente
        public async Task<ActionResult<OpeningSavingAccount>> Get_(int id)
        {
            return await _dbContext.OpeningSavingAccount.Where(element => element.Id == id).FirstOrDefaultAsync();
        }

        public async Task<ActionResult<OpeningSavingAccount>> GetLast()
        {
            return await _dbContext.OpeningSavingAccount.OrderByDescending(item => item.Id).FirstOrDefaultAsync();
        }

        // Obtener cuentas de ahorro de acuerdo al cliente que inicia sesión
        public async Task<ActionResult<List<OpeningSavingAccount>>> GetBySession(int key)
        {
            return await _dbContext.OpeningSavingAccount.Where(element => element.IdClient == key).ToListAsync();
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
                _dbContext.OpeningSavingAccount.Where(item => item.Id == _openingSavingAccount.Id).First();
                _dbContext.Entry(_openingSavingAccount).State = EntityState.Modified;
                _dbContext.Entry(_openingSavingAccount).Property(x => x.Balance).IsModified = true;
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
