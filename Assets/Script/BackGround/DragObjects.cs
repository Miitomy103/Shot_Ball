using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DragObjects : MonoBehaviour
    {
        List<GameObject> objects = new List<GameObject>();
        [SerializeField] Transform runtimeParent;
        private void Awake()
        {
            GetChildren(runtimeParent);
        }
        void GetChildren(Transform parent)
        {
            Transform children = parent.GetComponentInChildren<Transform>();
            //éqóvëfÇ™Ç¢Ç»ÇØÇÍÇŒèIóπ
            if (children.childCount == 0)
            {
                return;
            }
            foreach (Transform ob in children)
            {
                if(ob.GetComponent<RuntimeDragObject>())
                {
                    objects.Add(ob.gameObject);
                }
                GetChildren(ob);
            }
        }

        public GameObject[] GetObjects() => objects.ToArray();
    }

}
