using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class StageNameUI : MonoBehaviour
    {
        [SerializeField] Text Text;

        private void Start()
        {
            Text.text = StageName.name;
        }
    }
}
