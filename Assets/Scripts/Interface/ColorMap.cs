using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Visuals{
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
    }
}
