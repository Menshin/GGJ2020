using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
	public List<Cell> child;

    public Material[] DeadBiome;
    public Material[] LiveBiome;
    public Sprite[] herb;
    public Sprite[] carnivore;
    public Sprite[] nature;

    public Sprite defaultSprite;

    public float biomass = 0;
    public float biomassMax = 30;
    public Slider slider;


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
        if (biomass + Time.deltaTime < biomassMax)
            biomass += Time.deltaTime;

       slider.value = biomass / biomassMax; 
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
    	Cell.herb = herb;
    	Cell.carnivore = carnivore;
    	Cell.nature = nature;
    }
}
