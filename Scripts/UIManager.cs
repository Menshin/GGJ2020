using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UIManager : MonoBehaviour
{
    static public UIManager S; //singleton

    public Canvas HUD;
    public GameObject clickMenu;
    public ClickMenu clickMenuScript;
    public Camera mainCamera;
    public CameraMove mainCameraScript;

    public RectTransform biomassClippingRect;
    public RectTransform aliveClippingRect;
    private float biomassClippingRectMaxWidth;
    private float aliveClippingRectMaxWidth;

    public GraphicRaycaster raycaster;
    public EventSystem eventSystem;
    private List<RaycastResult> graphicRaycastResults;
    private Vector3 mousepos;

    public enum ClickEventCodes { PutPlants, PutHerbivors, PutCarnivors, ChangeBiome };
    public string selectedBiomeName = "jungle";
    public BiomeType selectedBiomeType;
    public Cell selectedCell;

    private void Awake()
    {
        if (S)
        {
            Debug.Log("ERROR: shouldn't be able to instantiate more than one UIManager.");
        }

        S = this;
    }

    private void Start()
    {
        raycaster = HUD.GetComponent<GraphicRaycaster>();
        eventSystem = HUD.GetComponent<EventSystem>();
        graphicRaycastResults = new List<RaycastResult>();

        clickMenuScript = clickMenu.GetComponent<ClickMenu>();
        mainCameraScript = mainCamera.GetComponent<CameraMove>();

        biomassClippingRectMaxWidth = biomassClippingRect.sizeDelta.x;
        biomassClippingRect.sizeDelta = new Vector2(0, biomassClippingRect.sizeDelta.y);

        aliveClippingRectMaxWidth = aliveClippingRect.sizeDelta.x;
        aliveClippingRect.sizeDelta = new Vector2(0, aliveClippingRect.sizeDelta.y);
    }

    public void UpdateBiomassBar()
    {
        biomassClippingRect.sizeDelta = new Vector2(biomassClippingRectMaxWidth * Planet.S.biomass / Planet.S.biomassMax, biomassClippingRect.sizeDelta.y);
    }

    public void UpdateAliveMeter()
    {
        aliveClippingRect.sizeDelta = new Vector2(aliveClippingRectMaxWidth * Planet.S.cellsAlive / Planet.S.cellsTotal, biomassClippingRect.sizeDelta.y);
    }

    public bool CheckClickedUI(Vector3 mousepos)
    {
        PointerEventData pointerEventData = new PointerEventData(UIManager.S.eventSystem);
        pointerEventData.position = mousepos;

        UIManager.S.raycaster.Raycast(pointerEventData, graphicRaycastResults);

        if (graphicRaycastResults.Count == 0)
            return false;

        graphicRaycastResults.Clear();
        return true;
    }

    public void HideClickMenu()
    {
        clickMenu.SetActive(false);
        clickMenuScript.SetBiomeType("None");
        selectedCell = null;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            CycleSelectedBiomeType();
        bool isRightClick = Input.GetMouseButtonDown(1);

        if (Input.GetMouseButtonDown(0) || isRightClick)
        {
            mousepos = Input.mousePosition;

            //is there a UI clickable object in our way ?
            if (CheckClickedUI(mousepos))
                return;

            //right clicked and not intercepted by a UI element, so should be handled by Camera Move
            if (isRightClick)
            {
                mainCameraScript.HandleRightClick();
                return;
            }

            //neither, so let's try to select a cell
            Ray ray = Camera.main.ScreenPointToRay(mousepos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 10.0f))
            {
                Cell clickedCell = hit.collider.gameObject.GetComponent<Cell>();

                if ((clickedCell == selectedCell) || clickedCell.isPentagon)
                {
                    if(selectedCell)
                        selectedCell.SetSelectedCell(false);
                    HideClickMenu();
                }
                else
                {
                    if (selectedCell)
                        selectedCell.SetSelectedCell(false);
                    selectedCell = clickedCell;
                    selectedCell.SetSelectedCell(true);
                    clickMenu.transform.position = mousepos;
                    clickMenu.SetActive(true);
                    clickMenuScript.SetBiomeType(clickedCell.biomeName);
                }
            }
        }
    }

    public void CycleSelectedBiomeType()
    {
        selectedBiomeName = GraphicsManager.S.GetNextBiomeType();
        selectedBiomeType = GraphicsManager.S.biomes[selectedBiomeName];
        clickMenuScript.UpdateGraphics(true);
    }
}
