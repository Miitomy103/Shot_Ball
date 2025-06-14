using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DragObjects : MonoBehaviour
    {
        [SerializeField] List<DragObject> objects = new List<DragObject>();
        [SerializeField] Transform runtimeParent;
        private void Awake()
        {
            GetChildren(runtimeParent);
        }
        public void GetChildren(Transform parent)
        {
            DragObject[] components = Resources.FindObjectsOfTypeAll<DragObject>();
            foreach (var comp in components)
            {
                // シーンに存在し、非Editor用のものだけを対象にする
                if (comp.gameObject.hideFlags == HideFlags.None && comp.gameObject.scene.IsValid())
                {
                    objects.Add(comp);
                }
            }
        }

        public DragObject[] GetObjects() => objects.ToArray();
    }

}
