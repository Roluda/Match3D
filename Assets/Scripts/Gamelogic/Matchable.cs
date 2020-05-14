using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Gamelogic {
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Renderer))]
    public class Matchable : MonoBehaviour {
        public int type;
        public bool isActive;

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

        bool isMatch;
        int colorType = -1;

        private void Update() {
            if (isActive) {
                render.material.SetFloat("_LastActive", Time.time);
            }
            if(colorType != type) {
                colorType = type;
                ApplyColor(colorMap.GetColor(colorType));
                StopHighlight();
                StopSelect();
            }
            col.enabled = isActive;
        }

        private void Awake() {
            col.enabled = false;
        }

        private void Start() {
            render.material.SetColor("_OldColor", colorMap.GetColor(type));
            render.material.SetColor("_BaseColor", colorMap.GetColor(type));
            render.material.SetFloat("_LastActive", -2);
            col.enabled = isActive;
        }

        void ApplyColor(Color color) {
            render.material.SetColor("_OldColor", render.material.GetColor("_BaseColor"));
            render.material.SetColor("_BaseColor", color);
            render.material.SetFloat("_ColorChanged", Time.time);
        }

        public void Highlight() {
            render.material.SetFloat("_Highlighted", 1);
        }

        public void StopHighlight() {
            render.material.SetFloat("_Highlighted", 0);
        }

        public void Select() {
            render.material.SetFloat("_Selected", 1);
        }

        public void StopSelect() {
            render.material.SetFloat("_Selected", 0);
        }

        public void Flag() {
            isMatch = AnyMatch();
        }

        public void UpdateState() {
            if (isActive && isMatch) {
                isActive = false;
            }else if(!isActive && !isMatch) {
                isActive = true;
            }
        }

        public bool IsNeighbor(Matchable candidate) {
            return (candidate == top 
                || candidate == bottom 
                || candidate == left 
                || candidate == right 
                || candidate == front 
                || candidate == back);
        }

        public List<Matchable> GetNeighbors() {
            List<Matchable> neighbors = new List<Matchable>();
            if (top) {
                neighbors.Add(top);
            }
            if (bottom) {
                neighbors.Add(bottom);
            }
            if (left) {
                neighbors.Add(left);
            }
            if (right) {
                neighbors.Add(right);
            }
            if (front) {
                neighbors.Add(front);
            }
            if (back) {
                neighbors.Add(back);
            }
            return neighbors;
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

        public bool AnyAlmost() {
            return BottomAlmost()
                || TopAlmost()
                || LeftAlmost()
                || RightAlmost()
                || FrontAlmost()
                || BackAlmost()
                || HorizontalAlmost()
                || VerticalAlmost()
                || DepthAlmost();
        }

        public bool BottomAlmost() {
            return bottom && !bottom.AnyMatch() && bottom.BottomMatch();
        }

        public bool TopAlmost() {
            return top && !top.AnyMatch() && top.TopMatch();
        }

        public bool LeftAlmost() {
            return left && !left.AnyMatch() && left.LeftMatch();
        }

        public bool RightAlmost() {
            return right && !right.AnyMatch() && right.RightMatch();
        }

        public bool FrontAlmost() {
            return front && !front.AnyMatch() && front.FrontMatch();
        }

        public bool BackAlmost() {
            return back && !back.AnyMatch() &&back.BackMatch();
        }

        public bool VerticalAlmost() {
            return top && bottom && top.type == bottom.type && !top.AnyMatch() && !bottom.AnyMatch();
        }

        public bool HorizontalAlmost() {
            return left && right && left.type == right.type && !left.AnyMatch() && !right.AnyMatch();
        }

        public bool DepthAlmost() {
            return front && back && front.type == back.type && !front.AnyMatch() && !back.AnyMatch();            ;
        }

        public bool Solvable() {
            return !AnyMatch() && (TopSolvable()
                || BottomSolvable()
                || LeftSolvable()
                || RightSolvable()
                || FrontSolvable()
                || BackSolvable()
                || HorizontalSolvable()
                || VerticalSolvable()
                || DepthSolvable());
        }

        public bool TopSolvable() {
            return top && top.top && !top.AnyMatch() && !top.top.AnyMatch();
        }

        public bool BottomSolvable() {
            return bottom && bottom.bottom && !bottom.AnyMatch() && !bottom.bottom.AnyMatch();
        }

        public bool LeftSolvable() {
            return left && left.left && !left.AnyMatch() && !left.left.AnyMatch();
        }

        public bool RightSolvable() {
            return right && right.right && !right.AnyMatch() && !right.right.AnyMatch();
        }

        public bool FrontSolvable() {
            return front && front.front && !front.AnyMatch() && !front.front.AnyMatch();
        }

        public bool BackSolvable() {
            return back && back.back && !back.AnyMatch() && !back.back.AnyMatch();
        }

        public bool HorizontalSolvable() {
            return left && right && !left.AnyMatch() && !right.AnyMatch();
        }

        public bool VerticalSolvable() {
            return top && bottom && !bottom.AnyMatch() && !top.AnyMatch();
        }

        public bool DepthSolvable() {
            return front && back && !front.AnyMatch() && !back.AnyMatch();
        }

        public bool Surrounded() {
            return top && bottom && left && right && front && back;
        }

        public bool Hidden() {
            return Surrounded()
                && left.isActive
                && right.isActive
                && top.isActive
                && bottom.isActive
                && front.isActive
                && back.isActive;
        }
    }
}
