using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;

namespace NinjaCactus.Interface {
    public class SwapHistory : MonoBehaviour {
        Stack<SwapPair> history;

        public void Push(SwapPair swap) {
            history.Push(swap);
        }

        public void Undo() {
            history.Pop().Swap();
        }

        public void Clear() {
            history.Clear();
        }
    }
}
