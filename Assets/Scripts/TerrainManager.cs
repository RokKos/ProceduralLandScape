//Author: Rok Kos <kosrok97@gmail.com>
//File: MeshGenerator.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Editor\MeshGenerator.cs
//Date: 27.07.2016
//Description: Script that controls spawning and deleting terrain

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TerrainManager : MonoBehaviour {

    public const float viewDistance = 450;
    [SerializeField]
    Transform player;
    [SerializeField]
    Transform parent;
    public static Vector2 playerPosition;
    private int chunkSize;
    private int chunkVisible;

    private Dictionary<Vector2, TerrainChunk> terrainChunkDict = new Dictionary<Vector2, TerrainChunk>();
    private List<TerrainChunk> terrainChunkLastUpdate = new List<TerrainChunk>();

    void Start () {
        chunkSize = MapGenerator.mapChunkSize - 1;
        chunkVisible = Mathf.RoundToInt(viewDistance / chunkSize);
    }

    void Update () {
        playerPosition = new Vector2(player.position.x, player.position.z);
        UpdateVisibleChunk();
    }

    void UpdateVisibleChunk () {

        // Disable all chunks from previus frame
        for (int i = 0; i < terrainChunkLastUpdate.Count; ++i) {
            terrainChunkLastUpdate[i].SetVisible(false);
        }
        terrainChunkLastUpdate.Clear();

        int currChunkX = Mathf.RoundToInt(playerPosition.x / chunkSize);
        int currChunkY = Mathf.RoundToInt(playerPosition.y / chunkSize);

        // Goes thru all Chunks in this frame
        for (int x = -chunkVisible; x <= chunkVisible; ++x) {
            for (int y = -chunkVisible; y <= chunkVisible; ++y) {
                Vector2 viewChunkCord = new Vector2(currChunkX + x, currChunkY + y);

                if (terrainChunkDict.ContainsKey(viewChunkCord)) {
                    TerrainChunk tempChunk = terrainChunkDict[viewChunkCord];
                    // Update chunk
                    tempChunk.UpdateTerrainChunk();
                    // And then see if it is visible
                    if (tempChunk.IsVisible()) {
                        terrainChunkLastUpdate.Add(tempChunk);
                    }
                } else {
                    terrainChunkDict.Add(viewChunkCord, new TerrainChunk(viewChunkCord, chunkSize, parent));
                }
            }

        }
    }

    public class TerrainChunk {

        private Vector2 position;
        private GameObject meshObject;
        private Bounds bounds;  // Represents an axis aligned bounding box.

        // Constructor
        public TerrainChunk (Vector2 cordinate, int size, Transform parent) {
            position = cordinate * size;
            bounds = new Bounds(position, Vector2.one * size);
            Vector3 worldPosition = new Vector3(position.x, 0, position.y);

            meshObject = GameObject.CreatePrimitive(PrimitiveType.Plane);
            meshObject.transform.position = worldPosition;
            meshObject.transform.localScale = Vector3.one * size / 10.0f;
            meshObject.transform.parent = parent;  // Just to have all planes in hiearhy under map generator

            SetVisible(false);
        }

        public void UpdateTerrainChunk () {
            float viewerDistanceToEdge = Mathf.Sqrt( bounds.SqrDistance(playerPosition));
            bool visible = viewerDistanceToEdge <= viewDistance;
            SetVisible(visible);
        }

        public void SetVisible (bool visible) {
            meshObject.SetActive(visible);
        }

        public bool IsVisible () {
            return meshObject.activeSelf;
        }
    }

}

