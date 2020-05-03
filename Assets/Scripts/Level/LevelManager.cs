using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;
using NinjaCactus.SaveAndLoad;
using NinjaCactus.AI;

namespace NinjaCactus.Level {
    public class LevelManager : MonoBehaviour {

        [Header("Generators")]
        [SerializeField]
        Generator[] levels = default;

        [Header("Events")]
        [SerializeField]
        public MatchspaceEvent onLevelWin;
        [SerializeField]
        public MatchspaceEvent onLevelStart;

        Matchspace currentLevel;

        int index_ = 0;
        public int index {
            get {
                return index_;
            }
            set {
                index_ = value % levels.Length;
            }
        }

        bool levelActive;

        private void Update() {
            if(levelActive && currentLevel.InEquilibrium()) {
                levelActive = false;
                onLevelWin?.Invoke(currentLevel);
            }
        }

        public void StartLevel(int index) {
            this.index = index;
            if (currentLevel) {
                Destroy(currentLevel.gameObject);
            }
            currentLevel = levels[index].Generate();
            onLevelStart?.Invoke(currentLevel);
            levelActive = true;
        }

        public void SaveLevel() {
            SaveManager.SaveToTypeMap(currentLevel);
        }

        public void Restart() {
            StartLevel(index);
        }

        public void NextLevel() {
            index++;
            StartLevel(index);
        }

        public void PreviousLevel() {
            index--;
            StartLevel(index);
        }
    }
}
