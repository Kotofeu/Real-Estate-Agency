using System;
using System.Windows;

namespace Real_Estate_Agency
{
    class CommandDelete: Command
    {

        public CommandDelete(string from, string where, string whereValue)
        {
            CommandChange(from, where, whereValue);
        }
        public void CommandChange(string from, string where, string whereValue)
        {
            try
            {
                ConnectionString = "DELETE FROM " + from + " WHERE " + where + $" = '{whereValue}'";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
