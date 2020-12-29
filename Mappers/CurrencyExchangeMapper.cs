using BcpChallenge.Models;
using BcpChallenge.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BcpChallenge.Mappers
{
    public static class CurrencyExchangeMapper
    {

        public static ChangeViewModel MapModelToViewModel(CurrencyChange originCurrency, CurrencyChange destinationCurrency, CurrencyExchangeViewModel currencyExchangeViewModel)
        {
            var dollars = currencyExchangeViewModel.Amount / originCurrency.Change;
            var oneDollar = 1 / originCurrency.Change;
            return new ChangeViewModel
            {
                Amount = currencyExchangeViewModel.Amount,
                OriginCurrency = originCurrency.CurrencyChangeName,
                DestinationCurrency = destinationCurrency.CurrencyChangeName,
                AmountWihtExchangeRate = Math.Round(dollars * destinationCurrency.Change, 2),
                ExchangeRate = $"{originCurrency.CurrencyChangeName} - 1 a {destinationCurrency.CurrencyChangeName} - {Math.Round(destinationCurrency.Change * oneDollar, 2)}"
            };
        }

        public static List<CurrencyExchangeIndexViewModel> MapModelToIndexViewModel(List<CurrencyChange> currencyChanges)
        {
            return currencyChanges.Select(x => new CurrencyExchangeIndexViewModel 
            { 
                IdCurrencyExchange = x.IdCurrencyChange,
                CurrencyExnchangeName = x.CurrencyChangeName,
                ChangeType = $"{x.CurrencyChangeName} - {x.Change} a Dolar 1"
            }).ToList();
        }

    }
}
