using UnityEngine;

namespace ShotBall.Create
{
    public class StageNameView : MonoBehaviour,IName
    {
        public string Name => StageName.name;
    }
}
