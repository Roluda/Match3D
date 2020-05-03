using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Gamelogic {
    public class SwapPair {
        public SwapPair(Matchable first, Matchable second) {
            this.first = first;
            this.second = second;
        }
        public Matchable first;
        public Matchable second;
        public void Swap() {
            int firstType = first.type;
            first.type = second.type;
            second.type = firstType;
        }

        public bool MatchingSwap() {
            Swap();
            if (first.AnyMatch() || second.AnyMatch()) {
                Swap();
                return true;
            } else {
                Swap();
                return false;
            }
        }

        public static bool MatchingSwap(Matchable first, Matchable second) {
            Swap(first, second);
            if (first.AnyMatch() || second.AnyMatch()) {
                Swap(first, second);
                return true;
            } else {
                Swap(first, second);
                return false;
            }
        }

        public static void Swap(Matchable first, Matchable second) {
            int firstType = first.type;
            first.type = second.type;
            second.type = firstType;
        }
    }
}
