using ShotBall.InGame;
using System;
using UnityEngine;

namespace ShotBall
{
    /// <summary>
    /// 1ギミックのデータ
    /// </summary>
    [Serializable]
    public struct BlockData
    {
        public BlockType BlockType;
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
        public int BlockId;
        public Vector2 size;
        public Color color;
        public ObjectType Type;
        public BlockData[] childData;
        public string GimmickData;
        public int[] BlockIds;
        public BlockData(GameObject gameObject)
        {
            GimmickBase gimmick = gameObject.GetComponent<GimmickBase>();
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            ObjectBase objectBase = gameObject.GetComponent<ObjectBase>();
            Position = gameObject.transform.position;
            Scale = gameObject.transform.localScale;
            Rotation = gameObject.transform.rotation;
            BlockId = objectBase.BlockId;
            size = spriteRenderer.size;
            color = spriteRenderer.color;
            Type = objectBase switch
            {
                RuntimeDragObject => ObjectType.Runtime,
                EditorDragObject => ObjectType.Editor,
                ChildrenDragObject => ObjectType.Children,
                _ => throw new InvalidOperationException("Unknown object type.")
            };
            if(objectBase is ChildrenDragObject childrenDragObject)
            {
                childData = new BlockData[childrenDragObject.transform.childCount];
                for (int i = 0; i < childrenDragObject.transform.childCount; i++)
                {
                    childData[i] = new BlockData(childrenDragObject.transform.GetChild(i).gameObject);
                }
            }
            else
            {
                childData = null;
            }
            if(gimmick == null)
            {
                GimmickData = null;
                BlockType = BlockType.Block;
            }
            else
            {
                BlockType = gimmick.BlockType;
                GimmickData = gimmick.FrameData();
                Debug.Log("GimmickData: " + GimmickData);
            }
            BlockIds = null;
        }
        public BlockData(BlockType blockType, Vector3 position, Vector3 scale, Quaternion rotation, Vector2 size, int blockId,Color color, ObjectType type, BlockData[] childData, string gimmickData, int[] blockIds)
        {
            BlockType = blockType;
            Position = position;
            Scale = scale;
            Rotation = rotation;
            BlockId = blockId;
            this.size = size;
            this.color = color;
            Type = type;
            this.childData = childData;
            GimmickData = gimmickData;
            BlockIds = blockIds;
        }
    }
    public enum ObjectType
    {
        Runtime,
        Editor,
        Children,
    }
}
