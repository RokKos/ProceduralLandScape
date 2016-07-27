//Author: Rok Kos <kosrok97@gmail.com>
//File: TextureGenerator.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Editor\TextureGenerator.cs
//Date: 27.07.2016
//Description: Script that creates colored textures

using UnityEngine;
using System.Collections;

public static class TextureGenerator {
    public static Texture2D TextureToColorMap (Color[] colorMap, int width, int height) {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point;  // Fixed blurines at the end of the texture
        texture.wrapMode = TextureWrapMode.Clamp;
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromNoisetMap (float[,] noiseMap) {
        int height = noiseMap.GetLength(0);
        int width = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(height, width);

        Color[] colorMap = new Color[width * height];
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                // colourMap is single dimentional array so by multipling i with height and adding j we
                // get the right cordinate
                // Color.Lerp interpolates between two colors
                colorMap[i * width + j] = Color.Lerp(Color.black, Color.white, noiseMap[i, j]);
            }
        }

        texture.SetPixels(colorMap);
        texture.Apply();

        return TextureToColorMap(colorMap, width, height);
    }

}
