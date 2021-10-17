using System;
using System.Collections.Generic;
using System.Windows;

namespace Real_Estate_Agency
{
    class CommandInsert: Command
    {
        public CommandInsert(string table, List<string> fields, List<string> values)
        {
            try
            {
                if(fields.Count > values.Count)
                {
                    for(int i = values.Count; i < fields.Count; i++)
                    {
                        values.Add("NULL");
                    }
                }
                ConnectionString = "INSERT INTO  " + table + " (";
                for (int i = 0; i < fields.Count; i++)
                {
                    ConnectionString += fields[i];
                    if (i != fields.Count - 1)
                    {
                        ConnectionString += ", ";
                    }
                }
                ConnectionString += ") VALUES (";
                for (int i = 0; i < values.Count; i++)
                {
                    ConnectionString += $"'{values[i]}'";
                    if (i != values.Count - 1)
                    {
                        ConnectionString += ", ";
                    }
                }
                ConnectionString += ");";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
