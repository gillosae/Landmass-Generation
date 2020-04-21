﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Take the noisemap and turn it into a texture
/// Apply the texture to a plane
/// </summary>
public class MapDisplay : MonoBehaviour
{
    public Renderer textureRenderer;
    public void DrawTexture(Texture2D texture)
    {
        textureRenderer.sharedMaterial.mainTexture = texture;
        textureRenderer.transform.localScale = new Vector3(texture.width, 1, texture.height);
    }
}
