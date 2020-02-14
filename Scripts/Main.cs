using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    [Header("Game prefabs")]
    public AudioManager audioManagerPrefab;
    public generator generatorPrefab;
    
    void Start()
    {
        Instantiate<generator>(generatorPrefab);
        Instantiate<AudioManager>(audioManagerPrefab);
    }

}
