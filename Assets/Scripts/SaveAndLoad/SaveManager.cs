using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;
using System.IO;

namespace NinjaCactus.SaveAndLoad {
    public static class SaveManager{
        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

        public static void SaveToTypeMap(Matchspace space) {
            if (!Directory.Exists(SAVE_FOLDER)) {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
            TypeMap saveMap = new TypeMap(space);
            string json = JsonUtility.ToJson(saveMap);
            Debug.Log("Saving to: " + SAVE_FOLDER);
            File.WriteAllText(SAVE_FOLDER + "typeMap", json);
        }
    }
}
