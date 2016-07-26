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
    public bool autoUpdate;

    public void GenerateMap () {
        float[,] noiseMap = NoiseClass.GenerateNoiseMap(height, width, scale);

        MapDisplay mapDisplay = FindObjectOfType<MapDisplay>();
        mapDisplay.DrawNoiseMap(noiseMap);
    }

}
