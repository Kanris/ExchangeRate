using System;
using System.Collections.ObjectModel;
using System.Windows;
using ExchangeRateLibrary;

namespace tExchangeRate
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<ExchangeRateItem> exchangeRateItems; //dgExchangeRate items collection
        private ExchangeRate exchangeRate; //class to receive exchange rate

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Initialized(object sender, EventArgs e)
        {
            AddBankInfoValues(); //add bank info to the exchangeRate 
            AddItemsSource_dgExchangeRate(); //connect exchangeRateItems with dgExchangeRate

            DisplayRates(); //show exchange rates
        }

        private async void DisplayRates()
        {
            foreach(var bankID in exchangeRate.BanksInfo.Keys)
            {
                try
                {
                    var rateBuySell = await exchangeRate.GetBuySoldRate(bankID); //get buy and sell exchange rate
                    var rateItem = InitializeNewExchangeRateItem(bankID, rateBuySell); //create new item for dgExchangeRate

                    exchangeRateItems.Add(rateItem); //add item to the dgExchangeRate
                }
                catch (Exception e) //if bankID is not exist catch Exception and show error
                {
                    MessageBox.Show(e.Message, "Error", MessageBoxButton.OK);
                }

            }
        }

        //create new ExchangeRateItem for dgExchangeRate 
        private ExchangeRateItem InitializeNewExchangeRateItem(int bankID, Tuple<string, string> rateBuySell)
        {
            var newExchangeRateItem = new ExchangeRateItem(exchangeRate.BanksInfo[bankID].BankName, rateBuySell.Item1, rateBuySell.Item2); //Item1 - buy rate, Item2 - sell rate

            return newExchangeRateItem;
        }

        //add banks info to the exchangeRate class
        private void AddBankInfoValues()
        {
            exchangeRate = new ExchangeRate();

            try
            {
                exchangeRate.AddBankInfo(1, "Ощадбанк", "https://www.oschadbank.ua/ru/private/currency/currency_rates/", @"<td class=""text-right"">(\d+\.\d+)</td>", 1, 2);
                exchangeRate.AddBankInfo(2, "УКРГАЗБАНК", "https://www.ukrgasbank.com/", @"<td class=""val"">(\d+\.\d+)</td>", 0, 1);
            }
            catch (Exception e) //if bank id is already in the exchangeRate class 
            {
                MessageBox.Show(e.Message);
            }

        }

        //link exchangeRateItems with dgExchangeRate
        private void AddItemsSource_dgExchangeRate()
        {
            exchangeRateItems = new ObservableCollection<ExchangeRateItem>();
            dgExchangeRate.ItemsSource = exchangeRateItems;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
