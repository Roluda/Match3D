using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using NinjaCactus.Level;
using System.IO;
using NinjaCactus.SaveAndLoad;

public class JsonToGeneratorConverter
{
    [MenuItem("Assets/Create/Generator/TypeMapFromJson")]
    public static void ConvertJsonToGenerator() {
        string path = EditorUtility.OpenFilePanel("Select Save File", "", "");
        FromTypeMapGenerator asset = ScriptableObject.CreateInstance<FromTypeMapGenerator>();

        using (StreamReader stream = new StreamReader(path)) {
            string json = stream.ReadToEnd();
            asset.typeMap = JsonUtility.FromJson<TypeMap>(json);
        }

        EditorUtility.FocusProjectWindow();
        AssetDatabase.CreateAsset(asset, "Assets/Resources/Generators/New Generator.asset");
        AssetDatabase.SaveAssets();
        Selection.activeObject = asset;
    }
}
