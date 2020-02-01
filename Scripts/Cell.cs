using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Cell> neighbor = new List<Cell>();

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddNeighbour(Cell new_neighbor)
    {
    	neighbor.Add(new_neighbor);
    }
}
