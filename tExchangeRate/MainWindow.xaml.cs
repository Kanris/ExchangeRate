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
        public static ExchangeRate exchangeRate; //class to receive exchange rate

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
                    var rateBuySell = await exchangeRate.GetBuySellRate(bankID); //get buy and sell exchange rate
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
        private ExchangeRateItem InitializeNewExchangeRateItem(int ID, Tuple<string, string> rateBuySell)
        {
            var bankID = BankID.Create(ID);
            var newExchangeRateItem = new ExchangeRateItem(ID, exchangeRate.BanksInfo[bankID].Name, rateBuySell.Item1, rateBuySell.Item2); //Item1 - buy rate, Item2 - sell rate

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

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Menu_AddBank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var addWindow = new AddEditWindow();

                if (addWindow.ShowDialog() == true)
                {
                    UpdateDataGrid();
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private void UpdateDataGrid()
        {
            exchangeRateItems.Clear();
            DisplayRates();
        }

        private void Menu_EditBank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedID = BankID.Create((dgExchangeRate.SelectedItem as ExchangeRateItem).ID);

                var addWindow = new AddEditWindow(exchangeRate.BanksInfo[selectedID], AddEditWindow.Operations.Edit);
                
                if (addWindow.ShowDialog() == true)
                {
                    UpdateDataGrid();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void Menu_RemoveBank_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedID = BankID.Create((dgExchangeRate.SelectedItem as ExchangeRateItem).ID);

                var editWindow = new AddEditWindow(exchangeRate.BanksInfo[selectedID], AddEditWindow.Operations.Delete);

                if (editWindow.ShowDialog() == true)
                {
                    UpdateDataGrid();
                }
            }
            catch (Exception except)
            {
                MessageBox.Show(except.Message);
            }
        }

        private void dgExchangeRate_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            var selectedItem = dgExchangeRate.SelectedItem as ExchangeRateItem;

            if (!ReferenceEquals(selectedItem, null))
            {
                try
                {
                    var selectedID = BankID.Create(selectedItem.ID);

                    var editWindow = new AddEditWindow(exchangeRate.BanksInfo[selectedID], AddEditWindow.Operations.Edit);

                    if (editWindow.ShowDialog() == true)
                    {
                        UpdateDataGrid();
                    }
                }
                catch (Exception excep)
                {
                    MessageBox.Show(excep.Message);
                }
            }
        }
    }
}
