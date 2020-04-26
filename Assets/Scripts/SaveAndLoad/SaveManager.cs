using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;
using System.IO;

namespace NinjaCactus.SaveAndLoad {
    public static class SaveManager{
        private static readonly string SAVE_FOLDER = Application.dataPath + "/Saves/";

        public static string fileName = "typeMap";

        public static void SaveToTypeMap(Matchspace space) {
            if (!Directory.Exists(SAVE_FOLDER)) {
                Directory.CreateDirectory(SAVE_FOLDER);
            }
            int count = 0;
            while(File.Exists(SAVE_FOLDER + fileName + count + ".json")) {
                count++;
            }
            TypeMap saveMap = new TypeMap(space);
            string json = JsonUtility.ToJson(saveMap);
            Debug.Log("Saving to: " + SAVE_FOLDER);
            File.WriteAllText(SAVE_FOLDER + fileName + count +".json", json);
        }
    }
}
