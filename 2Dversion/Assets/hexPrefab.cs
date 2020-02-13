using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hexPrefab : HexTileBase
{
    public TileMapGenerator parent;
    public List<hexPrefab> neighbors;

    public GameObject living;

    void Start()
    {
        List<hexPrefab> neighbors = new List<hexPrefab>();
    }
    public void OnMouseDown()
    {
        if (parent.selectedTile != this)
        {
            parent.selectedTile = this;
            parent.changedSelectedTile = true;
        }
    }

    public bool handleClick(int clickType)
    {
        return (clickType > evolutionStage);

    }

    public void ChangeBiome(int biomeType)
    {
        type = biomeType;
        GetComponent<SpriteRenderer>().sprite = parent.spriteList[biomeType + (evolutionStage > 0 ? 6 : 0)];
    }

}
