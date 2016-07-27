//Author: Rok Kos <kosrok97@gmail.com>
//File: NoiseClass.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Scripts\NoiseClass.cs
//Date: 26.07.2016
//Description: Class that will represent perlin noise

using UnityEngine;
using System.Collections;


public class NoiseClass {

    private const int offsetNum = 100000;

    /**
    * @brief Generates 2 dimensional array of floats taht represent noisem map.
    * @param height Integer for height.
    * @param width Integer for width.
    * @param scale Scale of the noise.
    * @param octaves 
    * @param persistance 
    * @param lacunarity 
    * @returns 2 dimensiona array of floats.
    */

    public static float[,] GenerateNoiseMap (int height, int width, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset) {
        float[,] noiseMap = new float[height, width];

        System.Random randomGenerator = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[octaves];

        for (int i = 0; i < octaves; ++i) {
            float offsetX = randomGenerator.Next(-offsetNum, offsetNum) + offset.x;
            float offsetY = randomGenerator.Next(-offsetNum, offsetNum) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        // Avoiding division by zero
        if (scale == 0) {
            scale = 0.0001f;
        }

        float halfWidth = width / 2.0f;
        float halfHeight = height / 2.0f;

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {

                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for (int k = 0; k < octaves; ++k) {
                    float sampleX = (j - halfHeight) / scale * frequency + octaveOffsets[k].x;
                    float sampleY = (i - halfWidth) / scale * frequency + octaveOffsets[k].y;

                    // Generate 2D Perlin noise.
                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1;

                    noiseHeight += perlinValue * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                // Just changing max and min height so that we can
                // later normalize values between 0 and 1
                if (noiseHeight > maxNoiseHeight) {
                    maxNoiseHeight = noiseHeight;
                } else if (noiseHeight < minNoiseHeight) {
                    minNoiseHeight = noiseHeight;
                }

                noiseMap[i, j] = noiseHeight;
            }
        }

        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                // Normalize values of noise map between 0 and 1
                // if value is equal to minimum then return 0
                // if value is equal to maximum then return 1
                noiseMap[i, j] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[i, j]);
                //Debug.Log(noiseMap[i, j]);
            }
        }

        return noiseMap;
    }

}