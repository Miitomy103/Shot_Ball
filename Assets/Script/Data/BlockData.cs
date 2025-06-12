using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ShotBall.InGame;
using System;

namespace ShotBall.Data
{
    [Serializable]
    public struct BlockData
    {
        public string GimickName;
        public Vector3 Position;
        public Vector3 Scale;
        public Quaternion Rotation;
        public Vector2 size;
        public Color color;
        public ObjectType Type;
        public BlockData[] childData;
        public string GimmickData;
        public BlockData(GameObject gameObject)
        {
            GimmickBase gimmick = gameObject.GetComponent<GimmickBase>();
            SpriteRenderer spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            ObjectBase objectBase = gameObject.GetComponent<ObjectBase>();
            Position = gameObject.transform.position;
            Scale = gameObject.transform.localScale;
            Rotation = gameObject.transform.rotation;
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
                GimmickData = string.Empty;
                GimickName = string.Empty;
            }
            else
            {
                GimickName = gimmick.Name;
                GimmickData = gimmick.FrameData();
                Debug.Log("GimmickData: " + GimmickData);
            }
        }
    }
    public enum ObjectType
    {
        Runtime,
        Editor,
        Children,
    }
}
