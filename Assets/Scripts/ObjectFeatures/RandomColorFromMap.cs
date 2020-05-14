using NinjaCactus.Gamelogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCatus.ObjectFeatures {
    public class RandomColorFromMap : MonoBehaviour {
        [SerializeField]
        Renderer render;
        [SerializeField]
        ColorMap colorMap;

        public void SetMainColorRadom() {
            render.material.SetColor("_MainColor", colorMap.RandomColor());
        }
    }
}
