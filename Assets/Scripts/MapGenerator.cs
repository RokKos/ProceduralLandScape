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

    public void GenerateMap () {
        float[,] noiseMap = NoiseClass.GenerateNoiseMap(height, width, seed, scale, octaves, persistance, lacunarity, offset);

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.DrawNoiseMap(noiseMap);
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
