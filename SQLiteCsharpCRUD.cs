using System;
using System.Data.SQLite;

class SQLiteCRUD
{
    static SQLiteConnection CreateConnection()
    {
        SQLiteConnection sqliteConnection = new SQLiteConnection("Data Source= Database.db; Version = 3; New = True; Compress = True; ");
        try
        {
            sqliteConnection.Open();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        return sqliteConnection;
    }

    static void CreateTable(SQLiteConnection sqliteConnection)
    {
        try 
        {
            SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
            sqliteCommand.CommandText = "CREATE TABLE People(name VARCHAR(20), age INT)";
            sqliteCommand.ExecuteNonQuery();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

    static void InsertData(SQLiteConnection sqliteConnection, string name,int age)
    {
        SQLiteCommand sqliteCommand;
        sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = "INSERT INTO People(name, age) VALUES($name,$age);";
        sqliteCommand.Parameters.AddWithValue("$name", name);
        sqliteCommand.Parameters.AddWithValue("$age", age);
        sqliteCommand.ExecuteNonQuery();
    }

    static void DeleteData(SQLiteConnection sqliteConnection, string name, int age)
    {
        SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = "DELETE FROM People WHERE name=$name AND age=$age";
        sqliteCommand.Parameters.AddWithValue("$name", name);
        sqliteCommand.Parameters.AddWithValue("$age", age);
        sqliteCommand.ExecuteNonQuery();
    }

    static void UpdateData(SQLiteConnection sqliteConnection, string name, int age)
    {
        SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = "UPDATE People SET age=$age WHERE name=$name";
        sqliteCommand.Parameters.AddWithValue("$name", name);
        sqliteCommand.Parameters.AddWithValue("$age", age);
        sqliteCommand.ExecuteNonQuery();
    }

    static void ReadData(SQLiteConnection sqliteConnection)
    {
        SQLiteDataReader sqliteDatareader;
        SQLiteCommand sqliteCommand = sqliteConnection.CreateCommand();
        sqliteCommand.CommandText = "SELECT * FROM People";

        sqliteDatareader = sqliteCommand.ExecuteReader();
        while (sqliteDatareader.Read())
        {
            string name = sqliteDatareader.GetString(0);
            int age = sqliteDatareader.GetInt32(1);
            Console.WriteLine(name + ": " + age);
        }
        sqliteConnection.Close();
    }
}
