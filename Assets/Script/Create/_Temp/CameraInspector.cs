using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    /// <summary>
    /// インスペクターでカメラサイズ・中央エリア幅を設定し、StageBlockData/Stageに反映するクラス。
    /// </summary>
    public class CameraInspector : MonoBehaviour
    {
        private const float MinCameraSize = 3f;
        private const float MaxCameraSize = 14f;
        private const float MinCenterWorldWidth = 4f;
        private const float MaxCenterWorldWidth = 14.8f;

        [SerializeField] Button button;

        [SerializeField] string Name;
        [SerializeField] string unique;

        [SerializeField] Stage stage;

        IStageBlockData stageBlockData;

        float cameraSize;
        float centerWorldWidth;
        private void Awake()
        {
            stageBlockData = GetComponent<IStageBlockData>();
            button.onClick.AddListener(OnClick);
        }
        private void Start()
        {
            stageBlockData.StageBlockData.floatSet += ChangeData;
        }

        /// <summary>
        /// このブロックの値をInspectorに表示する。
        /// </summary>
        public void OnClick()
        {
            Inspector.Instance.ValueSet(stageBlockData.StageBlockData, Name, unique);
        }

        /// <summary>
        /// StageBlockDataのCameraSize/CenterWorldWidthが変更されたときに、値を範囲内にクランプしてStageに反映する。
        /// </summary>
        public void ChangeData()
        {
            Debug.Log("CameraChange");

            float cameraSize = Mathf.Clamp(
                stageBlockData.StageBlockData.GetFloatParameter("CameraSize"),
                MinCameraSize, MaxCameraSize);

            if (!Mathf.Approximately(cameraSize, stageBlockData.StageBlockData.GetFloatParameter("CameraSize")))
            {
                stageBlockData.StageBlockData.SetFloatParameter("CameraSize", cameraSize);
            }

            float centerWorldWidth = Mathf.Clamp(
                stageBlockData.StageBlockData.GetFloatParameter("CenterWorldWidth"),
                MinCenterWorldWidth, MaxCenterWorldWidth);

            if (!Mathf.Approximately(centerWorldWidth, stageBlockData.StageBlockData.GetFloatParameter("CenterWorldWidth")))
            {
                stageBlockData.StageBlockData.SetFloatParameter("CenterWorldWidth", centerWorldWidth);
            }

            this.cameraSize = cameraSize;
            this.centerWorldWidth = centerWorldWidth;
            stage.SizeSet(cameraSize, centerWorldWidth);
        }



        private void OnDestroy()
        {
            stageBlockData.StageBlockData.floatSet -= ChangeData;
        }

    }
}
