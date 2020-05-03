using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;
using System.Linq;

namespace NinjaCactus.AI {
    public static class EquilibriumFinder{

        public static bool Search(Matchspace space) {
            return Search(space, out List<SwapPair> list);
        }

        public static bool Search(Matchspace space, out List<SwapPair> steps) {
            Stack<SwapPair> path = new Stack<SwapPair>();
            if(Depthsearch(space, path)) {
                steps = path.ToList();
                steps.Reverse();
                while (path.Count > 0) {
                    path.Pop().Swap();
                    space.Step();
                }
                return true;
            } else {
                steps = null;
                return false;
            }
        }

        static bool Depthsearch(Matchspace space, Stack<SwapPair> path) {
            foreach(SwapPair step in PossibleSwaps(space)) {
                step.Swap();
                space.Step();
                path.Push(step);
                if (space.IsSolvable() && Depthsearch(space, path)) {
                    break;
                } else {
                    path.Pop().Swap();
                    space.Step();
                }
            }
            return space.InEquilibrium();
        }

        static IEnumerable<SwapPair> PossibleSwaps(Matchspace space) {
            var surfaceNoMatch = from Matchable match in space.grid
                      where !match.Hidden() && !match.AnyMatch()
                      select match;
            foreach(Matchable first in surfaceNoMatch.Where(first => first.AnyAlmost())) {
                foreach(Matchable second in surfaceNoMatch) {
                    if (SwapPair.MatchingSwap(first, second)) {
                        yield return new SwapPair(first, second);
                    }
                }
            }
        }
    }
}
