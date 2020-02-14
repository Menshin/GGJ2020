using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public List<Cell> neighbors = new List<Cell>();

    public enum EvolutionStage { Vide, Plantes, Herbivores, Carnivores };

    EvolutionStage evolutionStage = EvolutionStage.Vide;

    private SpriteRenderer evolutionSprite;
    private SpriteRenderer selectedSprite;
    private Planet planet;
    private AudioManager audioManager;
    public string biomeName;
    public bool isPentagon = false;
    public float genPropsFactor = 0.8f;
    private BiomeType biomeType;

    //for diffusion algorithm
    public bool processed = false;

    /*
    debug
    */
    public Vector3 center;

    void Start()
    {
        planet = Planet.S;
        audioManager = AudioManager.S;
        if (isPentagon)
        {
            createProps(GraphicsManager.S.boussolePrefab, 0.04f, 0.3f);
            return;
        }

        planet.cellsTotal++;
        
        GameObject c = new GameObject("animal");
        c.transform.SetParent(transform);
        c.transform.position = center;
        c.transform.localScale = new Vector3(0.04f, 0.04f, 0.04f);
        c.AddComponent<SpriteRenderer>();
        evolutionSprite = c.GetComponent<SpriteRenderer>();
        evolutionSprite.transform.LookAt(Vector3.zero);

        c = new GameObject("selected");
        c.transform.SetParent(transform);
        c.transform.position = center;
        c.transform.localScale = new Vector3(0.12f, 0.12f, 0.12f);
        c.AddComponent<SpriteRenderer>();
        selectedSprite = c.GetComponent<SpriteRenderer>();
        selectedSprite.sprite = GraphicsManager.S.selectedCellSprite;
        selectedSprite.enabled = false;
        selectedSprite.transform.LookAt(Vector3.zero);

        SetBiome(GraphicsManager.S.PickRandomBiome());
        addProps(biomeType.propsModelPrefab);
    }

    public void AddNeighbour(Cell new_neighbor)
    {
        neighbors.Add(new_neighbor);
    }

    public void SetBiome(string newBiome)
    {
        if (isPentagon || biomeName == newBiome)
            return;

        biomeName = newBiome;
        biomeType = GraphicsManager.S.biomes[biomeName];
        updateGraphics(false);
    }

    private void updateGraphics(bool spriteOnly = false)
    {
        if(!spriteOnly)
            GetComponent<MeshRenderer>().material = isAlive ? biomeType.aliveMat : biomeType.deadMat;

        switch (evolutionStage) {
            case EvolutionStage.Plantes:
                evolutionSprite.sprite = biomeType.plantsSprite;
                break;
            case EvolutionStage.Herbivores:
                evolutionSprite.sprite = biomeType.herbivorSprite;
                break;
            case EvolutionStage.Carnivores:
                evolutionSprite.sprite = biomeType.carnivorSprite;
                break;
            default:
                evolutionSprite.sprite = null;
                break;
        }
    }

    public void SetEvolutionStage(EvolutionStage evoStage)
    {
        if (isPentagon || evolutionStage == evoStage)
            return;

        bool awakens = !isAlive;

        evolutionStage = evoStage;

        switch (evolutionStage)
        {
            case EvolutionStage.Plantes:
                planet.plantsTotal++;
                break;
            case EvolutionStage.Herbivores:
                evolutionSprite.sprite = biomeType.herbivorSprite;
                planet.plantsTotal--;
                planet.herbivorsTotal++;
                break;
            case EvolutionStage.Carnivores:
                evolutionSprite.sprite = biomeType.carnivorSprite;
                planet.herbivorsTotal--;
                planet.carnivorsTotal++;
                break;
            default:
                evolutionSprite.sprite = null;
                Planet.S.AdjustCellsAlive(-1);
                Debug.Log("ERROR: cells shouldn't be able to devolve to nothingness.");
                return;
        }

        if (awakens)
        {
            Planet.S.AdjustCellsAlive(+1);
            updateGraphics(false);
            UIManager.S.clickMenuScript.UpdateGraphics(true);
        }
        else
        {
            updateGraphics(true);
        }

        

        audioManager.LaunchBiomeAudio(biomeType.soundIndex, awakens);
    }

    public List<Cell> GetConnectedCells()
    {

        List<Cell> connectedCells = new List<Cell>();
        Cell currentCell;

        connectedCells.Add(this);
        processed = true;

        for (int i = 0; i < connectedCells.Count; i++)
        {
            currentCell = connectedCells[i];

            foreach (Cell neighborCell in currentCell.neighbors)
            {
                if (neighborCell.processed)
                    continue;

                if ((!neighborCell.isPentagon) && (neighborCell.biomeType == biomeType) && (neighborCell.evolutionStage == (evolutionStage - 1)))
                {
                    connectedCells.Add(neighborCell);
                    neighborCell.processed = true;
                }

            }

        }

        foreach (Cell connectedCell in connectedCells)
            connectedCell.processed = false;

        return connectedCells;
    }

    public void HandleClick(UIManager.ClickEventCodes eventCode)
    {
        bool willPropagate = false;

        if (isPentagon)
            return;

        switch (eventCode)
        {
            case UIManager.ClickEventCodes.PutPlants:
                if (Planet.S.biomass < Planet.S.plantCost)
                    return;
                Planet.S.AdjustBiomass(-Planet.S.plantCost);
                SetEvolutionStage(EvolutionStage.Plantes);
                break;

            case UIManager.ClickEventCodes.PutHerbivors:
                if (Planet.S.biomass < Planet.S.herbivorCost)
                    return;
                Planet.S.AdjustBiomass(-Planet.S.herbivorCost);
                if (evolutionStage == EvolutionStage.Plantes)
                    willPropagate = true;
                SetEvolutionStage(EvolutionStage.Herbivores);
                break;

            case UIManager.ClickEventCodes.PutCarnivors:
                if (Planet.S.biomass < Planet.S.carnivorCost)
                    return;
                Planet.S.AdjustBiomass(-Planet.S.carnivorCost);
                if (evolutionStage == EvolutionStage.Herbivores)
                    willPropagate = true;
                SetEvolutionStage(EvolutionStage.Carnivores);
                break;

            case UIManager.ClickEventCodes.ChangeBiome:
                if ((Planet.S.biomass < Planet.S.changeBiomeCost) || (biomeName == UIManager.S.selectedBiomeName))
                    return;
                Planet.S.AdjustBiomass(-Planet.S.changeBiomeCost);
                SetBiome(UIManager.S.selectedBiomeName);
                if (UIManager.S.selectedCell == this)
                    UIManager.S.clickMenuScript.SetBiomeType(UIManager.S.selectedBiomeName);
                break;
        }

        if (willPropagate)
            Propagate();
    }

    public void Propagate()
    {
        List<Cell> connectedCells = this.GetConnectedCells();

        foreach (Cell cell in connectedCells)
        {
            cell.SetEvolutionStage(evolutionStage);
        }
    }

    public bool isAlive
    {
        get {
            if (isPentagon)
                return false;
            return (evolutionStage != EvolutionStage.Vide);
        }
    }

    public void createProps(GameObject propModel, float scale, float dist_from_center)
    {
        GameObject prop;

        prop = Instantiate(propModel) as GameObject;
        prop.transform.SetParent(transform);
        prop.transform.position = center;
        prop.transform.localScale = new Vector3(scale, scale, scale);
        prop.transform.LookAt(Vector3.zero);
        prop.transform.Rotate(new Vector3(-90, 0, 0), Space.Self);
        prop.transform.Translate(new Vector3(0, dist_from_center, 0), Space.Self);
    }

    public void addProps(GameObject propModel)
    {
        bool genProp = (Random.Range(0f, 100f) > (genPropsFactor * 100));

        if (genProp)
        {
            /*GameObject prop;
            prop = Instantiate(propModel) as GameObject;
            prop.transform.SetParent(transform);
            prop.transform.position = center;
            prop.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            prop.transform.LookAt(Vector3.zero);*/

            createProps(propModel, 0.1f, 0);
            //prop.transform.Rotate( new Vector3(-90, 0, 0), Space.Self );

        }
    }

    public void SetSelectedCell(bool isSelected) {
            //TOFIX ROTATION selectedSprite.enabled = isSelected;
    } 
}
