using ShotBall.InGame;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class CameraInspector : MonoBehaviour
    {
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

        public void OnClick()
        {
            Inspector.Instance.ValueSet(stageBlockData.StageBlockData, Name, unique);
        }

        public void ChangeData()
        {
            Debug.Log("CameraChange");
            cameraSize =stageBlockData.StageBlockData.GetFloatParameter("CameraSize");
            centerWorldWidth = stageBlockData.StageBlockData.GetFloatParameter("CenterWorldWidth");
            stage.SizeSet(cameraSize, centerWorldWidth);
        }
    }
}
