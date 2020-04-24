using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;

namespace NinjaCactus.Interface {
    public class Swapper : MonoBehaviour {

        [SerializeField]
        LayerMask blockLayer;

        Matchable bufferData;
        Matchable buffer {
            get => bufferData;
            set {
                bufferData?.StopHighlight();
                bufferData = value;
                bufferData?.Highlight();
            }
        }


        struct SwapPair {
            public SwapPair(Matchable first, Matchable second) {
                this.first = first;
                this.second = second;
            }
            public Matchable first;
            public Matchable second;
        }
        Stack<SwapPair> undoStack = new Stack<SwapPair>();

        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, blockLayer)){
                    if(hit.collider.TryGetComponent(out Matchable match)) {
                        HandleMatch(match);
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.Z)) {
                Undo();
            }
        }

        void HandleMatch(Matchable match) {
            if (buffer) {
                Swap(buffer, match);
                if (match.AnyMatch()||buffer.AnyMatch()) {
                    undoStack.Push(new SwapPair(buffer, match));
                } else {
                    Swap(buffer, match);      
                }
                buffer = null;
            } else {
                buffer = match;
            }
        }

        void Swap(Matchable first, Matchable second) {
            int firstType = first.type;
            first.type = second.type;
            second.type = firstType;
        }

        public void Reset() {
            undoStack.Clear();
            buffer = null;
        }

        public void Undo() {
            if (undoStack.Count > 0) {
                SwapPair nextUndo = undoStack.Pop();
                Swap(nextUndo.first, nextUndo.second);
            }
        }
    }
}
