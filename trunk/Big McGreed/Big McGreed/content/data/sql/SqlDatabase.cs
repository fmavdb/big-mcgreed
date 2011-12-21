using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.IO;

namespace Big_McGreed.content.data.sql
{
    public class SqlDatabase
    {
        private static string databaseNaam = "TestDB";
        private OleDbConnection connection;
        private OleDbCommand command;

        public SqlDatabase()
        {
            //Hier gaat de code 4 mapjes omhoog, en dan naar de map Big McGreedContent
            //Moet nog veranderd worden....
            if (!File.Exists(@"..\..\..\..\Big McGreedContent\" + databaseNaam + ".accdb"))
            {
                throw new DatabaseException("The program failed to locate the database.");
            }
            string programHost = "Provider=Microsoft.ACE.OLEDB.12.0";
            string databaseSource = "Data Source=@..\\..\\..\\..\\Big McGreedContent\\" + databaseNaam + ".accdb";
            this.connection = new OleDbConnection(programHost + ";" + databaseSource);
            this.command = connection.CreateCommand();
            Connect(); //TODO - Een of andere error fixen.
        }

        public void Connect()
        {
            // Check for any conditions that could interupt the connection.
            if (connection == null)
            {
                throw new DatabaseException("The program failed to connect to the database.");
            }
            try
            {
                connection.Open();
            }
            catch
            {
                throw new DatabaseException("-.- Welke exception kan hier voorkomen? - Print hiervan een message...");
            }
        }

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

        public void Destroy()
        {
            Disconnect();

            this.connection.Dispose();
            this.connection = null;
            this.command.Dispose();
            this.command = null;
        }

        public object ExecuteQuery(string query)
        {
            this.command.CommandText = query;
            object obj = this.command.ExecuteScalar();
            this.command.CommandText = null;
            return obj;
        }

        public void ExecuteUpdate(string query)
        {
            this.command.CommandText = query;
            this.command.ExecuteNonQuery();
            this.command.CommandText = null;
        }

        //Moet nog wat aangepast worden...
        public void read(string query)
        {
            this.command.CommandText = query;
            OleDbDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                //Lees tabellen ofzo
            }
        }
    }
}
