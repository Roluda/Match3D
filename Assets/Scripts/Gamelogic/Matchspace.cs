﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NinjaCactus.Gamelogic {
    public class Matchspace : MonoBehaviour {

        [SerializeField]
        int width;
        [SerializeField]
        int height;
        [SerializeField]
        int depth;
        [SerializeField]
        Matchable prefab;

        public Matchable[,,] grid;

        // Start is called before the first frame update
        void Start() {
            CreateGrid();
            SetHorizontalNeighbors();
            SetVerticalNeighbors();
            SetDepthNeighbors();
        }

        void CreateGrid() {
            grid = new Matchable[width, height, depth];
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    for (int z = 0; z < depth; z++) {
                        grid[x,y,z] = Instantiate(prefab, new Vector3(x-width/2,y-height/2,z-depth/2), Quaternion.identity, transform);
                    }
                }
            }
        }

        void SetHorizontalNeighbors() {
            for (int x = 1; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    for (int z = 0; z < depth; z++) {
                        grid[x,y,z].left = grid[x - 1,y,z];
                        grid[x - 1,y,z].right = grid[x,y,z];
                    }
                }
            }
        }

        void SetVerticalNeighbors() {
            for (int x=0; x<width; x++) {
                for (int y=1; y<height; y++) {
                    for (int z =0; z<depth; z++) {
                        grid[x,y,z].bottom = grid[x,y - 1,z];
                        grid[x,y - 1,z].top = grid[x,y,z];
                    }
                }
            }
        }

        void SetDepthNeighbors() {
            for (int x = 0; x < width; x++) {
                for (int y = 0; y < height; y++) {
                    for (int z = 1; z < depth; z++) {
                        grid[x,y,z].front = grid[x,y,z-1];
                        grid[x,y,z-1].back = grid[x,y,z];
                    }
                }
            }
        }
    }
}