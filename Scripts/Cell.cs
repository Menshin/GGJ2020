using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Cell> neighbor = new List<Cell>();

    public enum TypeResident {Herbivore, Predateur, Nature, Alone};

    public TypeResident type = TypeResident.Alone;
    public bool alive = false;

    public int ibiome = -1;

    public static Material[] DeadBiome;
    public static Material[] LiveBiome;
    void Start()
    {
        SetBiome(Random.Range(0, 6));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNeighbour(Cell new_neighbor)
    {
    	neighbor.Add(new_neighbor);
    }

    public void SetBiome(int option)
    {

        if (option < DeadBiome.Length)
        {
            ibiome = option;
             var biome = (alive) ? LiveBiome[option] : DeadBiome[option];
            GetComponent<MeshRenderer>().material = biome;
        }
        else
        {
            Debug.Log("No enough Material");
        }
    }

    public void SetAlive(bool isAlive)
    {
        alive = isAlive;
        SetBiome(ibiome);
    }
}
