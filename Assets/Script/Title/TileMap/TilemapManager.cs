using System;
using UnityEngine;
using UnityEngine.Tilemaps;
using Unity.Collections;
using UnityEngine.UIElements;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace ShotBall.InGame
{
    [ExecuteAlways] // エディター時でも `Awake` や `Start` を実行
    public class TilemapManager : MonoBehaviour
    {
        public Tilemap tilemap;

        static TilemapManager instance;
        public static TilemapManager Instance => instance;

        [SerializeField] TileColors[] tileColors;

        // tile状態
        // 0 => 空
        // 1 => 固定オブジェクト
        // 2 => 設置オブジェクト

        private int[,] tileData = new int[8, 12]; // 修正: プロパティではなくフィールドとして宣言

        [SerializeField] private Vector3Int origin;

        private void Awake()
        {
            instance = this;
            foreach(var t in tileColors)
            {
                t.Initialize();
            }
        }

        private void Start()
        {
            if (tilemap != null)
            {
                origin = tilemap.origin;
            }
        }


        void EditorCheck()
        {
            try
            {
                for (int x = 0; x < tileData.GetLength(1); x++)
                {
                    for (int y = 0; y < tileData.GetLength(0); y++)
                    {
                        Debug.Log("EditorCheck");
                        if (tileData[y, x] != 0)
                        {
                            tilemap.SetColor(new Vector3Int(x + origin.x, y + origin.y, 0), TileColorMode().GetColor(TileColor.Change));
                        }
                        else
                        {
                            tilemap.SetColor(new Vector3Int(x + origin.x, y + origin.y, 0), TileColorMode().GetColor(TileColor.Original));
                        }
                    }
                }
            }
            catch
            {

            }
        }

        public bool DropCheck(int[,] gridData,Vector3 worldPoint)
        {
            Vector3Int worldPos = tilemap.WorldToCell(worldPoint);

            try
            {
                if (tileData[worldPos.y - origin.y, worldPos.x - origin.x] != 0) return false;

                Vector3Int sidePos = SidePosition(gridData, worldPos);

                int gridY = gridData.GetLength(0); // 行数 → 縦方向（Y）
                int gridX = gridData.GetLength(1); // 列数 → 横方向（X）

                for (int x = 0; x < gridX; x++)
                {
                    for (int y = 0; y < gridY; y++)
                    {
                        int flippedY = gridY - 1 - y; // ← ここでY反転

                        if (gridData[flippedY, x] > 0)
                        {
                            int checkX = sidePos.x + x - origin.x;
                            int checkY = sidePos.y + y - origin.y;

                            if (checkY < 0 || checkY >= tileData.GetLength(0) || checkX < 0 || checkX >= tileData.GetLength(1))
                            {
                                Debug.Log("false");
                                return false;
                            }

                            if (tileData[checkY, checkX] > 0)
                            {
                                Debug.Log("すでに何かがあります");
                                return false;
                            }
                        }
                    }
                }
                return true;
            }
            catch (IndexOutOfRangeException)
            {
                Debug.Log("範囲外です" + (origin.x - worldPos.x) + ":" + (origin.y - worldPos.y));
                return false;
            }
        }

        public void Drop(Transform trans, int[,] gridData,Vector3 worldPoint,Vector2 pivot)
        {
            worldPoint = new Vector3(worldPoint.x - pivot.x, worldPoint.y - pivot.y, worldPoint.z);
            Debug.Log("Drop()");
            Vector3Int gridPosition = tilemap.WorldToCell(worldPoint);

            Vector2 position = GetCellCenterWorld(gridPosition);
            trans.position = new Vector3(position.x + pivot.x, position.y + pivot.y + 0);

            TileDetaChange(gridData,1,worldPoint);
        }

        public void TileDetaChange(int[,]gridData,int value,Vector3 worldPoint)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(worldPoint);

            Vector3Int sidePos = SidePosition(gridData, gridPosition);

            int gridY = gridData.GetLength(0); // 行数（縦）
            int gridX = gridData.GetLength(1); // 列数（横）

            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    if (gridData[y, x] > 0)
                    {
                        int targetX = sidePos.x + x - origin.x;
                        int targetY = sidePos.y + y - origin.y;

                        tileData[targetY, targetX] = value;
                    }
                }
            }
            EditorCheck();
        }

        public void TileColorChange(Vector3 worldPoint,TileColor tileColor)
        {
            Vector3Int gridPosition = tilemap.WorldToCell(worldPoint);

            tilemap.SetColor(gridPosition, TileColorMode().GetColor(tileColor));

            Debug.Log(gridPosition + "" + tileColor);
        }

        Vector3Int SidePosition(int[,] gridData, Vector3Int worldPos)
        {
            if (gridData == null)
            {
                Debug.LogError("gridDataがnullだよ！");
                return Vector3Int.zero;
            }

            Debug.Log($"gridDataサイズ: {gridData.GetLength(0)} x {gridData.GetLength(1)}");

            int gridY = gridData.GetLength(0);
            int gridX = gridData.GetLength(1);

            for (int x = 0; x < gridX; x++)
            {
                for (int y = 0; y < gridY; y++)
                {
                    int flippedY = gridY - 1 - y;
                    if (flippedY < 0 || flippedY >= gridY || x < 0 || x >= gridX)
                    {
                        Debug.LogError($"範囲外アクセス検知！flippedY:{flippedY} x:{x}");
                        continue;
                    }

                    if (gridData[flippedY, x] == 2)
                    {
                        return new Vector3Int(worldPos.x - x, worldPos.y - y);
                    }
                }
            }
            return Vector3Int.zero;
        }


        Vector2 GetCellCenterWorld(Vector3Int cellPosition)
        {
            Vector3 cellBottomLeft = tilemap.CellToWorld(cellPosition);
            Vector3 cellSize = tilemap.cellSize;

            return cellBottomLeft + new Vector3(cellSize.x / 2, cellSize.y / 2, 0);
        }

        public TileColors TileColorMode()
        {
            if (NightMode.Night) return tileColors[0];
            return tileColors[1];
        }
    }


}
