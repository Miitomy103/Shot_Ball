using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ShotBall.Create
{
    public class Connect : MonoBehaviour
    {
        public int limit=1;
        public BlockType[] blockTypes;


        public List<ConnectUI> connectUIs = new List<ConnectUI>();

        private void Start()
        {
            InputSystem.Instance.CameraScroll += UpdateLine;
            InputSystem.Instance.CameraDrag += UpdateLine;
            GetComponent<CreateObject>().DragObject += UpdateLine;
        }

        
        void UpdateLine()
        {
            if (connectUIs.Count == 0) return;
            foreach (var c in connectUIs)
            {
                if(c.lineUI.gameObject.activeSelf == false) return;
            }
            foreach (var connect in connectUIs)
            {
                if (connect.toPoint == null || connect.lineUI == null) continue;
                if(ConnectionManager.Instance == null) continue;
                
                ConnectionManager.Instance.LineSet(Camera.main.WorldToScreenPoint(connect.createObject.transform.position), connect.toPoint,connect.lineUI);
            }
            Debug.Log("UpdateLine");
        }
    }
    [System.Serializable]
    public class ConnectUI
    {
        public RectTransform toPoint;
        public RectTransform lineUI;
        public CreateObject createObject;

        public RectTransform[] images=>new RectTransform[] { toPoint, lineUI };

        public ConnectUI(RectTransform toPoint, RectTransform lineUI)
        {
            this.toPoint = toPoint;
            this.lineUI = lineUI;
        }
        public void SetActive(bool isActive)
        {
            foreach (var img in images)
            {
                img.gameObject.SetActive(isActive);
            }
        }
    }
}
