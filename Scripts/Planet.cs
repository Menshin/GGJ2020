using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Planet : MonoBehaviour
{
	public List<Cell> child;

    public Material[] DeadBiome;
    public Material[] LiveBiome;
	/*
	0 Jungle, 1 Prairie, 2 Ocean, 3 Montagne, 4 Foret, 5 Desert
	*/
    // Start is called before the first frame update
    void Start()
    {
    	SetMaterialPckg(DeadBiome, LiveBiome);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Terraform()
    {
    	foreach (Cell children in child)
    	{
    		children.SetAlive(false);
    		children.SetBiome(Random.Range(0, 6));
    	}
    }

    void SetMaterialPckg(Material[] Dead, Material[] Alive)
    {
    	Cell.DeadBiome = Dead;
    	Cell.LiveBiome = Alive;
    }
}
