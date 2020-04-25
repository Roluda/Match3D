using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Gamelogic {
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Renderer))]
    public class Matchable : MonoBehaviour {
        int type_ = 0;
        public int type {
            get => type_;
            set {
                type_ = value;
                ApplyColor(colorMap.GetColor(value));
            }
        }

        bool isActive_ = true;
        public bool isActive {
            get => isActive_;
            set {
                isActive_ = value;
                col.enabled = value;
            }
        }

        public bool isMatch { get; set; }
        public Matchable left { get; set; }
        public Matchable right { get; set; }
        public Matchable back { get; set; }
        public Matchable front { get; set; }
        public Matchable top { get; set; }
        public Matchable bottom { get; set; }


        [SerializeField]
        ColorMap colorMap = default;
        [SerializeField]
        Renderer render = default;
        [SerializeField]
        Collider col = default;

        private void Update() {
            isMatch = AnyMatch();
            if (isActive) {
                render.material.SetFloat("_LastActive", Time.time);
            }
        }

        private void LateUpdate() {
            if (isActive && isMatch) {
                isActive = false;
            } else if(!isActive && !isMatch) {
                isActive = true;
            }
        }

        public void Highlight() {
            render.material.SetFloat("_Highlighted", 1);
        }

        public void StopHighlight() {
            render.material.SetFloat("_Highlighted", 0);
        }

        public bool IsNeighbor(Matchable candidate) {
            return (candidate == top 
                || candidate == bottom 
                || candidate == left 
                || candidate == right 
                || candidate == front 
                || candidate == back);
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

        void ApplyColor(Color color) {
            render.material.SetColor("_OldColor", render.material.GetColor("_BaseColor"));
            render.material.SetColor("_BaseColor", color);
            render.material.SetFloat("_ColorChanged", Time.time);
        }
    }
}
