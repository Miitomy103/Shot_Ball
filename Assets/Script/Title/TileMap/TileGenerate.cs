using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace ShotBall.InGame
{
    [ExecuteInEditMode] // エディタ実行時にも動作
    public class TileGenerate : MonoBehaviour
    {
        public Tilemap tilemap;
        public TileBase tile;

        Vector2Int gridSize = new Vector2Int(12, 8);

        private TileBase[,] gridData;

        private Vector3Int origin;

        void OnEnable() // エディタでオブジェクトが有効化されたときに実行
        {
            if (!Application.isPlaying)
            {
                InitializeGrid();
            }
        }

        void InitializeGrid()
        {
            if (tilemap == null || tile == null) return;

            origin = tilemap.origin;
            gridData = new TileBase[gridSize.x, gridSize.y];

            tilemap.ClearAllTiles(); // 既存のタイルをクリア（再配置防止）

            //for (int x = 0; x < gridSize.x; x++)
            //{
            //    for (int y = 0; y < gridSize.y; y++)
            //    {
            //        Vector3Int tilePosition = new Vector3Int(origin.x + x, origin.y + y , 0);
            //        gridData[x, y] = tile;
            //        tilemap.SetTile(tilePosition, tile);
            //    }
            //}
        }
    }
}
