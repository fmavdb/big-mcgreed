using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace Big_McGreed.content.data.sql
{
    /// <summary>
    /// Represents the database connection.
    /// Makes use of smart pooling, to minimalize the bandwidth.
    /// </summary>
    public class SqlDatabase
    {
        private static string databaseNaam = "TestDB";
        private OleDbConnection connection;
        private OleDbCommand command;

        /// <summary>
        /// Initializes a new instance of the <see cref="SqlDatabase"/> class.
        /// </summary>
        public SqlDatabase()
        {
            //Hier gaat de code 4 mapjes omhoog, en dan naar de map Big McGreedContent
            //Moet nog veranderd worden....
            if (!File.Exists(@"" + databaseNaam + ".accdb"))
            {
                throw new DatabaseException("The program failed to locate the database.");
            }

            string programHost = "Provider=Microsoft.ACE.OLEDB.12.0";
            string databaseSource = "Data Source=" + databaseNaam + ".accdb";
            this.connection = new OleDbConnection(programHost + ";" + databaseSource);
            this.command = connection.CreateCommand();
            Connect(); 
        }

        /// <summary>
        /// Connects this instance.
        /// </summary>
        public void Connect()
        {
            Console.WriteLine("Connecting...");
            // Check for any conditions that could interupt the connection.
            if (!checkState())
            {
                throw new DatabaseException("Failed to connect to the database.");
            }
            try
            {
                Console.WriteLine("Connected to the database.");
                connection.Open();
            }
            catch (InvalidOperationException e)
            {
                throw new DatabaseException(e.Message);
            }
        }

        /// <summary>
        /// Disconnects this instance.
        /// </summary>
        public void Disconnect()
        {
            try
            {
                connection.Close();
            }
            catch
            {
                throw new DatabaseException("-.- Welke exception kan hier voorkomen? - Print hiervan een message...");
            }
        }

        /// <summary>
        /// Destroys this instance.
        /// </summary>
        public void Destroy()
        {
            Disconnect();

            this.connection.Dispose();
            this.connection = null;
            this.command.Dispose();
            this.command = null;
        }

        /// <summary>
        /// Executes the query.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public object ExecuteQuery(string query)
        {
            this.command.CommandText = query;
            object obj = this.command.ExecuteScalar();
            this.command.CommandText = null;
            return obj;
        }

        /// <summary>
        /// Executes the update.
        /// </summary>
        /// <param name="query">The query.</param>
        public void ExecuteUpdate(string query)
        {
            this.command.CommandText = query;
            this.command.ExecuteNonQuery();
            this.command.CommandText = null;
        }

        /// <summary>
        /// Gets the reader.
        /// </summary>
        /// <param name="query">The query.</param>
        /// <returns></returns>
        public OleDbDataReader getReader(string query)
        {
            this.command.CommandText = query;
            return command.ExecuteReader();
        }

        /// <summary>
        /// Reads a set of tables from the database.
        /// </summary>
        /// <param name="query">The query that is executed to return and fill the data set.</param>
        /// <returns>Returns the dataset.</returns>
        public Dictionary<string, object> getTable(string query, string[] columns)
        {
            Dictionary<string, object> data = new Dictionary<string, object>();
            this.command.CommandText = query;

            OleDbDataReader reader = getReader(query);
            while(reader.Read())
            {
                foreach(string column in columns) {
                    data.Add(column, reader.GetValue(reader.GetOrdinal(column)));
                }
            }
            
            this.command.CommandText = null;
            return data;
        }

        /// <summary>
        /// Adds a parameter to the command.
        /// </summary>
        /// <param name="param">The name of the parameter.</param>
        /// <param name="value">The value that corresponds with the parameter.</param>
        public void AddParameter(string parameter, object value)
        {
            this.command.Parameters.AddWithValue(parameter, value);
        }

        /// <summary>
        /// Clears the added parameters.
        /// </summary>
        public void ClearParameters()
        {
            this.command.Parameters.Clear();
        }

        //TODO - Reconnect to database when failed.
        public bool checkState()
        {
            switch (connection.State)
            {
                case ConnectionState.Broken:
                    return false;
                case ConnectionState.Open:
                    return true;
            }
            return true;
        }

        public void getTable(string table)
        {
            this.command.CommandText = "SELECT * FROM " + table;
            OleDbDataReader reader = command.ExecuteReader();
        }
    }
}
