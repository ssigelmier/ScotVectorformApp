using System;

namespace ScotVectorformApp
{
    public class DatabaseAccess
    {
        private System.Data.SQLite.SQLiteConnection _dbConnection;

        public DatabaseAccess()
        {
            _dbConnection = new System.Data.SQLite.SQLiteConnection("Data Source=ToDoDatabase.sqlite;");
        }

        public void CreateDatabase()
        {
            if (!System.IO.File.Exists("ToDoDatabase.sqlite"))
            {
                System.Data.SQLite.SQLiteConnection.CreateFile("ToDoDatabase.sqlite");
                CreateTasksTableInDatabase();
            }
//            InsertTaskIntoDatabase();
//            DeleteTaskFromDatabase();
                }

        private void CreateTasksTableInDatabase()
        {
            try
            {
                _dbConnection.Open();
                System.Data.SQLite.SQLiteCommand dbCommand = _dbConnection.CreateCommand();
                var tableCreationString = "CREATE TABLE IF NOT EXISTS " +
                    "Tasks (TaskId INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT, " +
                    "Name VARCHAR(100), Status VARCHAR(50));";
                dbCommand.CommandText = tableCreationString;
                dbCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("Database setup failed.");
                Console.ReadLine();
            }
        }

        public void InsertTaskIntoDatabase()
        {
            if (_dbConnection.State != System.Data.ConnectionState.Open)
                _dbConnection.Open();

            try
            {
                System.Data.SQLite.SQLiteCommand dbCommand = _dbConnection.CreateCommand();
                var insertString = "INSERT INTO Tasks(Name,Status) VALUES ('NewOne','New');";
                dbCommand.CommandText = (insertString);
                dbCommand.ExecuteNonQuery();
                dbCommand.ExecuteNonQuery();
                dbCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("INSERT line failed.");
                Console.ReadLine();
            }
        }

        public void DeleteTaskFromDatabase()
        {
            if (_dbConnection.State != System.Data.ConnectionState.Open)
                _dbConnection.Open();

            try
            {
                System.Data.SQLite.SQLiteCommand dbCommand = _dbConnection.CreateCommand();
                var insertString = "DELETE FROM Tasks WHERE TaskId=5;";
                dbCommand.CommandText = (insertString);
                dbCommand.ExecuteNonQuery();
                insertString = "DELETE FROM Tasks WHERE TaskId=6;";
                dbCommand.CommandText = (insertString);
                dbCommand.ExecuteNonQuery();
            }
            catch
            {
                Console.WriteLine("INSERT line failed.");
                Console.ReadLine();
            }
        }

        public void GetAllDatabaseRows()
        {
            try
            {
                System.Data.SQLite.SQLiteCommand dbCommand = _dbConnection.CreateCommand();
                var selectString = "SELECT * FROM Tasks;";
                dbCommand.CommandText = (selectString);
                System.Data.SQLite.SQLiteDataReader dbReader = dbCommand.ExecuteReader();

                while (dbReader.Read())
                {

                    var rowData = dbReader["Id"].ToString() + " " + dbReader["Name"] + " " + dbReader["Status"];
                    Console.WriteLine(rowData);
                    Console.ReadLine();
                }
            }
            catch
            {
                Console.WriteLine("SELECT line failed.");
                Console.ReadLine();
            }

        }
    }
}
