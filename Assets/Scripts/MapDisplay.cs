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
    [SerializeField]
    MeshFilter meshFilter;
    [SerializeField]
    MeshRenderer meshRenderer;

    /**
    * @brief Draws noise map on 2d plane.
    **/

    public void DrawTexture (Texture2D texture) {
        // Setting texture to renderer
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 0, texture.height);
    }

    public void DrawMesh (MeshData meshData, Texture2D texture) {
        // Mesh filter is used to proceduraly change mesh
        // It allow us to get to the mesh components
        meshFilter.sharedMesh = meshData.GenerateMesh();
        meshRenderer.sharedMaterial.mainTexture = texture;
    }
}
