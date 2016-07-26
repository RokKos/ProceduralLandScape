//Author: Rok Kos <kosrok97@gmail.com>
//File: NoiseClass.cs
//File path: D:\Documents\Unity\ProceduralLandScape\Assets\Editor\GenerateEditor.cs
//Date: 26.07.2016
//Description: Script that overides editor and let us create map in editor

using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor (typeof (MapGenerator))]
public class GenerateEditor : Editor {

    public override void OnInspectorGUI () {
        MapGenerator mapGenerator = (MapGenerator)target;

        // Create default inspector and cheks if values were updated
        if (DrawDefaultInspector()) {
            if (mapGenerator.autoUpdate) {
                mapGenerator.GenerateMap();
            }
        }

        // But ads the button to generate map that trigers funtion if pressed
        if (GUILayout.Button("Generate noise map")) {
            mapGenerator.GenerateMap();
        }

    }
}
