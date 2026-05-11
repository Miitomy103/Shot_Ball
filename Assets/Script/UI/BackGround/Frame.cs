using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

        private void Awake()
        {
            Sprite = GetComponent<SpriteRenderer>();
        }
        private void Start()
        {
            Sprite.sortingOrder = (int)OrderInLayer.Frame;
        }
        public void MouseDown()
        {
            if (ObjectCount <= 0||GameLoop.StageState!=StageState.Setting) return;

            GameObject game = Instantiate(FrameObject, new Vector3(transform.position.x,transform.position.y,FrameObject.transform.position.z), FrameObject.transform.rotation);
            
            game.transform.parent=DragObjects.Instance.RuntimeParent;
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
            GameObject copy = Instantiate(drag.gameObject,DragObjects.Instance.RuntimeParent);
            FrameObject = copy.gameObject;
            //コピーを表示しないようにする
            copy.SetActive(false);
            Data = drag.FrameData;
        }
        public void FrameSpriteIn(GameObject dragObject)
        {
            SpriteRenderer[] all = dragObject.GetComponentsInChildren<SpriteRenderer>();
            int c = 0;
            foreach(var s in all)
            {
                if (s.name != "SubSprite")
                {
                    s.sortingOrder = (int)OrderInLayer.DragNow+c;
                    c++;
                }
                else
                {
                    s.sortingOrder = -100;
                }
            }
            Debug.Log(c + "dragObject.childSpriteCount"+dragObject.name);
            //dragObject.GetComponent<IFitSpriteInSquare>().FitSprite(Sprite);
            if(dragObject.TryGetComponent<IFitSpriteInSquare>(out var drag))
            {
                drag.FitSprite(Sprite);
            }


            Vector3 pos = transform.position;
            //dragObject.transform.position = new Vector3(pos.x, pos.y, dragObject.transform.position.z);

            Component[] components = dragObject.GetComponents<Component>();
            foreach (Component comp in components)
            {
                if (comp is SpriteRenderer)
                {
                    continue;
                }
                if (comp is Transform) continue;

                Destroy(comp);
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
