using NinjaCactus.Gamelogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Interface {
    public class MatchspacePresenter : MonoBehaviour {
        [SerializeField]
        AnimationCurve size;
        [SerializeField]
        float duration;
        [SerializeField]

        Matchspace currentLevel;

        public void ShowSpace(Matchspace space) {
            currentLevel = space;
            StartCoroutine(PresentSpace());
        }

        IEnumerator PresentSpace() {
            currentLevel.transform.localScale = Vector3.zero;
            currentLevel.transform.Rotate(Random.insideUnitSphere * 90);
            float timer = 0;
            while (timer < duration) {
                timer += Time.deltaTime;
                currentLevel.transform.localScale = Vector3.one * size.Evaluate(timer / duration);
                yield return null;
            }
            currentLevel.transform.localScale = Vector3.one;
        }


    }
}
