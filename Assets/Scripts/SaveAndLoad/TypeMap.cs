using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NinjaCactus.Gamelogic;

namespace NinjaCactus.SaveAndLoad {
    [Serializable]
    public class TypeMap{
        public TypeMap(Matchspace space) {
            width = space.width;
            height = space.height;
            depth = space.height;
            List<int> typelist = new List<int>();
            for (int x = 0; x < space.width; x++) {
                for (int y = 0; y < space.height; y++) {
                    for (int z = 0; z < space.depth; z++) {
                        typelist.Add(space.grid[x, y, z].type);
                    }
                }
            }
            types = typelist.ToArray();
        }
        public int width;
        public int height;
        public int depth;
        public int[] types;

        public int[,,] GetTypes() {
            int[,,] typeArray = new int[width, height, depth];
            int count = 0;
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    for (int z = 0; z < depth; z++) {
                        typeArray[x, y, z] = types[count];
                        count++;
                    }
                }
            }
            return typeArray;
        }
    }
}
