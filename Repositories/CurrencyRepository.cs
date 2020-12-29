using BcpChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BcpChallenge.Repositories
{

    public interface ICurrencyRepository
    {
        List<CurrencyChange> GetAll();
        CurrencyChange GetById(int idCurrency);
        void Update(CurrencyChange currencyChange);
    }

    public class CurrencyRepository : ICurrencyRepository
    {

        private readonly ApiContext _context;

        public CurrencyRepository(ApiContext context)
        {
            _context = context;
        }

        public List<CurrencyChange> GetAll()
        {
            return _context.CurrencyChange.ToList();
        }

        public CurrencyChange GetById(int idCurrency)
        {
            return _context.CurrencyChange.Where(x => x.IdCurrencyChange == idCurrency).FirstOrDefault();
        }

        public void Update(CurrencyChange currencyChange)
        {
            _context.Update(currencyChange);
            _context.SaveChanges();
        }
    }
}
