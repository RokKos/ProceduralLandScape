//Author: Rok Kos <kosrok97@gmail.com>
//File: MeshGenerator.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Editor\MeshGenerator.cs
//Date: 27.07.2016
//Description: Script that generates our mesh

using UnityEngine;
using System.Collections;

public static class MeshGenerator {

    public static MeshData GenerateTerrainMesh (float[,] noiseMap, float heightMultiplier, AnimationCurve heightCurve, int  levelOfDetail) {
        int height = noiseMap.GetLength(0);
        int width = noiseMap.GetLength(1);
        
        // For centering mesh
        float topLeftX = (width - 1) / -2.0f;
        float topLeftZ = (height - 1) / 2.0f;

        // If levelOfDeteail is 0 then change to 1 else multiplie by 2
        // This is because we dont get stuck in infinity loop
        int detailIncrement = (levelOfDetail == 0) ? 1 : levelOfDetail * 2;

        int verticesPerLine = (width - 1) / detailIncrement + 1;
        MeshData meshData = new MeshData(verticesPerLine, verticesPerLine);
        int vertexIndex = 0;

        for (int i = 0; i < height; i += detailIncrement) {
            for (int j = 0; j < width; j += detailIncrement) {
                // Height Curve gives us coresponding value based on passed value
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + j, noiseMap[i, j] * heightCurve.Evaluate(noiseMap[i, j]) * heightMultiplier, topLeftZ - i);
                meshData.UVMaps[vertexIndex] = new Vector2(j / (float)width, i / (float)height);

                if (j < width - 1 && i < height - 1) {
                    meshData.AddTringle(vertexIndex, vertexIndex + verticesPerLine + 1, vertexIndex + verticesPerLine);
                    meshData.AddTringle(vertexIndex + verticesPerLine + 1, vertexIndex, vertexIndex + 1);
                }

                vertexIndex++;
            }
        }

        return meshData;

    }

}

/**
* @brief Class that will hold mesh data. It contains all positions of vertices and
* all conections of triangles.
**/

public class MeshData {
    public Vector3[] vertices;
    public int[] triangles;
    public Vector2[] UVMaps;
    int triangleIndex;

    // Constructor
    public MeshData (int meshHeight, int meshWidth) {
        vertices = new Vector3[meshHeight * meshWidth];
        triangles = new int[(meshHeight - 1) * (meshWidth - 1) * 6];
        UVMaps = new Vector2[meshHeight * meshWidth];
    }

    public void AddTringle (int pointA, int pointB, int pointC) {
        triangles[triangleIndex] = pointA;
        triangles[triangleIndex + 1] = pointB;
        triangles[triangleIndex + 2] = pointC;

        triangleIndex += 3;

    }

    public Mesh GenerateMesh () {
        // Building mesh should always be in this order:
        // 1. Assign vertices
        // 2. Assign triangles
        // 3. Assign UV map

        Mesh mesh = new Mesh();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = UVMaps;
        mesh.RecalculateNormals();
        return mesh;
    }
}