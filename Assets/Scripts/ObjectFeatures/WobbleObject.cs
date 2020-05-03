using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.ObjectFeatures {
    public class WobbleObject : MonoBehaviour {
        [SerializeField]
        GameObject target;
        [SerializeField]
        AnimationCurve wobbleCurve;
        [SerializeField]
        float wobbleDuration;
        // Start is called before the first frame update
        public void DoWobble() {
            StartCoroutine(Wobble());
        }

        IEnumerator Wobble() {
            float timer = 0;
            Vector3 initialScale = target.transform.localScale;
            while (timer < wobbleDuration) {
                timer += Time.deltaTime;
                target.transform.localScale = initialScale * wobbleCurve.Evaluate(timer/wobbleDuration);
                yield return null;
            }
            target.transform.localScale = initialScale;
        }
    }
}
