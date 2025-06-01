using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public struct FrameData : IEquatable<FrameData>
    {
        public Vector3 scale;
        public string name;

        public FrameData(Vector3 s, string n)
        {
            scale = s;
            name = n;
        }

        // HashSetで正しく比較されるために必要
        public override bool Equals(object obj)
        {
            return obj is FrameData data && Equals(data);
        }

        public bool Equals(FrameData other)
        {
            return scale == other.scale && name == other.name;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(scale, name);
        }

        // 任意の比較用関数（そのまま残してもOK）
        public bool SameData(FrameData data)
        {
            return Equals(data);
        }
    }

}
