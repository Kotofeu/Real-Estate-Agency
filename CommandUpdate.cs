using System;
using System.Windows;
using System.Collections.Generic;
namespace Real_Estate_Agency
{
    class CommandUpdate: Command
    {

        public CommandUpdate(string table, List<string> update, List<string> values, string where, string whereValue)
        {
            try
            {
                if (update.Count > values.Count)
                {
                    for (int i = values.Count; i < update.Count; i++)
                    {
                        values.Add("NULL");
                    }
                }
                ConnectionString = "UPDATE " + table + " SET ";
                for (int i = 0; i < update.Count; i++)
                {
                    ConnectionString += update[i] + $" = '{values[i]}'";
                    if (i != update.Count - 1)
                    {
                        ConnectionString += ", ";
                    }
                }
                ConnectionString += " WHERE " + where + " = " + whereValue;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
