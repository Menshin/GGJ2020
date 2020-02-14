using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphicsManager : MonoBehaviour
{
    static public GraphicsManager S; //singleton
    
    [Header("Biome Prefabs")]
    public BiomeType desertBiomePrefab;
    public BiomeType oceanBiomePrefab;
    public BiomeType foretBiomePrefab;
    public BiomeType montagneBiomePrefab;
    public BiomeType prairieBiomePrefab;
    public BiomeType jungleBiomePrefab;
    public GameObject boussolePrefab;
    public Sprite selectedCellSprite;

    public Dictionary<string, BiomeType> biomes;
    private List<string> BiomeList = new List<string> { "jungle", "desert", "ocean", "prairie", "foret", "montagne" };
    private int selectedBiomeIndex = 0;

    public string PickRandomBiome()
    {
        return BiomeList[Random.Range(0, BiomeList.Count)];
    }

    private void Awake()
    {
        if (S)
            Debug.Log("ERROR : more than one instance of GraphicsManager has been created.");
        S = this;
    }

    private void Start()
    {
        biomes = new Dictionary<string, BiomeType>();

        biomes["desert"] = Instantiate<BiomeType>(desertBiomePrefab);
        biomes["jungle"] = Instantiate<BiomeType>(jungleBiomePrefab);
        biomes["ocean"] = Instantiate<BiomeType>(oceanBiomePrefab);
        biomes["montagne"] = Instantiate<BiomeType>(montagneBiomePrefab);
        biomes["prairie"] = Instantiate<BiomeType>(prairieBiomePrefab);
        biomes["foret"] = Instantiate<BiomeType>(foretBiomePrefab);
    }

    public string GetNextBiomeType()
    {
        selectedBiomeIndex = (selectedBiomeIndex + 1) % 6;
        return BiomeList[selectedBiomeIndex];
    }

    public string CurrentBiomeType
    {
        get { return BiomeList[selectedBiomeIndex]; }
    }
}
