using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.SaveAndLoad;
using NinjaCactus.Gamelogic;
using System.IO;

namespace NinjaCactus.Level {
    [CreateAssetMenu(menuName="Generator/TypeMapGenerator")]
    public class FromTypeMapGenerator : Generator {
        public TypeMap typeMap;

        public override Matchspace Generate() {
            Vector3Int size = new Vector3Int(typeMap.width, typeMap.height, typeMap.depth);
            Matchspace space = Matchspace.Create(size);
            int[,,] types = typeMap.GetTypes();
            for (int x = 0; x < space.width; x++) {
                for (int y = 0; y < space.height; y++) {
                    for (int z = 0; z < space.depth; z++) {
                        space.grid[x, y, z].type = types[x, y, z];
                    }
                }
            }
            space.Step();
            return space;
        }
    }
}
