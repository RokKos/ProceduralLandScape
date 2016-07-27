//Author: Rok Kos <kosrok97@gmail.com>
//File: NoiseClass.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Scripts\MapGenerator.cs
//Date: 26.07.2016
//Description:Script that generates map

using UnityEngine;
using System.Collections;

public class MapGenerator : MonoBehaviour {
    [Range(0,6)]
    [SerializeField]
    int leveleOfDetail;
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
    [SerializeField]
    float heightMultiplier;
    [SerializeField]
    AnimationCurve meshHeigthCurve;

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

    public const int mapChunkSize = 241;

    public void GenerateMap () {
        float[,] noiseMap = NoiseClass.GenerateNoiseMap(mapChunkSize, mapChunkSize, seed, scale, octaves, persistance, lacunarity, offset);

        Color[] colorMap = new Color[mapChunkSize * mapChunkSize];

        for (int i = 0; i < mapChunkSize; ++i) {
            for (int j = 0; j < mapChunkSize; ++j) {
                float currHeight = noiseMap[i, j];
                for (int k = 0; k < terrainRegions.Length; ++k) {
                    if (currHeight <= terrainRegions[k].height) {
                        colorMap[i * mapChunkSize + j] = terrainRegions[k].color;
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
                mapDisplay.DrawTexture(TextureGenerator.TextureToColorMap(colorMap, mapChunkSize, mapChunkSize));
                break;

            case DrawMode.DrawMesh:
                mapDisplay.DrawMesh(MeshGenerator.GenerateTerrainMesh(noiseMap, heightMultiplier, meshHeigthCurve, leveleOfDetail), TextureGenerator.TextureToColorMap(colorMap, mapChunkSize, mapChunkSize));
                break;
            default:
                mapDisplay.DrawTexture(TextureGenerator.TextureFromNoisetMap(noiseMap));
                break;
        }
        
    }


    // Funtion that is called every time when value in inspector is changed
    // and it basicly doesnt let value to drob belo1
    void OnValidate () {
        if (lacunarity < 1) {
            lacunarity = 1;
        }
        if (octaves < 1) {
            octaves = 1;
        }
    }
}
