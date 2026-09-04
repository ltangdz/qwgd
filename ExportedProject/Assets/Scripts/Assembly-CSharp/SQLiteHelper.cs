using System;
using Mono.Data.Sqlite;
using UnityEngine;

public class SQLiteHelper
{
	private SqliteConnection dbConnection;

	private SqliteCommand dbCommand;

	private SqliteDataReader dataReader;

	public SQLiteHelper(string connectionString)
	{
		InitDB(connectionString);
	}

	private void InitDB(string connectionString)
	{
		try
		{
			dbConnection = new SqliteConnection(connectionString);
			dbConnection.Open();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.Message);
		}
	}

	public SqliteDataReader ExecuteQuery(string queryString)
	{
		dbCommand = dbConnection.CreateCommand();
		dbCommand.CommandText = queryString;
		try
		{
			dataReader = dbCommand.ExecuteReader();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.ToString());
			return null;
		}
		return dataReader;
	}

	public void CloseConnection()
	{
		if (dbCommand != null)
		{
			dbCommand.Cancel();
		}
		dbCommand = null;
		if (dataReader != null)
		{
			dataReader.Close();
		}
		dataReader = null;
		if (dbConnection != null)
		{
			dbConnection.Close();
		}
		dbConnection = null;
	}

	public SqliteDataReader ReadFullTable(string tableName)
	{
		string queryString = "SELECT * FROM " + tableName;
		return ExecuteQuery(queryString);
	}

	public SqliteDataReader InsertValues(string tableName, string[] values)
	{
		int fieldCount = ReadFullTable(tableName).FieldCount;
		if (values.Length != fieldCount)
		{
			throw new SqliteException("values.Length!=fieldCount");
		}
		string text = "INSERT INTO " + tableName + " VALUES (" + values[0];
		for (int i = 1; i < values.Length; i++)
		{
			text = text + ", " + values[i];
		}
		text += " )";
		return ExecuteQuery(text);
	}

	public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
	{
		if (colNames.Length != colValues.Length)
		{
			throw new SqliteException("colNames.Length!=colValues.Length");
		}
		string text = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
		for (int i = 1; i < colValues.Length; i++)
		{
			text = text + ", " + colNames[i] + "=" + colValues[i];
		}
		text = text + " WHERE " + key + operation + value;
		return ExecuteQuery(text);
	}

	public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
	{
		if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
		{
			throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
		}
		string text = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
		for (int i = 1; i < colValues.Length; i++)
		{
			text = text + "OR " + colNames[i] + operations[0] + colValues[i];
		}
		return ExecuteQuery(text);
	}

	public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
	{
		if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
		{
			throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
		}
		string text = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
		for (int i = 1; i < colValues.Length; i++)
		{
			text = text + " AND " + colNames[i] + operations[i] + colValues[i];
		}
		return ExecuteQuery(text);
	}

	public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
	{
		string text = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
		for (int i = 1; i < colNames.Length; i++)
		{
			text = text + ", " + colNames[i] + " " + colTypes[i];
		}
		text += "  ) ";
		return ExecuteQuery(text);
	}

	public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
	{
		string text = "SELECT " + items[0];
		for (int i = 1; i < items.Length; i++)
		{
			text = text + ", " + items[i];
		}
		text = text + " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
		for (int j = 0; j < colNames.Length; j++)
		{
			text = text + " AND " + colNames[j] + " " + operations[j] + " " + colValues[0] + " ";
		}
		return ExecuteQuery(text);
	}

	public SqliteDataReader ReadTableSql(string sql)
	{
		return ExecuteQuery(sql);
	}
}
