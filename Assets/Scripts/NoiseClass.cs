//Author: Rok Kos <kosrok97@gmail.com>
//File: NoiseClass.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Scripts\NoiseClass.cs
//Date: 26.07.2016
//Description: Class that will represent perlin noise

using UnityEngine;
using System.Collections;


public class NoiseClass {

    /**
    * @brief Generates 2 dimensional array of floats taht represent noisem map.
    * @param height Integer for height.
    * @param width Integer for width.
    * @param scale Scale of the noise.
    * @returns 2 dimensiona array of floats.
    */
    public static float[,] GenerateNoiseMap (int height, int width, float scale) {
        float[,] noiseMap = new float[height, width];

        // Avoiding division by zero
        if (scale == 0) {
            scale = 0.0001f;
        } 

        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                float sampleX = j / scale;
                float sampleY = i / scale;

                // Generate 2D Perlin noise.
                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);

                noiseMap[i, j] = perlinValue;
            }
        }

        return noiseMap;
    }

}