using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Create Texture from Map
/// </summary>
public static class TextureGenerator
{
    public static Texture2D TextureFromColorMap(Color[] colorMap, int width, int height) {
        Texture2D texture = new Texture2D(width, height);
        texture.filterMode = FilterMode.Point; //Point filtering - texture pixels become blocky up close.
        texture.wrapMode = TextureWrapMode.Clamp; //Clamps the texture to the last pixel at the edge. <-> Repeat
        texture.SetPixels(colorMap);
        texture.Apply();
        return texture;
    }

    public static Texture2D TextureFromHeightMap(float [,] heightMap)
    {
        int width = heightMap.GetLength(0); //1st dimension
        int height = heightMap.GetLength(1); //2nd dimension

        Color[] colorMap = new Color[width * height];
        for (int y = 0; y < height; y++)
        {
            for (int x = 0; x < width; x++)
            {
                colorMap[y * width + x] = Color.Lerp(Color.black, Color.white, heightMap[x, y]);
            }
        }

        return TextureFromColorMap(colorMap, width, height);
    }
}
