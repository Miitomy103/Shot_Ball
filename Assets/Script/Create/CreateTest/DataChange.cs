using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

namespace ShotBall.InGame
{
    public static class DataChange
    {
        public static BlockData ChangeBlockData(StageBlockData stageBlockData)
        {
            BlockType blockType = stageBlockData.Type;
            Vector3 position = stageBlockData.GetVector3Parameter("Position");
            Vector3 scale = stageBlockData.GetVector3Parameter("Scale");
            Quaternion rotation = Quaternion.Euler(stageBlockData.GetVector3Parameter("Rotation"));
            int blockId=stageBlockData.GetIntParameter("BlockId");
            Vector2 size = stageBlockData.GetVector2Parameter("Size");
            float r = stageBlockData.GetFloatParameter("ColorR");
            float g = stageBlockData.GetFloatParameter("ColorG");
            float b = stageBlockData.GetFloatParameter("ColorB");
            float a = stageBlockData.GetFloatParameter("ColorA");
            Color color = new Color(r, g, b, a);
            ObjectType type = stageBlockData.GetBoolParameter("InFrame") ? ObjectType.Runtime: ObjectType.Editor;
            BlockData[] childData = null; // This would need to be populated based on your specific requirements
            string gimmickData = StageDataGimmickData.GetGimmickData(stageBlockData);

            List<int> keyNumbers = new List<int>();

            keyNumbers.Clear();
            for (int i = 0; ; i++)
            {
                string paramName = "KeyNumber" + i;
                if (!stageBlockData.intParametors.ContainsKey(paramName))
                    break;

                keyNumbers.Add(stageBlockData.GetIntParameter(paramName));
            }


            return new BlockData(blockType, position, scale, rotation, size,blockId, color, type, childData, gimmickData,keyNumbers.ToArray());
        }
    }
}
