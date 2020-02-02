using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Predateur_btn : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
	public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Predateur_btn");
        click.cellselect.SetAnimal(1);
    }
}
