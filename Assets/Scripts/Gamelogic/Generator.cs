using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using NinjaCactus.Gamelogic;

namespace NinjaCactus.Level {
    public class Generator : MonoBehaviour {
        [SerializeField]
        Matchspace space;
        [SerializeField]
        int[] buckets;

        public void Generate() {
            List<int> types = new List<int>();
            for(int b=0; b < buckets.Length; b++) { 
                for(int i=0; i< buckets[b]; i++) {
                    types.Add(b);
                }
            }
            for (int i = types.Count-1; i>0; i--) {
                int buffer = types[i];
                int random = types[Random.Range(0, i)];
                types[i] = types[random];
                types[random] = buffer;
            }
            int count = 0;
            foreach(Matchable match in space.grid) {
                if (count < types.Count) {
                    match.type = types[count];
                    count++;
                } else {
                    match.type = -1;
                }
            }
        }
    }
}
