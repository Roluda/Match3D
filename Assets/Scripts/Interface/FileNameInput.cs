using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.SaveAndLoad;

namespace NinjaCactus.Interface {
    public class FileNameInput : MonoBehaviour {
        public void SetName(string name) {
            SaveManager.fileName = name;
        }
        // Start is called before the first frame update
        void Start() {

        }

        // Update is called once per frame
        void Update() {

        }
    }
}
