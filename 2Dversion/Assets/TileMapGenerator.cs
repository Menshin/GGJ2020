using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileMapGenerator : MonoBehaviour
{
    public GameObject go;
    
    public const int tileDesert =   0;
    public const int tileForet =    1;
    public const int tilePrairie =   2;
    public const int tileMontagne = 3;
    public const int tileOcean =    4;
    public const int tileJungle =   5;

    public const int evoStageEmpty = 0;
    public const int evoStagePlants = 1;
    public const int evoStageHerbivorous = 2;
    public const int evoStageCarnivorous = 3;

    public const float plantsThreshold = 0.25f;
    public const float herbivorousThreshold = 0.5f;
    public const float carnivorousThreshold = 0.75f;
    public const float changeBiomeThreshold = 1f;

    public const int clickChangePlants = 1;
    public const int clickChangeHerbivorous = 2;
    public const int clickChangeCarnivorous = 3;
    public const int clickChangeBiome = 4;

    public List<Sprite> PlantsSprites;
    public List<Sprite> HerbivorousSprites;
    public List<Sprite> CarnivorousSprites;

    public Sprite desertPlantsSprite;
    public Sprite foretPlantsSprite;
    public Sprite prairiePlantsSprite;
    public Sprite montagnePlantsSprite;
    public Sprite oceanPlantsSprite;
    public Sprite junglePlantsSprite;

    public Sprite desertHerbivorousSprite;
    public Sprite foretHerbivorousSprite;
    public Sprite prairieHerbivorousSprite;
    public Sprite montagneHerbivorousSprite;
    public Sprite oceanHerbivorousSprite;
    public Sprite jungleHerbivorousSprite;

    public Sprite desertCarnivorousSprite;
    public Sprite foretCarnivorousSprite;
    public Sprite prairieCarnivorousSprite;
    public Sprite montagneCarnivorousSprite;
    public Sprite oceanCarnivorousSprite;
    public Sprite jungleCarnivorousSprite;

    public Sprite desertDeadSprite;
    public Sprite foretDeadSprite;
    public Sprite prairieDeadSprite;
    public Sprite montagneDeadSprite;
    public Sprite oceanDeadSprite;
    public Sprite jungleDeadSprite;
    public Sprite desertLivingSprite;
    public Sprite foretLivingSprite;
    public Sprite prairieLivingSprite;
    public Sprite montagneLivingSprite;
    public Sprite oceanLivingSprite;
    public Sprite jungleLivingSprite;
    public List<Sprite> spriteList;

    public int selectedBiomeType = 0;

    public hexPrefab foretPlus;
    public hexPrefab foretMoins;
    public hexPrefab desertPlus;
    public hexPrefab desertMoins;
    public hexPrefab oceanPlus;
    public hexPrefab oceanMoins;
    public hexPrefab prairiePlus;
    public hexPrefab prairieMoins;
    public hexPrefab montagnePlus;
    public hexPrefab montagneMoins;
    public hexPrefab junglePlus;
    public hexPrefab jungleMoins;
    public hexPrefab highlightTileSprite;
    public bool changedSelectedTile = false;

    int width = 15;
    int height = 15;

    float xOffset = 0.88f;
    float yOffset = 0.763f;
    float xOddOffset;

    List<hexPrefab> tileTypes;
    List<hexPrefab> gameTiles;

    //List<hexPrefab> DEBUGNEIGHBORS;

    public hexPrefab selectedTile;

    float biomass = 0f;
    float nextBiomassUpdate = 0f;
    float biomassUpdateDelay = 1f;
    float totalUsedBiomass = 0f;
    float totalNumOfTiles;
    float totalAliveTiles = 0;
    float totalPlants = 0;
    float totalHerbivore = 0;
    float totalCarnivore = 0;
    //float MaxTime = 300f;
    float time = 300f;

    int population;

    private void Awake()
    {
        gameTiles = new List<hexPrefab>();
        tileTypes = new List<hexPrefab> { desertMoins, foretMoins, prairieMoins, montagneMoins, oceanMoins, jungleMoins };
        spriteList = new List<Sprite> { desertDeadSprite, foretDeadSprite, prairieDeadSprite, montagneDeadSprite, oceanDeadSprite, jungleDeadSprite,
                                        desertLivingSprite, foretLivingSprite, prairieLivingSprite, montagneLivingSprite, oceanLivingSprite, jungleLivingSprite };
        PlantsSprites = new List<Sprite> { desertPlantsSprite, foretPlantsSprite, prairiePlantsSprite, montagnePlantsSprite, oceanPlantsSprite, junglePlantsSprite };
        HerbivorousSprites = new List<Sprite> { desertHerbivorousSprite, foretHerbivorousSprite, prairieHerbivorousSprite, montagneHerbivorousSprite, oceanHerbivorousSprite, jungleHerbivorousSprite };
        CarnivorousSprites = new List<Sprite> { desertCarnivorousSprite, foretCarnivorousSprite, prairieCarnivorousSprite, montagneCarnivorousSprite, oceanCarnivorousSprite, jungleCarnivorousSprite };

        totalNumOfTiles = height * width;
        //DEBUGNEIGHBORS = new List<hexPrefab>();
    }

    void Start()
    {
        for (int i = 0; i < width; i++)
        {
            if (i % 2 == 0)
                xOddOffset = 0;
            else
                xOddOffset = -0.44f;

            for (int j = 0; j < height; j++)
            {
                int tileType = Random.Range(0, tileTypes.Count);
                hexPrefab randomTile = tileTypes[tileType];
                hexPrefab tempGo = Instantiate(randomTile, new Vector3(j*xOffset + xOddOffset, i*yOffset,0), Quaternion.identity );
                tempGo.name = "Hex (" + i + ", " + j + ")";
                tempGo.x = j;
                tempGo.y = i;
                tempGo.parent = this;
                tempGo.transform.SetParent(this.transform);
                tempGo.GetComponent<SpriteRenderer>().sortingOrder = 2;
                tempGo.type = tileType;
                tempGo.evolutionStage = evoStageEmpty;
                tempGo.living = Instantiate(go, tempGo.GetComponent<Transform>().position, Quaternion.identity);
                tempGo.living.GetComponent<SpriteRenderer>().enabled = false;
                tempGo.living.GetComponent<SpriteRenderer>().sortingOrder = 4;
                gameTiles.Add(tempGo);
            }

        }

        for (int i = 0; i < tileTypes.Count; i++)
        {
            Destroy(tileTypes[i]);
        }

        MakeNeighbors();

    }

    public void MakeNeighbors()
    {
        int offset = 1;
        bool onRightBorder, onLeftBorder = false;
        hexPrefab currentTile;
        hexPrefab neighborTile;

        //horizontal neighbors
        for (int i = 0; i < gameTiles.Count; i++)
        {

            onRightBorder = ((i % width) == (width - 1));

            currentTile = gameTiles[i];
            
            if (!onRightBorder)
            {
                neighborTile = gameTiles[i + 1];
                currentTile.neighbors.Add(neighborTile);
                neighborTile.neighbors.Add(currentTile);
            }

            if (onRightBorder)
                offset = 1 - offset;
        }

        //diagonal up neighbors
        offset = 1;

        for (int i = 0; i < gameTiles.Count - width; i++)
        {
            onRightBorder = ((i % width) == (width - 1));
            
            currentTile = gameTiles[i];
            
            
            if (!onRightBorder || (offset == 0))
            {
                neighborTile = gameTiles[i + width + offset];
                currentTile.neighbors.Add(neighborTile);
                neighborTile.neighbors.Add(currentTile);
            }

            if (onRightBorder)
                offset = 1 - offset;

        }

        //diagonal down neighbors
        offset = 0;

        for (int i = 0; i < gameTiles.Count - width; i++)
        {
            onRightBorder = ((i % width) == (width - 1));
            onLeftBorder = ((i % width) == 0);

            currentTile = gameTiles[i];

            if (!onLeftBorder || (offset == 0))
            {
                neighborTile = gameTiles[i + width - offset];
                currentTile.neighbors.Add(neighborTile);
                neighborTile.neighbors.Add(currentTile);
            }

            if (onRightBorder)
                offset = 1 - offset;

        }
    }

    public void Update()
    {

        if (Time.time > nextBiomassUpdate)
        {
            time--;
            biomass = Mathf.Min(biomass + 0.2f, 1);
            /*biomass = Mathf.Min(biomass + (0.5f * totalNumOfTiles / Time.time +
            0.95f * totalPlants / totalNumOfTiles +
            3.55f * totalHerbivore / totalNumOfTiles +
            6.69f * totalCarnivore / totalNumOfTiles) * biomassUpdateDelay,4);*/

            nextBiomassUpdate = Time.time + biomassUpdateDelay;
        }

        if (changedSelectedTile)
        {
            highlightTileSprite.GetComponent<Transform>().position = selectedTile.GetComponent<Transform>().position;

            /*for (int i=0; i < DEBUGNEIGHBORS.Count; i++) {
                hexPrefab hp = DEBUGNEIGHBORS[i];
                Destroy(hp);
                DEBUGNEIGHBORS.Clear();
                    }*/

            /*foreach (hexPrefab hp in selectedTile.neighbors) {
                hexPrefab temp = Instantiate(highlightTileSprite, hp.GetComponent<Transform>().position, Quaternion.identity); 
                DEBUGNEIGHBORS.Add(temp);
                    }*/
            /*List<hexPrefab> chain = GetConnectedTiles(selectedTile);

            foreach (hexPrefab hp in chain)
            {
                hexPrefab temp = Instantiate(highlightTileSprite, hp.GetComponent<Transform>().position, Quaternion.identity);
            }*/

            changedSelectedTile = false;
        }
        
    }

    public List<hexPrefab> GetConnectedTiles(hexPrefab originTile, int evoStage)
    {
        List<hexPrefab> connectedTiles = new List<hexPrefab>();
        hexPrefab currentTile;
        int tileType = originTile.type;

        connectedTiles.Add(originTile);
        originTile.processed = true;

        for (int i = 0; i < connectedTiles.Count; i++)
        {
            currentTile = connectedTiles[i];

            foreach (hexPrefab neighborTile in currentTile.neighbors)
            {
                if (neighborTile.processed)
                    continue;

                if ((neighborTile.type == tileType) && (neighborTile.evolutionStage == (evoStage - 1)))
                {
                    connectedTiles.Add(neighborTile);
                    neighborTile.processed = true;
                }

            }

        }

        foreach (hexPrefab connectedTile in connectedTiles)
            connectedTile.processed = false;

        return connectedTiles;
    }

    string Type2Name(int tileType)
    {
        switch (tileType)
        {
            case 0:
                return "DESERT";
            case 1:
                return "FOREST";
            case 2:
                return "GRASSLAND";
            case 3:
                return "MOUNTAINS";
            case 4:
                return "OCEAN";
            case 5:
                return "JUNGLE";
            default:
                return "Error";
        }

    }

    string EvoType2Name(int evoStage)
    {
        switch (evoStage)
        {
            case 0:
                return "EMPTY";
            case 1:
                return "PLANTS";
            case 2:
                return "HERBIVOROUS";
            case 3:
                return "CARNIVOROUS";
            default:
                return "Error";
        }

    }

    public void Propagate(hexPrefab originTile, int evoStage)
    {

        List<hexPrefab> connectedTiles = GetConnectedTiles(originTile, evoStage);

        for(int i = 1; i < connectedTiles.Count; i++)
        {
            hexPrefab currentTile = connectedTiles[i];
            currentTile.evolutionStage = evoStage;
            UpdateSprite(currentTile);
        }

    }

    void OnGUI()
    {
        GUI.Label(new Rect(20, 40, 150, 40), "BIOMASS : " + biomass);
        GUI.Label(new Rect(20, 80, 200, 40), "Timer : " + time);
        GUI.Label(new Rect(20, 120, 200, 40), "Alive/Total : " + totalAliveTiles + "/" + totalNumOfTiles);

        if (selectedTile)
        {
            //GUI.Label(new Rect(20, 120, 150, 40), "(" + selectedTile.x + ", " + selectedTile.y + ")");
            /*GUI.Label(new Rect(20, 150, 150, 40), "TYPE : " + Type2Name(selectedTile.type));
            GUI.Label(new Rect(20, 180, 230, 40), "EVO STAGE : " + EvoType2Name(selectedTile.evolutionStage));*/
            
            /*int offset = 30;
            GUI.Label(new Rect(20, 210, 150, 40), "Neighbors :");
            
            foreach (hexPrefab hp in selectedTile.neighbors)
            {
                GUI.Label(new Rect(20, 210 + offset, 150, 40), "(" + hp.x + ", " + hp.y + ")");
                offset += 30;
            }*/

            //buttons
            if (GUI.Button(new Rect(20, 250, 150, 40), "Plants"))
            {
                if(biomass >= plantsThreshold)
                {
                    if (selectedTile.handleClick(clickChangePlants))
                    {
                        biomass -= plantsThreshold;
                        totalUsedBiomass += plantsThreshold;
                        totalAliveTiles++;
                        totalPlants++;
                        selectedTile.evolutionStage = evoStagePlants;
                        selectedTile.GetComponent<SpriteRenderer>().sprite = spriteList[selectedTile.type + 6];

                        selectedTile.living.GetComponent<SpriteRenderer>().sprite = PlantsSprites[selectedTile.type];
                        selectedTile.living.GetComponent<SpriteRenderer>().enabled = true;
                    }
                }
                
            }

            if (GUI.Button(new Rect(20, 300, 150, 40), "Herbivorous"))
            {
                if (biomass >= herbivorousThreshold)
                {
                    if (selectedTile.handleClick(clickChangeHerbivorous))
                    {
                        bool willPropagate = (selectedTile.evolutionStage == evoStagePlants);
                        biomass -= herbivorousThreshold;
                        totalUsedBiomass += herbivorousThreshold;
                        if (selectedTile.evolutionStage == evoStageEmpty)
                        {
                            selectedTile.GetComponent<SpriteRenderer>().sprite = spriteList[selectedTile.type + 6];
                            totalAliveTiles++;
                        }

                        totalHerbivore++;
                        if(selectedTile.evolutionStage == evoStagePlants)
                            totalPlants--;
                        selectedTile.evolutionStage = evoStageHerbivorous;
                        selectedTile.living.GetComponent<SpriteRenderer>().sprite = HerbivorousSprites[selectedTile.type];
                        selectedTile.living.GetComponent<SpriteRenderer>().enabled = true;
                        if (willPropagate)
                            Propagate(selectedTile, evoStageHerbivorous);
                    }
                }
            }

            if (GUI.Button(new Rect(20, 350, 150, 40), "Carnivorous"))
            {
                if (biomass >= carnivorousThreshold)
                {
                    if (selectedTile.handleClick(clickChangeCarnivorous)){
                        bool willPropagate = (selectedTile.evolutionStage == evoStageHerbivorous);
                        biomass -= carnivorousThreshold;
                        totalUsedBiomass += carnivorousThreshold;
                        if (selectedTile.evolutionStage == evoStageEmpty)
                        {
                            selectedTile.GetComponent<SpriteRenderer>().sprite = spriteList[selectedTile.type + 6];
                            totalAliveTiles++;
                        }
                        totalCarnivore++;
                        if (selectedTile.evolutionStage == evoStagePlants)
                            totalPlants--;
                        if (selectedTile.evolutionStage == evoStageHerbivorous)
                            totalHerbivore--;
                        selectedTile.evolutionStage = evoStageCarnivorous;
                        selectedTile.living.GetComponent<SpriteRenderer>().sprite = CarnivorousSprites[selectedTile.type];
                        selectedTile.living.GetComponent<SpriteRenderer>().enabled = true;
                        if (willPropagate)
                            Propagate(selectedTile, evoStageCarnivorous);
                    }
                }
            }

            if (GUI.Button(new Rect(20, 400, 150, 40), "Change Biome"))
            {
                if ((biomass >= changeBiomeThreshold) && (selectedTile.type != selectedBiomeType))
                {
                    if (selectedTile.handleClick(clickChangeBiome))
                    {
                        biomass -= changeBiomeThreshold;
                        totalUsedBiomass += changeBiomeThreshold;
                        selectedTile.ChangeBiome(selectedBiomeType);
                        UpdateSprite(selectedTile);
                    }
                }
            }

            if (GUI.Button(new Rect(20, 450, 200, 40), "Biome type : " + Type2Name(selectedBiomeType)))
            {
                selectedBiomeType = (selectedBiomeType + 1) % 6;
            }

        }
        
    }

    public void UpdateSprite(hexPrefab tile)
    {
        switch (tile.evolutionStage) {
        case 1:
            tile.living.GetComponent<SpriteRenderer>().sprite = PlantsSprites[selectedTile.type];
            break;
        
        case 2:
            tile.living.GetComponent<SpriteRenderer>().sprite = HerbivorousSprites[selectedTile.type];
            break;

        case 3:
            tile.living.GetComponent<SpriteRenderer>().sprite = CarnivorousSprites[selectedTile.type];
            break;
        }

        tile.living.GetComponent<SpriteRenderer>().enabled = true;
    }

}
