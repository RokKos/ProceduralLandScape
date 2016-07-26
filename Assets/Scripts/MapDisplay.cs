//Author: Rok Kos <kosrok97@gmail.com>
//File: NoiseClass.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Scripts\MapDisplay.cs
//Date: 26.07.2016
//Description:Script that displays map

using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour {

    [SerializeField]
    Renderer textureRenderer;

    public void DrawNoiseMap (float[,] noiseMap) {
        int height = noiseMap.GetLength(0);
        int width = noiseMap.GetLength(1);

        Texture2D texture = new Texture2D(height, width);

        Color[] colourMap = new Color[width * height];
        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                // colourMap is single dimentional array so by multipling i with height and adding j we
                // get the right cordinate
                // Color.Lerp interpolates between two colors
                colourMap[i * width + j] = Color.Lerp(Color.black, Color.white, noiseMap[i,j]);
            }
        }

        texture.SetPixels(colourMap);
        texture.Apply();

        // Setting texture to renderer
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(height, 0, width);
    }
}
