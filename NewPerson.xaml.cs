using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
namespace Real_Estate_Agency
{
    public partial class NewPerson : Window
    {
        public int id = 0;
        public string table = null;

        private string FirstName;
        private string MiddleName;
        private string LastName;
        private string DealShare;
        private string Phone;
        private string Email;

        private bool IsCorrectToAppend = false;
        
        CommandSelect commandSelect;
        CommandInsert commandInsert;
        CommandUpdate commandUpdate;
        private List<string> FieldsValues;
        private List<string> Fields;
        Dictionary<string, string> Tables = new Dictionary<string, string>
        {
            {"Client","clients"},
            {"Agent","agents"},
        };
        public NewPerson()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            FirstName = FirstNameTextBox.Text;
            MiddleName = MiddleNameTextBox.Text;
            LastName = LastNameTextBox.Text;
            FieldsValues = new List<string> { FirstName, MiddleName, LastName };
            Fields = new List<string> { "FirstName", "MiddleName", "LastName" };
            if (FirstName != string.Empty && LastName != string.Empty && MiddleName != string.Empty)
            {
                if (table == "Client")
                {
                    Phone = PhoneTextBox.Text;
                    Email = EmailTextBox.Text;
                    FieldsValues.Add(Phone);
                    FieldsValues.Add(Email);
                    Fields.Add("Phone");
                    Fields.Add("Email");
                    if (Phone != string.Empty || Email != string.Empty)
                    {
                        IsCorrectToAppend = true;
                    }
                    else
                    {
                        MessageBox.Show("Phone or email must be filled in", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                else if (table == "Agent")
                {
                    DealShare = DealShereTextBox.Text;
                    FieldsValues.Add(DealShare);
                    Fields.Add("DealShare");
                    if (DealShare != string.Empty)
                    {
                        IsCorrectToAppend = true;
                    }
                    else
                    {
                        MessageBox.Show("Deal share must be filled in", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
                    }
                }
                if (IsCorrectToAppend)
                {
                    if (id == 0)
                    {
                        commandInsert = new CommandInsert(Tables[table], Fields, FieldsValues);
                        commandInsert.SqlCommand().ExecuteScalar();
                        MessageBox.Show(table + " has been added!", "Append", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        commandUpdate = new CommandUpdate(Tables[table], Fields, FieldsValues, "Id", id.ToString());
                        commandUpdate.SqlCommand().ExecuteScalar();
                        MessageBox.Show(table + " has been update!", "Update", MessageBoxButton.OK, MessageBoxImage.Information);

                    }
                    Close();
                }
            }
            else
            {
                MessageBox.Show("Required fields (*) must be filled in", "Error", MessageBoxButton.OK, MessageBoxImage.Hand);
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void FormLoad(object sender, RoutedEventArgs e)
        {
            //DataGridSelect(selectAll, Tables[ChoseString], ComboboxResurs[SearchCombobox.Text], SearchTextBox.Text);
            if (table == "Client")
            {
                EmailLable.Visibility = PhoneLable.Visibility = EmailTextBox.Visibility = PhoneTextBox.Visibility = Visibility.Visible;
                DealShareLable.Visibility = DealShereTextBox.Visibility = Visibility.Hidden;
            }
            else if (table == "Agent")
            {
                EmailLable.Visibility = PhoneLable.Visibility = EmailTextBox.Visibility = PhoneTextBox.Visibility = Visibility.Hidden;
                DealShareLable.Visibility = DealShereTextBox.Visibility = Visibility.Visible;
            }

            if (id != 0)
            {
                commandSelect = new CommandSelect(Command.selectAll, Tables[table]);

                commandSelect.CommandChange("FirstName", Tables[table], "Id", id.ToString());
                FirstNameTextBox.Text = commandSelect.SqlCommand().ExecuteScalar().ToString();
                commandSelect.CommandChange("MiddleName", Tables[table], "Id", id.ToString());
                MiddleNameTextBox.Text = commandSelect.SqlCommand().ExecuteScalar().ToString();
                commandSelect.CommandChange("LastName", Tables[table], "Id", id.ToString());
                LastNameTextBox.Text = commandSelect.SqlCommand().ExecuteScalar().ToString();
                if (table == "Client")
                {
                    commandSelect.CommandChange(Command.selectAllClients[4], Tables[table], "Id", id.ToString());
                    PhoneTextBox.Text = commandSelect.SqlCommand().ExecuteScalar().ToString();
                    commandSelect.CommandChange(Command.selectAllClients[5], Tables[table], "Id", id.ToString());
                    EmailTextBox.Text = commandSelect.SqlCommand().ExecuteScalar().ToString();
                }
                else if (table == "Agent")
                {
                    commandSelect.CommandChange(Command.selectAllAgents[4], Tables[table], "Id", id.ToString());
                    DealShereTextBox.Text = commandSelect.SqlCommand().ExecuteScalar().ToString();
                }
            }
        }
    }
}
