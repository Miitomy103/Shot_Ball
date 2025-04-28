
//using ShotBall.InGame;
//#if UNITY_EDITOR
//using UnityEngine;
//using UnityEditor;

//[CustomEditor(typeof(EditorDragObject))]
//public class EditorDraggableObjectEditor : Editor
//{
//    private bool isDragging = false;
//    private Vector3 offset;

//    void OnSceneGUI()
//    {
//        Event e = Event.current;
//        EditorDragObject draggable = (EditorDragObject)target;

//        // オブジェクトの位置を取得・表示
//        Vector3 pos = draggable.transform.position;

//        // ドラッグ開始検出
//        if (e.type == EventType.MouseDown && e.button == 0)
//        {
//            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
//            if (Physics.Raycast(ray, out RaycastHit hit))
//            {
//                if (hit.transform == draggable.transform)
//                {
//                    isDragging = true;
//                    offset = draggable.transform.position - hit.point;
//                    e.Use(); // イベントを消費
//                }
//            }
//        }

//        // ドラッグ中
//        if (isDragging && e.type == EventType.MouseDrag)
//        {
//            Ray ray = HandleUtility.GUIPointToWorldRay(e.mousePosition);
//            if (Physics.Raycast(ray, out RaycastHit hit))
//            {
//                draggable.transform.position = hit.point + offset;
//                e.Use();
//            }
//        }

//        // ドラッグ終了
//        if (e.type == EventType.MouseUp)
//        {
//            isDragging = false;
//        }
//    }
//}
//#endif
