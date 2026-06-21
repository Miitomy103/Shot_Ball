using UnityEngine;

namespace ShotBall.Create
{
    /// <summary>
    /// StageName.nameをIName経由で公開するクラス。
    /// </summary>
    public class StageNameView : MonoBehaviour,IName
    {
        public string Name => StageName.name;
    }
}
