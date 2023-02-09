using UnityEngine;
using UnityEditor;
using System.IO;

public class NoiseTextureGenerator : EditorWindow
{
    static public int resolution = 256;
    static public string path = "Assets/NoiseTexture.png";
    static public float scale = 1.0f;
    static public int octaves = 1;
    static public float persistence = 1.0f;
    static public float lacunarity = 2.0f;

    [MenuItem("Tools/Generate Noise Texture")]
    static void GenerateNoiseTexture()
    {
        // Create a new Texture2D with the specified resolution
        Texture2D noiseTex = new Texture2D(resolution, resolution);

        // Iterate through the pixels of the texture
        for (int y = 0; y < noiseTex.height; y++)
        {
            for (int x = 0; x < noiseTex.width; x++)
            {
                // Generate a noise value using the Perlin noise function
                float noise = Mathf.PerlinNoise((float)x / noiseTex.width * scale, (float)y / noiseTex.height * scale);

                // Set the pixel color to the noise value
                noiseTex.SetPixel(x, y, new Color(noise, noise, noise));
            }
        }

        // Apply the changes to the texture
        noiseTex.Apply();

        // Encode the texture to PNG format
        byte[] bytes = noiseTex.EncodeToPNG();

        // Write the bytes to a file
        File.WriteAllBytes(path, bytes);
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Noise Texture Generator")]
    public static void ShowWindow()
    {
        GetWindow<NoiseTextureGenerator>("Noise Texture Generator");
    }

    private void OnGUI()
    {
        resolution = EditorGUILayout.IntField("Resolution", resolution);
        path = EditorGUILayout.TextField("Path", path);
        scale = EditorGUILayout.FloatField("Scale", scale);
        octaves = EditorGUILayout.IntField("Octaves", octaves);
        persistence = EditorGUILayout.FloatField("Persistence", persistence);
        lacunarity = EditorGUILayout.FloatField("Lacunarity", lacunarity);

        if (GUILayout.Button("Generate Noise Texture"))
        {
            GenerateNoiseTexture();
        }
    }
}
