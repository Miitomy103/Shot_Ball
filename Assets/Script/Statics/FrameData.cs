using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{

    public struct FrameData : IEquatable<FrameData>
    {
        public Vector3 scale;
        public BlockType blockType;

        public FrameData(Vector3 s, BlockType b)
        {
            scale = s;
            blockType = b;
        }

        // HashSetで正しく比較されるために必要
        public override bool Equals(object obj)
        {
            return obj is FrameData data && Equals(data);
        }

        public bool Equals(FrameData other)
        {
            return scale == other.scale && blockType == other.blockType;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(scale, blockType);
        }

        // 任意の比較用関数（そのまま残してもOK）
        public bool SameData(FrameData data)
        {
            return Equals(data);
        }
    }

}
