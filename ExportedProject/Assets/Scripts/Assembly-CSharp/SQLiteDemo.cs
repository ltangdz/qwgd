using Mono.Data.Sqlite;
using UnityEngine;

public class SQLiteDemo : MonoBehaviour
{
	private SQLiteHelper sql;

	private void Start()
	{
		sql = new SQLiteHelper("data source=" + Application.dataPath + "/cybermanhunt.db");
		SqliteDataReader sqliteDataReader = sql.ReadFullTable("entry");
		while (sqliteDataReader.Read())
		{
			Debug.Log(sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("id")));
		}
		sqliteDataReader = sql.ReadTable("entry", new string[1] { "id" }, new string[1] { "id" }, new string[1] { ">=" }, new string[1] { "'25'" });
		sqliteDataReader = sql.ReadTableSql("select * from entry where id='10001'");
		while (sqliteDataReader.Read())
		{
			Debug.Log(sqliteDataReader.GetInt32(sqliteDataReader.GetOrdinal("id")));
		}
		sql.CloseConnection();
	}
}
