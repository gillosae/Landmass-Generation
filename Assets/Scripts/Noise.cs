using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Generate Noise Map, return grids of values between 0 and 1
/// </summary>
public static class Noise
{
    /// <param name="octaves">Noises added up to create single noise</param>
    /// <param name="persistance">Controls decrease in amplitude of octaves</param>
    /// <param name="lacunarity">Controls increase in frequency octaves</param>
    public static float[,] GenerateNoiseMap(int mapWidth, int mapHeight, int seed, float scale, int octaves, float persistance, float lacunarity, Vector2 offset)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        System.Random prng = new System.Random(seed); //Pseudo-random number generator
        Vector2[] octaveOffsets = new Vector2[octaves];
        for(int i=0; i<octaves; i++) {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetY = prng.Next(-100000, 100000) + offset.y;
            octaveOffsets[i] = new Vector2(offsetX, offsetY);
        }

        if (scale <= 0) scale = 0.0001f; //Avoid division by zero

        float maxNoiseHeight = float.MinValue;
        float minNoiseHeight = float.MaxValue;

        float halfWidth = mapWidth / 2;
        float halfHeight = mapHeight / 2;

        for(int y = 0; y < mapHeight; y++)
        {
            for(int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1;
                float frequency = 1;
                float noiseHeight = 0;

                for(int i = 0; i < octaves; i++)
                {
                    float sampleX = (x-halfWidth) / scale * frequency + octaveOffsets[i].x; //Divide by scale to get non-integer values
                    float sampleY = (y-halfHeight) / scale * frequency + octaveOffsets[i].y; //The higher the frequency, the further apart the sample points. Height values will change more rapidly

                    float perlinValue = Mathf.PerlinNoise(sampleX, sampleY) * 2 - 1; //Allow negative value, -1 to 1
                    noiseHeight += perlinValue * amplitude; //Increase noise height by perlin value of each octave

                    amplitude *= persistance; //Persistance is 0 to 1, Amplitude decreases each octave
                    frequency *= lacunarity; //Lacunarity is greater than 1, Frequency increases each octave
                }

                if(noiseHeight > maxNoiseHeight)
                {
                    maxNoiseHeight = noiseHeight;
                }
                else if(noiseHeight < minNoiseHeight)
                {
                    minNoiseHeight = noiseHeight;
                }
                noiseMap[x, y] = noiseHeight;//Apply noise height to noise map
            }
        }

        //Normalize to make values back to 0 to 1
        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                noiseMap[x, y] = Mathf.InverseLerp(minNoiseHeight, maxNoiseHeight, noiseMap[x, y]); //Returns a value between 0 to 1
            }
        }

        return noiseMap; 
    }
}
