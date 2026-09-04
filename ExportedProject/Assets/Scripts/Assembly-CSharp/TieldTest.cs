using UnityEngine;
using UnityEngine.Tilemaps;

public class TieldTest : MonoBehaviour
{
	public Tilemap _tileMap;

	private void Start()
	{
		TileBase[] tilesBlock = _tileMap.GetTilesBlock(default(BoundsInt));
		new TilemapRenderer();
		TileBase[] array = tilesBlock;
		for (int i = 0; i < array.Length; i++)
		{
			Debug.Log(array[i]);
		}
	}
}
