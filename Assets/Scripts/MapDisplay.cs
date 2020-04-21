using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Take the noisemap and turn it into a texture
/// Apply the texture to a plane
/// </summary>
public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public void DrawNoiseMap(float[,] noiseMap)
    {
        int width = noiseMap.GetLength(0); //1st dimension
        int height = noiseMap.GetLength(1); //2nd dimension

        Texture2D texture = new Texture2D(width, height);

        Color[] colorMap = new Color[width * height];
        for(int y=0; y<height; y++)
        {
            for(int x=0; x<width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, noiseMap[x, y]);
            }
        }
        texture.SetPixels(colorMap);
        texture.Apply();

        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(width, 1, height);
    }
}
