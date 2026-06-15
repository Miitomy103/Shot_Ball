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
                if (!stageBlockData.intParameters.ContainsKey(paramName))
                    break;

                keyNumbers.Add(stageBlockData.GetIntParameter(paramName));
            }


            return new BlockData(blockType, position, scale, rotation, size,blockId, color, type, childData, gimmickData,keyNumbers.ToArray());
        }
        public static StageBlockData ChangeStageBlockData(BlockData blockData)
        {
            StageBlockData stageBlockData = new StageBlockData();

            // 基本情報
            stageBlockData.SetType(blockData.BlockType);
            stageBlockData.SetVector3Parameter("Position", blockData.Position, false);
            stageBlockData.SetVector3Parameter("Scale", blockData.Scale, false);
            stageBlockData.SetVector3Parameter("Rotation", blockData.Rotation.eulerAngles, false);
            stageBlockData.SetIntParameter("BlockId", blockData.BlockId, false);
            stageBlockData.SetVector2Parameter("Size", blockData.size, false);

            // 色
            stageBlockData.SetFloatParameter("ColorR", blockData.color.r, false);
            stageBlockData.SetFloatParameter("ColorG", blockData.color.g, false);
            stageBlockData.SetFloatParameter("ColorB", blockData.color.b, false);
            stageBlockData.SetFloatParameter("ColorA", blockData.color.a, false);

            // 種別
            stageBlockData.SetBoolParameter("InFrame", blockData.Type == ObjectType.Runtime, false);

            // KeyNumbers
            for (int i = 0; i < blockData.BlockIds.Length; i++)
            {
                string paramName = "KeyNumber" + i;
                stageBlockData.SetIntParameter(paramName, blockData.BlockIds[i], false);
            }

            // 子データ（childData）の扱いは仕様次第
            // blockData.ChildData を StageBlockData[] に変換したい場合はここで再帰的に処理

            return stageBlockData;
        }

    }
}
