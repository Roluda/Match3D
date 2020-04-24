using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Visuals;

namespace NinjaCactus.Gamelogic {
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Renderer))]
    public class Matchable : MonoBehaviour {
        int typeBuffer = 0;
        public int type {
            get {
                return typeBuffer;
            }
            set {
                typeBuffer = value;
                ApplyColor(colorMap.GetColor(value));
            }
        }

        [SerializeField]
        ColorMap colorMap = default;
        [HideInInspector]
        public bool isMatch;
        [HideInInspector]
        public Matchable left;
        [HideInInspector]
        public Matchable right;
        [HideInInspector]
        public Matchable back;
        [HideInInspector]
        public Matchable front;
        [HideInInspector]
        public Matchable top;
        [HideInInspector]
        public Matchable bottom;


        Renderer render;
        Collider col;
        public bool isActive = true;

        void Activate() {
            isActive = true;
            col.enabled = true;
        }

        void Deactivate() {
            isActive = false;
            col.enabled = false;
        }

        void CheckMatches() {
            if(AnyMatch()) {
                isMatch = true;
            } else {
                isMatch = false;
            }
        }

        private void Update() {
            CheckMatches();
            if (isActive) {
                render.material.SetFloat("_LastActive", Time.time);
            }
        }

        private void LateUpdate() {
            if (isActive && isMatch) {
                Deactivate();
            } else if(!isActive && !isMatch) {
                Activate();
            }
        }

        public void Highlight() {
            render.material.SetFloat("_Highlighted", 1);
        }

        public void StopHighlight() {
            render.material.SetFloat("_Highlighted", 0);
        }

        void ApplyColor(Color color) {
            render.material.SetColor("_OldColor", render.material.GetColor("_BaseColor"));
            render.material.SetColor("_BaseColor", color);
            render.material.SetFloat("_ColorChanged", Time.time);
        }

        public bool IsNeighbor(Matchable candidate) {
            return (candidate == top 
                || candidate == bottom 
                || candidate == left 
                || candidate == right 
                || candidate == front 
                || candidate == back);
        }

        private void Awake() {
            render = GetComponent<Renderer>();
            col = GetComponent<Collider>();
        }

        public bool AnyMatch() {
            return HorizontalMatch() || VerticalMatch() || DepthMatch();
        }

        public bool VerticalMatch() {
            return (TopMatch() && top.TopMatch()) || (BottomMatch() && bottom.BottomMatch()) || (TopMatch() && BottomMatch());
        }

        public bool HorizontalMatch() {
            return (LeftMatch() && left.LeftMatch()) || (RightMatch() && right.RightMatch()) || (LeftMatch() && RightMatch());
        }

        public bool DepthMatch() {
            return (FrontMatch() && front.FrontMatch()) || (BackMatch() && back.BackMatch() || (FrontMatch() && BackMatch()));
        }

        public bool TopMatch() {
            return (top && top.isActive==isActive && top.type == type);
        }

        public bool BottomMatch() {
            return (bottom && bottom.isActive==isActive && bottom.type==type);
        }

        public bool LeftMatch() {
            return (left && left.isActive==isActive &&left.type==type);
        }

        public bool RightMatch() {
            return (right && right.isActive==isActive && right.type == type);
        }

        public bool FrontMatch() {
            return (front && front.isActive==isActive && front.type == type);
        }

        public bool BackMatch() {
            return (back && back.isActive == isActive && back.type == type);
        }
    }
}
