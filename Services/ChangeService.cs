using BcpChallenge.Mappers;
using BcpChallenge.Repositories;
using BcpChallenge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BcpChallenge.Services
{

    public interface IChangeService
    {
        ChangeViewModel CurrencyExchange(CurrencyExchangeViewModel currencyExchangeViewModel);
        List<CurrencyExchangeIndexViewModel> GetAll();
        void Update(CurrencyViewModel currencyViewModel);
    }

    public class ChangeService : IChangeService
    {

        private readonly ICurrencyRepository _currencyRepository;

        public ChangeService(ICurrencyRepository currencyRepository) 
        {
            _currencyRepository = currencyRepository;
        }

        public List<CurrencyExchangeIndexViewModel> GetAll()
        {
            return CurrencyExchangeMapper.MapModelToIndexViewModel(_currencyRepository.GetAll());
        }

        public ChangeViewModel CurrencyExchange(CurrencyExchangeViewModel currencyExchangeViewModel)
        {
            var originCurrency = _currencyRepository.GetById(currencyExchangeViewModel.IdOriginCurrency);
            var destinationCurrency = _currencyRepository.GetById(currencyExchangeViewModel.IdDestinationCurrency);
            return CurrencyExchangeMapper.MapModelToViewModel(originCurrency, destinationCurrency, currencyExchangeViewModel);
        }

        public void Update(CurrencyViewModel currencyViewModel)
        {
            var currency = _currencyRepository.GetById(currencyViewModel.IdCurrency);
            currency.Change = currencyViewModel.Amount;
            _currencyRepository.Update(currency);
        }
    }
}
