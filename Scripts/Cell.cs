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
    public int ianimal = -1;

    public static int cellAlive = 0;
    public static int cellTotal = 0;

    public static Material[] DeadBiome;
    public static Material[] LiveBiome;
    public static Sprite[] herb;
    public static Sprite[] carnivore;
    public static Sprite[] nature;

    private SpriteRenderer child;

    private bool haveAnimal = false;


    /*
    debug
    */
    public Vector3 center;

    void Start()
    {
        cellTotal++;
        SetBiome(Random.Range(0, 6));

        GameObject c = new GameObject("animal");
        c.transform.SetParent(transform);
        c.transform.position = center;
        c.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        c.AddComponent<SpriteRenderer>();
        child = c.GetComponent<SpriteRenderer>();
        child.transform.LookAt(Vector3.zero);
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
        if (isAlive)
            cellAlive++;
        else
            cellAlive--; 
        SetBiome(ibiome);
    }

    //0 herbivore 1 carnivore 2 plant
    public void SetAnimal(int _ianimal)
    {
        haveAnimal = true;
        ianimal = _ianimal;

        if (ianimal == 0)
            child.sprite = herb[ibiome];
        else if (ianimal == 1)
            child.sprite = carnivore[ibiome];
        else
            child.sprite = nature[ibiome];
        foreach (var voisin in neighbor)
        {
            if (voisin.ibiome == ibiome)
            {
                voisin.SetAnimal(ianimal);
            }
        }
    }
}
