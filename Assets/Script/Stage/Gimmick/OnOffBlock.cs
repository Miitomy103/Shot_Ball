using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
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

        [SerializeField] bool startIsOn;

        [SerializeField] GameObject onObject;
        [SerializeField] GameObject offObject;

        protected override void Start()
        {
            base.Start();
            OnOffChange(startIsOn);
            for(int i=0;i<Targets.Length;i++)
            {
                CreateLine(Targets[i]);
            }
        }
        private void Update()
        {
            if (GameLoop.StageState != StageState.Setting) return;
            for(int i=0; i<Targets.Length;i++)
            {
                ChangePosition(i);
            }
        }
        protected override void BallEnter(Collision2D collision)
        {
            base.BallEnter(collision);
            OnOffChange(!isOn);
        }
        private void OnOffChange(bool on)
        {
            isOn = on;
            foreach (var i in IOnOffs)
            {
                if (on)
                    i.ItOn();
                else
                    i.ItOff();
            }
            onObject.SetActive(on);
            offObject.SetActive(!on);
        }
    }
}
