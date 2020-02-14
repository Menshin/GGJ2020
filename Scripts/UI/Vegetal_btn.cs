using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Vegetal_btn : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            UIManager.S.selectedCell.HandleClick(UIManager.ClickEventCodes.PutPlants);
    }

}
