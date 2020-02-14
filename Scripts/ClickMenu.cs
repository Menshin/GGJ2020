using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ClickMenu : MonoBehaviour
{
    [Header("Buttons gameobjects")]
    public GameObject plantsButton;
    public GameObject herbivorsButton;
    public GameObject carnivorsButton;
    public GameObject changeBiomeButton;

    private Image plantsButtonIcon;
    private Image herbivorsButtonIcon;
    private Image carnivorsButtonIcon;
    private Image changeBiomeButtonIcon;
    private Vector3 initialScaleVector;
    private float initialScale;

    private BiomeType selectedCellBiomeType;

    void Start()
    {
        gameObject.SetActive(false);
        plantsButtonIcon = plantsButton.transform.GetChild(0).GetComponent<Image>();
        herbivorsButtonIcon = herbivorsButton.transform.GetChild(0).GetComponent<Image>();
        carnivorsButtonIcon = carnivorsButton.transform.GetChild(0).GetComponent<Image>();
        changeBiomeButtonIcon = changeBiomeButton.transform.GetChild(0).GetComponent<Image>();
        initialScaleVector = transform.localScale;
        initialScale = UIManager.S.mainCamera.transform.position.magnitude;
    }

    public void SetBiomeType(string biomeType)
    {
        if (biomeType == "None")
            selectedCellBiomeType = null;
        else
            selectedCellBiomeType = GraphicsManager.S.biomes[biomeType];

        UpdateGraphics(false);
    }

    public void UpdateGraphics(bool changeBiomeOnly = false)
    {
        if (selectedCellBiomeType == null)
        {
            plantsButtonIcon.gameObject.SetActive(false);
            herbivorsButtonIcon.gameObject.SetActive(false);
            carnivorsButtonIcon.gameObject.SetActive(false);
            changeBiomeButtonIcon.gameObject.SetActive(false);
            return;
        }

        changeBiomeButtonIcon.sprite = UIManager.S.selectedCell.isAlive ? UIManager.S.selectedBiomeType.aliveSprite : UIManager.S.selectedBiomeType.deadSprite;

        if (changeBiomeOnly)
            return;

        plantsButtonIcon.sprite = selectedCellBiomeType.plantsSprite;
        herbivorsButtonIcon.sprite = selectedCellBiomeType.herbivorSprite;
        carnivorsButtonIcon.sprite = selectedCellBiomeType.carnivorSprite;
        
        plantsButtonIcon.gameObject.SetActive(true);
        herbivorsButtonIcon.gameObject.SetActive(true);
        carnivorsButtonIcon.gameObject.SetActive(true);
        changeBiomeButtonIcon.gameObject.SetActive(true);

    }

    public void ResetScaling()
    {
        transform.localScale = initialScaleVector;
    }
    public void UpdateScaling() {
        float newScale = initialScale / UIManager.S.mainCamera.transform.position.magnitude;
        transform.localScale = initialScaleVector * newScale;
    }
}
