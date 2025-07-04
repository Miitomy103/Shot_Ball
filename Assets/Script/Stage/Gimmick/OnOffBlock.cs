using System.Linq;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class OnOffBlock : AreaBase
    {
        [SerializeField]public OnOff[] iOnOffs;
        protected Transform[] Targets => iOnOffs.Select(t => t.transform).ToArray();

        LineManager lineManager;
        public bool isOn { get; private set; }

        public override string Name => "OnOffBlock";

        [SerializeField] bool startIsOn;

        [SerializeField] GameObject onObject;
        [SerializeField] GameObject offObject;

        SpriteRenderer[] childSprites;
        TextMeshPro[] childMeshProes;

        protected override void Awake()
        {
            base.Awake();
            lineManager = GetComponent<LineManager>();

            GameObject[] objs = new GameObject[] { offObject, onObject };
            childSprites = new SpriteRenderer[objs.Length];
            childMeshProes = new TextMeshPro[objs.Length];
            for(int i=0;i<objs.Length;i++)
            {
                childSprites[i] = objs[i].GetComponent<SpriteRenderer>();
                childMeshProes[i] = objs[i].transform.GetChild(0).GetComponent<TextMeshPro>();
            }
            
        }
        protected override void Start()
        {
            base.Start();

            foreach (Transform t in transform)
            {
                if (t.name == "Line") Destroy(t.gameObject);
            }

            for (int i = 0; i < Targets.Length; i++)
            {
                lineManager.CreateLine(Targets[i]);
            }
            lineManager.EnableAllLines(true);

            // ‚·‚×‚Ä‚Ìƒ‰ƒCƒ“‚ªì¬‚³‚ê‚Ä‚©‚ç OnOffChange ‚ðŒÄ‚Ô
            OnOffChange(startIsOn);
        }

        private void OnDestroy()
        {
            foreach (Transform t in transform)
            {
                if (t.name == "Line") Destroy(t.gameObject);
            }
        }
        protected override void Drag()
        {
            base.Drag();
            lineManager.EnableAllLines(true);
            for (int i = 0; i < Targets.Length; i++)
            {
                lineManager.ChangePosition(i);
            }
        }
        protected override void StageReset()
        {
            base.StageReset();
            Debug.Log("StageReset: OnOffBlock");
            OnOffChange(startIsOn);
        }
        protected override void BallEnter(Ball collider)
        {
            base.BallEnter(collider);
            OnOffChange(!isOn);
        }
        protected override void DragStart()
        {
            base.DragStart();
            for(int i=0;i<childSprites.Length;i++)
            {
                childSprites[i].sortingOrder = (int)OrderInLayer.DragNow;
                childMeshProes[i].sortingOrder = (int)OrderInLayer.DragNow;
            }
        }
        protected override void DragEnd()
        {
            base.DragEnd();
            for (int i = 0; i < childSprites.Length; i++)
            {
                childSprites[i].sortingOrder = (int)OrderInLayer.NoDrag;
                childMeshProes[i].sortingOrder = (int)OrderInLayer.NoDrag;
            }
        }
        private void OnOffChange(bool on)
        {
            isOn = on;
            if (onObject != null && offObject != null)
            {
                onObject.SetActive(on);
                offObject.SetActive(!on);
            }
            foreach (var i in iOnOffs)
            {
                if (on)
                    i.ItOn();
                else
                    i.ItOff();
            }

        }

        protected override string StringData()
        {
            return startIsOn.ToString();
        }
    }
}
