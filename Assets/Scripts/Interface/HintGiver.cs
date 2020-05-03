using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.AI;
using NinjaCactus.Gamelogic;

namespace NinjaCactus.Interface {
    public class HintGiver : MonoBehaviour {

        [SerializeField]
        Matchspace currentSpace;
        [SerializeField]
        float highlightDiration;

        public void Setup(Matchspace space) {
            currentSpace = space;
        }

        public void Hint() {
            if (EquilibriumFinder.Search(currentSpace, out List<SwapPair> steps)) {
                if (steps.Count > 0) {
                    StartCoroutine(Highlight(steps[0].first));
                    StartCoroutine(Highlight(steps[0].second));
                }
            }
        }

        IEnumerator Highlight(Matchable match) {
            match.Select();
            yield return new WaitForSeconds(highlightDiration);
            match.StopSelect();
        }
    }
}
