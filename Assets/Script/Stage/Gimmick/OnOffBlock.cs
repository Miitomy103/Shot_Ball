using System.Linq;
using TMPro;
using UnityEngine;

namespace ShotBall.InGame
{
    public class OnOffBlock : LineManager
    {
        [SerializeField]SerializeIOnOff[] iOnOffs;
        IOnOff[] IOnOffs => iOnOffs.Select(x => x.Interface).ToArray();
        protected Transform[] Targets => IOnOffs.Select(t => t.Transform).ToArray();
        protected override Transform StartPoint => transform;
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

            lineDatas.Clear();

            foreach (Transform t in transform)
            {
                if (t.name == "Line") Destroy(t.gameObject);
            }

            for (int i = 0; i < Targets.Length; i++)
            {
                CreateLine(Targets[i]);
            }

            // ‚·‚×‚Ä‚Ìƒ‰ƒCƒ“‚ªì¬‚³‚ê‚Ä‚©‚ç OnOffChange ‚ðŒÄ‚Ô
            OnOffChange(startIsOn);
        }

        private void Update()
        {
            if (GameLoop.StageState != StageState.Setting) return;

            if (InFrame)
            {
                DisableAllLines();
                return;
            }
            else
            {
                EnableAllLines();
            }

            if (lineDatas.Count < Targets.Length)
            {
                Debug.LogWarning("lineDatas.Count is less than Targets.Length. Skipping ChangePosition.");
                return;
            }

            for (int i = 0; i < Targets.Length; i++)
            {
                ChangePosition(i);
            }
        }

        private void OnDestroy()
        {
            for (int i = 0; i < lineDatas.Count; i++) lineDatas.Remove(lineDatas[i]);

            foreach (Transform t in transform)
            {
                if (t.name == "Line") Destroy(t.gameObject);
            }
        }
        protected override void StageReset()
        {
            base.StageReset();
            Debug.Log("StageReset: OnOffBlock");
            OnOffChange(startIsOn);
        }
        protected override void BallEnter(Collider2D collider)
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
            foreach (var i in IOnOffs)
            {
                if (on)
                    i.ItOn();
                else
                    i.ItOff();
            }

        }

        protected override string StringData()
        {
            string a = "";
            foreach(var i in Targets)
            {
                a += i.name;
            }
            return startIsOn.ToString()+a;
        }
    }
}
