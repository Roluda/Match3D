using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Gamelogic{
    [CreateAssetMenu(menuName = "ColorMap")]
    public class ColorMap : ScriptableObject {
        [SerializeField]
        Color defaultColor = Color.white;
        [SerializeField]
        Color[] colors;

        public Color GetColor(int type) {
            if (type < colors.Length) {
                return colors[type];
            } else {
                return defaultColor;
            }
        }

        public Color RandomColor() {
            return colors[Random.Range(0, colors.Length)];
        }
    }
}
