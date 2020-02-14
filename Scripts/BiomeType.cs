using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiomeType : MonoBehaviour
{
    [Header("Biome fields")]
    public string biomeName;
    public Material deadMat;
    public Material aliveMat;
    public Sprite deadSprite;
    public Sprite aliveSprite;
    public Sprite plantsSprite;
    public Sprite herbivorSprite;
    public Sprite carnivorSprite;
    public int soundIndex;
    public GameObject propsModelPrefab;

}
