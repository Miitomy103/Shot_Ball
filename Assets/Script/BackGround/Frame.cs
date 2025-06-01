using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class Frame : MonoBehaviour
    {
        GameObject FrameObject;
        GameObject frameInObject;

        int objectMaxCount;
        public int ObjectCount;
        public SpriteRenderer Sprite { get; private set; }

        [SerializeField] TextMeshPro text;

        public FrameData Data { get; set; }

        public bool inFrame { get; private set; }

        private FrameObjectData objectData;

        private void Awake()
        {
            objectData = GetComponent<FrameObjectData>();
            Sprite = GetComponent<SpriteRenderer>();
        }
        public void MouseDown()
        {
            if (ObjectCount <= 0) return;

            GameObject game = Instantiate(FrameObject, transform.position, FrameObject.transform.rotation);
            game.SetActive(true);
            PutOut();
            DragObject dragObject = game.GetComponent<DragObject>();
            dragObject.frameScript = this;
            dragObject.StartDrag(Input.mousePosition);
        }
        public void Inialize(DragObject drag)
        {
            //自分の子どもに見せる用にゲームオブジェクトを生成してコンポーネントをすべて消して置く
            frameInObject = Instantiate(drag.gameObject,transform);
            FrameSpriteIn(frameInObject);
            //いつでもコピーが出せるように生成する
            GameObject copy = Instantiate(drag.gameObject);
            FrameObject = copy.gameObject;
            //コピーを表示しないようにする
            copy.SetActive(false);
            Data = new FrameData(drag.OriginalSize, drag.Name);
        }
        public void FrameSpriteIn(GameObject dragObject)
        {
            dragObject.GetComponent<DragObject>().FitSpriteInSquare(Sprite);

            Vector3 pos = transform.position;
            //dragObject.transform.position = new Vector3(pos.x, pos.y, dragObject.transform.position.z);

            Component[] components = dragObject.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp is SpriteRenderer sprite) sprite.sortingOrder = Sprite.sortingOrder + 1;
                if (comp is Transform || comp is SpriteRenderer)continue;
                DestroyImmediate(comp);
            }
        }
        public bool IsFrame(FrameData frameData)
        {
            return Data.SameData(frameData);
        }

        public void PutIn()
        {
            ObjectCount++;
            text.text = ObjectCount.ToString();
            if(ObjectCount>=1)
            {
                frameInObject.SetActive(true);
            }
        }

        public void PutOut()
        {
            ObjectCount--;
            text.text = ObjectCount.ToString();
            if (ObjectCount<=0)
            {
                frameInObject.SetActive(false);
            }
        }
    }
}
