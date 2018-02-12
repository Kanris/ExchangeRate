using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ExchangeRateLibrary;

namespace tExchangeRate
{
    /// <summary>
    /// Interaction logic for AddEditWindow.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        public enum Operations { Add, Edit }

        private Operations oper;
        private BankInfo bankInfo;

        public AddEditWindow()
        {
            InitializeComponent();

            InitializeWindow(Operations.Add);
        }

        public AddEditWindow(BankInfo bankInfo, Operations oper)
        {
            InitializeComponent();

            InitializeWindow(oper, bankInfo);
        }

        public void InitializeWindow(Operations oper, BankInfo bankInfo = null)
        {
            this.oper = oper;
            this.bankInfo = bankInfo;

            if (!ReferenceEquals(bankInfo, null)) InitializeWindowElements();
        }

        private void InitializeWindowElements()
        {
            txID.Text = bankInfo.ID.ToString();
            txName.Text = bankInfo.Name;
            txPattern.Text = bankInfo.Pattern;
            txURI.Text = bankInfo.URI;
            txBuyIndex.Text = bankInfo.Buy.ToString();
            txSellIndex.Text = bankInfo.Sell.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var bankID = BankID.Create(Convert.ToInt32(txID.Text));
                var bankName = BankName.Create(txName.Text);
                var bankURI = BankURI.Create(txURI.Text);
                var bankPattern = BankPattern.Create(txPattern.Text);
                var bankBuyIndex = BankIndex.Create(Convert.ToInt32(txBuyIndex.Text));
                var bankSellIndex = BankIndex.Create(Convert.ToInt32(txSellIndex.Text));

                if (oper == Operations.Edit)
                    MainWindow.exchangeRate.RemoveBankInfo(this.bankInfo.ID);

                BankInfo bankInfo;

                bankInfo = CreateBankInfo(bankID, bankName, bankURI, bankPattern, bankBuyIndex, bankSellIndex);
                MainWindow.exchangeRate.AddBankInfo(bankInfo);

                this.DialogResult = true;
                this.Close();
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        private BankInfo CreateBankInfo(BankID bankID, BankName bankName, BankURI bankURI, 
            BankPattern bankPattern, BankIndex bankBuy, BankIndex bankSell)
        {
            var newBankInfo = new BankInfo(bankID, bankName, bankURI, bankPattern, bankBuy, bankSell);

            return newBankInfo;
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                MainWindow.exchangeRate.RemoveBankInfo(this.bankInfo.ID);

                this.DialogResult = true;
                this.Close();
            }
        }
    }
}
