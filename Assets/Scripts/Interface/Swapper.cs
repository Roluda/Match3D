using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;
using NinjaCactus.AI;

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
        Stack<SwapPair> undoStack = new Stack<SwapPair>();


        // Update is called once per frame
        void Update() {
            if (Input.GetKeyDown(KeyCode.Mouse0)) {
                if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out RaycastHit hit, Mathf.Infinity, blockLayer)){
                    if(hit.collider.TryGetComponent(out Matchable match)) {
                        TryMatchSwap(match);
                    }
                }
            }

            if (Input.GetKeyDown(KeyCode.Z)) {
                Undo();
            }
        }

        void TryMatchSwap(Matchable match) {
            if (buffer && buffer.isActive) {
                SwapPair swap = new SwapPair(buffer, match);
                if (swap.MatchingSwap()) {
                    swap.Swap();
                    undoStack.Push(swap);
                    buffer = null;
                } else {
                    swap.Swap();
                    StartCoroutine(SwapBack(swap));
                    buffer = null;
                }
            } else {
                buffer = match;
            }
        }

        public void Reset() {
            undoStack.Clear();
            buffer = null;
        }

        public void Undo() {
            if (undoStack.Count > 0) {
                SwapPair nextUndo = undoStack.Pop();
                nextUndo.Swap();
            }
        }

        IEnumerator SwapBack(SwapPair swap) {
            yield return new WaitForFixedUpdate();
            swap.Swap();
        }
    }
}
