using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ShotBall.InGame
{
    public class StageNameView : MonoBehaviour,IName
    {
        public string Name => Create.StageName.name;
    }
}
