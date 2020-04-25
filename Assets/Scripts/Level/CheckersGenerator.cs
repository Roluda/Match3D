using NinjaCactus.Gamelogic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Level {
    [CreateAssetMenu(menuName="Generator/Checkers")]
    public class CheckersGenerator : Generator {
        [SerializeField]
        protected Vector3Int spaceSize;
        [SerializeField]
        int types = 2;
        public override Matchspace Generate() {
            Matchspace space = Matchspace.Create(spaceSize);
            for(int x=0; x<space.width; x++) {
                for(int y=0; y < space.height; y++) {
                    for(int z=0; z<space.depth; z++) {
                        space.grid[x, y, z].type = (x + y + z) % types;
                    }
                }
            }
            return space;
        }
    }
}
