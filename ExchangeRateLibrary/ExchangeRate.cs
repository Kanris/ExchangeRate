﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Text;
using System.Threading.Tasks;

namespace ExchangeRateLibrary
{
    public class ExchangeRate
    {
        private enum Operations { Buy, Sell }; //exchange operation type

        private Dictionary<int, BankInfo> banksInfo;

        public Dictionary<int, BankInfo> BanksInfo
        {
            get
            {
                return banksInfo;
            }
        }

        public ExchangeRate() { banksInfo = new Dictionary<int, BankInfo>(); }

        public ExchangeRate(Dictionary<int, BankInfo> banksInfo)
        {
            this.banksInfo = banksInfo;
        }

        //add bank info to the collection
        public void AddBankInfo(int id, string bankName, string URI, string pattern, int indexBuy, int indexSold)
        {
            var newBankInfoItem = new BankInfo()
                { ID = id, BankName = bankName, URI = URI, pattern = pattern, IndexBuy = indexBuy, IndexSell = indexSold }; //create new BankInfo item

            CheckBankInfo(newBankInfoItem); //check that item with this ID doesn't exist

            banksInfo.Add(id, newBankInfoItem); //add new BankInfo item to collection
        }

        //add bank info to the collection
        public void AddBankInfo(BankInfo bankInfo)
        {
            CheckBankInfo(bankInfo); //check that item with this ID doesn't exist and parameter is not null

            banksInfo.Add(bankInfo.ID, bankInfo); //add new BankInfo item to collection
        }

        //remove bank info by ID
        public void RemoveBankInfo(int bankID)
        {
            banksInfo.Remove(bankID);
        }

        //get buy exchange rate by Bank ID
        public async Task<string> GetBuyRate(int bankID)
        {
            return await GetRate(bankID, Operations.Buy);
        }

        //get sell exchange rate by Bank ID
        public async Task<string> GetSellRate(int bankID)
        {
            return await GetRate(bankID, Operations.Sell);
        }

        //get buy and sell exchange rate by Bank ID
        public async Task<Tuple<string, string>> GetBuySoldRate(int bankID)
        {
            string buyRate = await GetBuyRate(bankID); //get buy rate
            string sellRate = await GetSellRate(bankID); //get sell rate

            return new Tuple<string, string>(buyRate, sellRate);
        }

        //get exchange rate
        private async Task<string> GetRate(int bankID, Operations operation)
        {
            if (!banksInfo.ContainsKey(bankID)) //if Bank ID is not in collection throw exception
                throw new KeyNotFoundException("Can't find Bank with this ID ({bankID})");

            var URI = banksInfo[bankID].URI; //bank website
            var pattern = banksInfo[bankID].pattern; //exchange pattern
            var neededIndex = operation == Operations.Sell ? banksInfo[bankID].IndexSell : banksInfo[bankID].IndexBuy; //get needed index

            return await ParseHTMLPage(URI, pattern, neededIndex);
        }

        //get exchange rate from bank web site
        private async Task<string> ParseHTMLPage(string URI, string pattern, int neededIndex)
        {
            var result = string.Empty;

            using (var wc = new WebClient())
            {
                var input = await Task.Run( () => wc.DownloadString(URI) ); //download html code
                var regex = new Regex(pattern); 
                var match = regex.Match(input); //find match in html page

                var currentIndex = 0; //current match position
                 
                while (match.Success && currentIndex < neededIndex) { currentIndex++; match = match.NextMatch(); } //search needed pattern position

                if (currentIndex == neededIndex) result = match.Groups[1].Value; //if we found needed position, write result to the result variable

            }

            return result;
        }

        //check that can we add Bank Info to the collection
        private void CheckBankInfo(BankInfo bankInfo)
        {
            if (ReferenceEquals(bankInfo, null)) //if bankInfo variable is null throw execption
                throw new NullReferenceException("Can't add empty object to the ExchangRate collection.");

            if (banksInfo.ContainsKey(bankInfo.ID)) //if Bank ID is already in collection throw exception
                throw new Exception($"This ID ({bankInfo.ID}) is already added to the ExchangeRate collection.");
        }
    }
}