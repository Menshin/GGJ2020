using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Predateur_btn : MonoBehaviour, IPointerClickHandler
{
    // Start is called before the first frame update
	public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left)
            UIManager.S.selectedCell.HandleClick(UIManager.ClickEventCodes.PutCarnivors);
    }
}
