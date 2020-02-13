using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Proie_btn : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
	public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Proie_btn");
        click.cellselect.SetAnimal(0);
    }
}
