using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{

    /// <summary>
    /// フレームに置いたオブジェクトの元のスケール・サイズ・ブロック種別を保持する構造体。
    /// </summary>
    public struct FrameData : IEquatable<FrameData>
    {
        public Vector3 scale;
        public Vector2 size;
        public BlockType blockType;

        public FrameData(Vector3 s, Vector2 size, BlockType b)
        {
            scale = s;
            this.size = size;
            blockType = b;
        }

        // HashSetで正しく比較されるために必要
        public override bool Equals(object obj)
        {
            return obj is FrameData data && Equals(data);
        }

        public bool Equals(FrameData other)
        {
            return scale == other.scale && size == other.size && blockType == other.blockType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(scale, size, blockType);
        }

        // 任意の比較用関数（そのまま残してもOK）
        public bool SameData(FrameData data)
        {
            return Equals(data);
        }
    }

}
