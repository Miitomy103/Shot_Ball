using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    public class DragObjects : MonoBehaviour
    {
        public static DragObjects Instance => instance;
        static DragObjects instance;

        [SerializeField] List<EditorDragObject>  editorObjects = new List<EditorDragObject>();
        [SerializeField] List<DragObject> dragObjects = new List<DragObject>();
        [SerializeField] List<ObjectBase> allObjects = new List<ObjectBase>();
        [SerializeField] Transform runtimeParent;
        public Transform RuntimeParent => runtimeParent;
        private void Awake()
        {
            instance = this;
            GetChildren(runtimeParent);
        }
        IEnumerator AStart()
        {
            yield return new WaitForSeconds(4.5f);
            GetChildren(runtimeParent);
        }
        public void GetChildren(Transform parent)
        {
            editorObjects.Clear();
            dragObjects.Clear();
            allObjects.Clear();

            ObjectBase[] components = FindObjectsOfType<ObjectBase>();
            foreach (var comp in components)
            {
                // シーンに存在し、非Editor用のものだけを対象にする
                if (comp.gameObject.hideFlags == HideFlags.None && comp.gameObject.scene.IsValid())
                {
                    allObjects.Add(comp);
                    if (comp is DragObject drag)
                    {
                        dragObjects.Add(drag);
                    }
                    if (comp is EditorDragObject editorDrag)
                    {
                        editorObjects.Add(editorDrag);
                    }
                }
            }
        }

        public DragObject[] GetDragObjects()
        {
            GetChildren(runtimeParent);
            return dragObjects.ToArray();
        }

        public EditorDragObject[] GetEditorDragObjects()
        {
            GetChildren(runtimeParent);
            return editorObjects.ToArray();
        }

        public ObjectBase[] GetAllObjects()
        {
            GetChildren(runtimeParent);
            return allObjects.ToArray();
        }

        public ObjectBase GetBlockId(int id)
        {
            foreach (var obj in allObjects)
            {
                if (obj.BlockId == id) return obj;
            }
            return null;
        }
    }

}
