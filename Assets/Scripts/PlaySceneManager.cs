using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Level;
using NinjaCactus.Interface;
using NinjaCactus.Gamelogic;
using NinjaCactus.SaveAndLoad;

namespace NinjaCactus {
    public class PlaySceneManager : MonoBehaviour {
        [SerializeField]
        Generator[] levels = default;

        [SerializeField]
        Rotator rotator = default;
        [SerializeField]
        Swapper swapper = default;

        Matchspace currentLevel;


        public void StartLevel(int index) {
            if (currentLevel) {
                Destroy(currentLevel.gameObject);
            }
            currentLevel = levels[index].Generate();
            rotator.targetObject = currentLevel.gameObject;
            swapper.Reset();
        }

        public void SaveLevel() {
            SaveManager.SaveToTypeMap(currentLevel);
        }

        public void NextLevel() {

        }
    }
}
