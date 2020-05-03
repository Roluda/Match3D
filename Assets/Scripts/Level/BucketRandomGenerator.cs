using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;
using NinjaCactus.AI;

namespace NinjaCactus.Level {
    [CreateAssetMenu(menuName="Generator/RandomBucket")]
    public class BucketRandomGenerator : Generator {
        [SerializeField]
        protected Vector3Int spaceSize;
        [SerializeField]
        int[] buckets;

        public override Matchspace Generate() {
            int count = 0;
            Matchspace space = Matchspace.Create(spaceSize);
            do {
                count++;
                Randomize(space);
                space.Step();
            } while (!EquilibriumFinder.Search(space));
            Debug.Log("Iterations until solution found: " + count);
            return space;
        }

        void Randomize(Matchspace space) {
            List<int> types = new List<int>();
            for (int b = 0; b < buckets.Length; b++) {
                for (int i = 0; i < buckets[b]; i++) {
                    types.Add(b);
                }
            }
            for (int i = types.Count - 1; i > 0; i--) {
                int buffer = types[i];
                int random = types[Random.Range(0, i)];
                types[i] = types[random];
                types[random] = buffer;
            }
            int count = 0;
            foreach (Matchable match in space.grid) {
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
