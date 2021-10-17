using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
namespace Real_Estate_Agency
{
    public partial class MainWindow : Window
    {
        DataTable dataSet;
        SqlDataAdapter adapter;
        CommandSelect command;
        CommandDelete commandDelete;
        Dictionary<string, string> ComboboxResurs;
        Dictionary<string, string> Tables = new Dictionary<string, string>
        {
            {"Client","clients"},
            {"Agent","agents"},
        };

        static string ChoseString;
        public object Form { get; private set; }

        public MainWindow()
        {
            InitializeComponent();
            SearchGrid.Visibility = Visibility.Hidden;
            ResetBtn.Visibility = Visibility.Hidden;
            GridBtn.Visibility = Visibility.Hidden;
            SearchCombobox.SelectedItem = "first name";
        }

        private void DBchose(object sender, SelectionChangedEventArgs e)
        {
            ListBoxItem listBoxItem = (sender as ListBox).SelectedItem as ListBoxItem;
            if (listBoxItem != null)
            {
                ChoseString = listBoxItem.Content.ToString();
                CallDataGridSelect(ChoseString);
                SearchGrid.Visibility = Visibility.Visible;
                GridBtn.Visibility = Visibility.Visible;
            }
        }

        public void CallDataGridSelect(string ChoseStr)
        {
            if (ChoseString == "Client")
            {
                ComboboxResurs = new Dictionary<string, string>
                    {
                        {"id", "Id"},
                        {"first name", "FirstName"},
                        {"middle name","MiddleName" },
                        {"last name","LastName" },
                        {"phone","Phone" },
                        {"email","Email" },
                    };
                DataGridSelect(CommandSelect.selectAll, Tables["Client"]);
            }
            else if (ChoseString == "Agent")
            {
                ComboboxResurs = new Dictionary<string, string>
                    {
                        {"id", "Id"},
                        {"first name", "FirstName"},
                        {"middle name","MiddleName" },
                        {"last name","LastName" },
                        {"deal share","DealShare" },
                    };
                DataGridSelect(CommandSelect.selectAll, Tables["Agent"]);
            }
            if (ComboboxResurs != null)
            {
                try
                {
                    SearchCombobox.ItemsSource = ComboboxResurs.Keys;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        public void DataGridSelect(string[] array, string datatable, string where = null, string wherevalue = null)
        {
            try
            {
                if (where != null && wherevalue != null)
                {
                    command = new CommandSelect(array, datatable, where, wherevalue);
                }
                else
                {
                    command = new CommandSelect(array, datatable);
                }
                command.SqlCommand().ExecuteNonQuery();
                adapter = new SqlDataAdapter(command.SqlCommand());
                dataSet = new DataTable(datatable);
                adapter.Fill(dataSet);
                DataBaseGrid.ItemsSource = dataSet.DefaultView;
            }
            catch 
            {
            }
        }
        private void SearchBtnClick(object sender, RoutedEventArgs e)
        {
            DataGridSelect(CommandSelect.selectAll, Tables[ChoseString], ComboboxResurs[SearchCombobox.Text], SearchTextBox.Text);
            ResetBtn.Visibility = Visibility.Visible;
        }

        private void SearchTextChange(object sender, TextChangedEventArgs e)
        {

        }
        private void ResetBtnClick(object sender, RoutedEventArgs e)
        {
            SearchTextBox.Text = "";
            ResetBtn.Visibility = Visibility.Hidden;
            CallDataGridSelect(ChoseString);
        }



        private void Close(object sender, EventArgs e)
        {
            SqlConnectionSingle.CloseConnection();
        }
        public void OpenPerson(int id)
        {
            Thread thread = new Thread(() =>
            {
                NewPerson w = new NewPerson();
                w.id = id;
                w.table = ChoseString;
                w.Show();

                w.Closed += (sender2, e2) =>
                {
                    w.Dispatcher.InvokeShutdown();
                };

                System.Windows.Threading.Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.IsBackground = true;
            thread.Start();
            DataBaseGrid.ItemsSource = null;
            ChoseDateBase.SelectedItem = null;
            GridBtn.Visibility = Visibility.Hidden;
            SearchGrid.Visibility = Visibility.Hidden;
        }
        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            OpenPerson(0);

        }
        private void DeleteBtnClick(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataBaseGrid.SelectedItem;
            if (dataRowView != null)
            {
                MessageBoxResult result = MessageBox.Show("Are you sure?", "Delete", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    commandDelete = new CommandDelete(Tables[ChoseString], "Id", Convert.ToString(dataRowView.Row[0]));
                    commandDelete.SqlCommand().ExecuteScalar();
                    CallDataGridSelect(ChoseString);
                }
            }
            else
            {
                MessageBox.Show("Chose somebody!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }
        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            DataRowView dataRowView = (DataRowView)DataBaseGrid.SelectedItem;
            if (dataRowView != null)
            {
                int id = Convert.ToInt32(dataRowView.Row[0]);
                OpenPerson(Convert.ToInt32(dataRowView.Row[0]));
            }
            else
            {
                MessageBox.Show("Chose somebody!", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }


        }
    }
}
