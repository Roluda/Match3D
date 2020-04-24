using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;

namespace NinjaCactus {
    public class GameManager : MonoBehaviour {
        [SerializeField]
        Matchspace spacePrefab;
        Matchspace currentSpace;

        private void Start() {
            currentSpace = Instantiate(spacePrefab);
        }
    }
}
