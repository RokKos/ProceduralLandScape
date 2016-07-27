//Author: Rok Kos <kosrok97@gmail.com>
//File: NoiseClass.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Scripts\MapGenerator.cs
//Date: 26.07.2016
//Description:Script that generates map

using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {

    [SerializeField]
    int height;
    [SerializeField]
    int width;
    [SerializeField]
    float scale;
    [SerializeField]
    int octaves;
    [Range(0,1)]
    [SerializeField]
    float persistance;
    [SerializeField]
    float lacunarity;
    [SerializeField]
    int seed;
    [SerializeField]
    Vector2 offset;

    public bool autoUpdate;

    [System.Serializable] // Showing in the inspector
    public struct TerrainType {
        public string nameOfTerrain;
        public float height;
        public Color color;
    }

    public enum DrawMode { NoiseMap, ColorMap, DrawMesh};
    public DrawMode drawMode;

    public TerrainType[] terrainRegions; 

    public void GenerateMap () {
        float[,] noiseMap = NoiseClass.GenerateNoiseMap(height, width, seed, scale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[height * width];

        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                float currHeight = noiseMap[i, j];
                for (int k = 0; k < terrainRegions.Length; ++k) {
                    if (currHeight <= terrainRegions[k].height) {
                        colorMap[i * width + j] = terrainRegions[k].color;
                        break;
                    }
                }
            }
       }

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();

        switch (drawMode) {
            case DrawMode.NoiseMap:
                mapDisplay.DrawTexture(TextureGenerator.TextureFromNoisetMap(noiseMap));
                break;
            case DrawMode.ColorMap:
                mapDisplay.DrawTexture(TextureGenerator.TextureToColorMap(colorMap, width, height));
                break;

            case DrawMode.DrawMesh:
                mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap), TextureGenerator.TextureToColorMap(colorMap, width, height));
                break;
            default:
                mapDisplay.DrawTexture(TextureGenerator.TextureFromNoisetMap(noiseMap));
                break;
        }
        
    }


    // Funtion that is called every time when value in inspector is changed
    // and it basicly doesnt let value to drob belo1
    void OnValidate () {
        if (height < 1) {
            height = 1;
        }
        if (width < 1) {
            width = 1;
        }
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 1) {
            octaves = 1;
        }
    }
}
