using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

namespace NinjaCactus {
    public class DelayedEvent : MonoBehaviour {
        [Serializable]
        public class Event : UnityEvent { }
        [SerializeField]
        Event onDelayFinish;


        public void Invoke(float delay) {
            StartCoroutine(Delay(delay));
        }

        IEnumerator Delay(float delay) {
            yield return new WaitForSeconds(delay);
            onDelayFinish.Invoke();
        }
    }
}
