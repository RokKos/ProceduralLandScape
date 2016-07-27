using UnityEngine;
using System.Collections;

public static class MeshGenerator {

    public static MeshData GenerateTerrainMesh (float[,] noiseMap) {
        int height = noiseMap.GetLength(0);
        int width = noiseMap.GetLength(1);
        
        // For centering mesh
        float topLeftX = (width - 1) / -2.0f;
        float topLeftZ = (height - 1) / 2.0f;

        MeshData meshData = new MeshData(height, width);
        int vertexIndex = 0;

        for (int i = 0; i < height; ++i) {
            for (int j = 0; j < width; ++j) {
                meshData.vertices[vertexIndex] = new Vector3(topLeftX + j, noiseMap[i,j], topLeftZ - i);
                meshData.UVMaps[vertexIndex] = new Vector2(j / (float)width, i / (float)height);

                if (j < width - 1 && i < height - 1) {
                    meshData.AddTringle(vertexIndex, vertexIndex + width + 1, vertexIndex + width);
                    meshData.AddTringle(vertexIndex + width + 1, vertexIndex, vertexIndex + 1);
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