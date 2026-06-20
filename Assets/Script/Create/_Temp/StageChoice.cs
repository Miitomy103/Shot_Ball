using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class StageChoice : MonoBehaviour
    {
        [SerializeField] Image[] stageImages;
        private void Start()
        {
            string folder = Application.persistentDataPath; // ï€ë∂èÍèä
            List<(string fileName, SaveData data)> allStages = StageDataLoader.LoadAllStages(folder);


        }
    }
}
