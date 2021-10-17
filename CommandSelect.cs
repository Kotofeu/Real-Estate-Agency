using System;
using System.Windows;

namespace Real_Estate_Agency
{
    class CommandSelect : Command
    {
        

        public CommandSelect(string[] select, string from, string where = null, string whereValue = null)
        {
            CommandChange(select, from, where, whereValue);
        }
        public CommandSelect(string select, string from, string where = null, string whereValue = null)
        {
            CommandChange(select, from, where, whereValue);
        }
        public void CommandChange(string[] select, string from, string where = null, string whereValue = null)
        {
            try
            {
                for (int i = 0; i < select.Length; i++)
                {
                    ConnectionString += select[i];
                    if (i != select.Length - 1)
                    {
                        ConnectionString += ", ";
                    }
                }
                ConnectionString = "SELECT " + ConnectionString + " FROM " + from;
                if (where != null && whereValue != null)
                {
                    ConnectionString += " WHERE " + where + $" = '{whereValue}'";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void CommandChange(string select, string from, string where = null, string whereValue = null)
        {
            try
            {
                ConnectionString = "SELECT " + select + " FROM " + from;
                if (where != null && whereValue != null)
                {
                    ConnectionString += " WHERE " + where + $" = '{whereValue}'";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
