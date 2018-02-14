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
        public enum Operations { Add, Edit } //window type

        private Operations oper; //current window type
        private BankInfo bankInfo;

        //constructor for add window
        public AddEditWindow()
        {
            InitializeComponent();

            InitializeWindow(Operations.Add);
        }

        //constructor for edit window
        public AddEditWindow(BankInfo bankInfo)
        {
            InitializeComponent();

            InitializeWindow(Operations.Edit, bankInfo);
        }

        
        public void InitializeWindow(Operations oper, BankInfo bankInfo = null)
        {
            this.oper = oper;
            this.bankInfo = bankInfo;

            if (!ReferenceEquals(bankInfo, null)) InitializeWindowElements();
        }

        //fill window's items with information
        private void InitializeWindowElements()
        {
            txID.Text = bankInfo.ID.ToString();
            txName.Text = bankInfo.Name;
            txPattern.Text = bankInfo.Pattern;
            txURI.Text = bankInfo.URI;
            txBuyIndex.Text = bankInfo.Buy.ToString();
            txSellIndex.Text = bankInfo.Sell.ToString();
        }

        //save button clicked
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var bankID = BankID.Create(Convert.ToInt32(txID.Text)); //read id from txID
                var bankName = BankName.Create(txName.Text); //read name from txName
                var bankURI = BankURI.Create(txURI.Text); //read URI from txURI
                var bankPattern = BankPattern.Create(txPattern.Text); //read pattern from txPattern
                var bankBuyIndex = BankIndex.Create(Convert.ToInt32(txBuyIndex.Text)); //read BuyIndex from txBuyIndex
                var bankSellIndex = BankIndex.Create(Convert.ToInt32(txSellIndex.Text)); //read SellIndex from txSellIndex

                if (oper == Operations.Edit) //if window in "edit mode"
                {
                    MainWindow.exchangeRate.RemoveBankInfo(this.bankInfo.ID); //remove bankinfo from list
                    FileHelper.DeleteFromFile(this.bankInfo); //remove bankinfo from file
                }

                var bankInfo = CreateBankInfo(bankID, bankName, bankURI, bankPattern, bankBuyIndex, bankSellIndex);
                MainWindow.exchangeRate.AddBankInfo(bankInfo); //save bankinfo in list
                FileHelper.WriteInFile(bankInfo); //save bankinfo in file

                this.DialogResult = true;
                this.Close(); //close file
            }
            catch (Exception excep)
            {
                MessageBox.Show(excep.Message);
            }
        }

        //create bankInfo class
        private BankInfo CreateBankInfo(BankID bankID, BankName bankName, BankURI bankURI, 
            BankPattern bankPattern, BankIndex bankBuy, BankIndex bankSell)
        {
            var newBankInfo = new BankInfo(bankID, bankName, bankURI, bankPattern, bankBuy, bankSell);

            return newBankInfo;
        }

        //close button clicked
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        //delete button clicked
        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Are you sure?", "Delete confirmation", MessageBoxButton.YesNo) 
                == MessageBoxResult.Yes) //confirmation dialog
            {
                MainWindow.exchangeRate.RemoveBankInfo(this.bankInfo.ID); //remove from list
                FileHelper.DeleteFromFile(this.bankInfo); //remove from file

                this.DialogResult = true; 
                this.Close(); //close window
            }
        }
    }
}
