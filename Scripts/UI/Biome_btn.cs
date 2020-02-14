using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class Biome_btn : MonoBehaviour, IPointerClickHandler
{
	public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            UIManager.S.CycleSelectedBiomeType();
            return;
        }
        UIManager.S.selectedCell.HandleClick(UIManager.ClickEventCodes.ChangeBiome);
    }
}
