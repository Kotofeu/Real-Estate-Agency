using System.Data.SqlClient;
using System.Windows;

namespace Real_Estate_Agency
{

    class Command
    {
        public static readonly string[] selectAll = { "*" };
        public static readonly string[] selectAllAgents = { "Id", "FirstName", "MiddleName", "LastName", "DealShare"};
        public static readonly string[] selectAllClients = { "Id", "FirstName", "MiddleName", "LastName", "Phone", "Email" };

        SqlConnectionSingle sqlConnection = SqlConnectionSingle.getInstance();
        protected string ConnectionString { get; set; }
        public void Print()
        {
            MessageBox.Show(ConnectionString);
        }
        public SqlCommand SqlCommand()
        {
            SqlCommand command = new SqlCommand(ConnectionString, sqlConnection.Connection());
            return command;
        }
    }
}
