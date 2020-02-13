using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexTileBase : MonoBehaviour
{
    public int x;
    public int y;
    public int type;

    public int evolutionStage;
    
    //for diffusion algorithm
    public bool processed = false;
}
