using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Generate Noise Map, return grids of values between 0 and 1
/// </summary>
public static class Noise
{
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if(scale <= 0) scale = 0.0001f;//Avoid division by zero error

        for(int y = 0; y< mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float sampleX = x / scale; //divide by scale to get non-integer values
                float sampleY = y / scale;

                float perlinValue = Mathf.PerlinNoise(sampleX, sampleY);
                noiseMap[x, y] = perlinValue;
            }
        }
        return noiseMap;
    }
}
