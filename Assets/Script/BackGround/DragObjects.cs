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
        void GetChildren(Transform parent)
        {
            //éqóvëfÇ™Ç¢Ç»ÇØÇÍÇŒèIóπ
            if (parent.childCount == 0)
            {
                return;
            }
            foreach (Transform ob in parent)
            {
                if(ob.TryGetComponent<DragObject>(out var run))
                {
                    objects.Add(run);
                }
                GetChildren(ob);
            }
        }

        public DragObject[] GetObjects() => objects.ToArray();
    }

}
