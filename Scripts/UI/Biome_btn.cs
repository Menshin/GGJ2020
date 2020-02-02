using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Biome_btn : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
	public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Biome_btn");
    }
}
