using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.Create
{
    [System.Serializable]
    public class CreateStageData 
    {
        public float CameraSize { get; private set; } = 5f; // Default camera size
        public float CenterWorldWidth { get; private set; } = 10f; // Default center world width
        public StageBlockData[] StageBlockDatas { get;private set; }
        public FrameData[] Frames { get; private set; } 
        public CreateStageData(StageBlockData[] stageBlockDatas, FrameData[] frames,float cameraSize,float centerWorldWidth)
        {
            this.StageBlockDatas = stageBlockDatas;
            this.Frames = frames;
            CameraSize = cameraSize;
            CenterWorldWidth = centerWorldWidth;
        }
    }
}
