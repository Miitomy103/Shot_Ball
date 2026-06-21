using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace ShotBall.InGame
{
    /// <summary>
    /// シーン上に存在する全てのオブジェクト(ObjectBase派生)を収集し、種類別に保持・検索できるようにするクラス。
    /// </summary>
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
        /// <summary>
        /// シーン上のObjectBaseを再収集し、種類別のリストを更新する。
        /// </summary>
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

        /// <summary>
        /// 再収集した上でDragObjectの一覧を返す。
        /// </summary>
        public DragObject[] GetDragObjects()
        {
            GetChildren(runtimeParent);
            return dragObjects.ToArray();
        }

        /// <summary>
        /// 再収集した上でEditorDragObjectの一覧を返す。
        /// </summary>
        public EditorDragObject[] GetEditorDragObjects()
        {
            GetChildren(runtimeParent);
            return editorObjects.ToArray();
        }

        /// <summary>
        /// 再収集した上で全ObjectBaseの一覧を返す。
        /// </summary>
        public ObjectBase[] GetAllObjects()
        {
            GetChildren(runtimeParent);
            return allObjects.ToArray();
        }

        /// <summary>
        /// 指定したBlockIdを持つObjectBaseを検索する。
        /// </summary>
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
