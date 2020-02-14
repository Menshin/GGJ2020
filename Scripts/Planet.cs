using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    static public Planet S; //singleton

    [Header("Gameplay Variables")]
    public float biomassUpdateDelay = 1f;
    public float biomassIncrement = 0.25f;
    public float perPlantIncrement = 0.645f;
    public float perHerbivorIncrement = 2.41f;
    public float perCarnivorIncrement = 5.05f;
    public float biomassMax = 4f;
    public float plantCost = 1f;
    public float herbivorCost = 2f;
    public float carnivorCost = 3f;
    public float changeBiomeCost = 4f;

    [Header("To visualize only : don't touch")]
    public float biomass = 0;
    public float cellsAlive = 0;
    public float cellsTotal = 0;
    public int plantsTotal = 0;
    public int herbivorsTotal = 0;
    public int carnivorsTotal = 0;
    
    private float nextBiomassUpdate;
    private float timeMax = 300f;

    private void Awake()
    {
        if (S)
        {
            Debug.Log("ERROR : Instanciating more than one Planet singleton");
        }

        S = this;
        nextBiomassUpdate = biomassUpdateDelay;
    }

    void Update()
    {
        //TODO
        if(Time.time > timeMax)
        {
            print("Booh, perdu !");
        }

        if (Time.time > nextBiomassUpdate)
        {
            float biomassToAdd = biomassUpdateDelay * (biomassIncrement
                + perPlantIncrement * plantsTotal / cellsTotal
                + perHerbivorIncrement * herbivorsTotal / cellsTotal
                + perCarnivorIncrement * carnivorsTotal / cellsTotal);

            AdjustBiomass(biomassToAdd);//biomass = Mathf.Min(biomass + biomassToAdd, biomassMax);
            nextBiomassUpdate = Time.time + biomassUpdateDelay;
        }

    }

    public void AdjustBiomass(float amount)
    {
        float oldbiomass = biomass;
        biomass = biomass = Mathf.Min(biomass + amount, biomassMax);

        if(biomass != oldbiomass)
            UIManager.S.UpdateBiomassBar();
    }

    public void AdjustCellsAlive(int amount)
    {
        cellsAlive += amount;
        UIManager.S.UpdateAliveMeter();
    }
    /*
    //TODEL
    private void OnGUI()
    {
        GUI.contentColor = Color.black;
        GUI.Label(new Rect(20, 40, 150, 40), "BIOMASS : " + biomass);
        GUI.Label(new Rect(20, 80, 200, 40), "Timer : " + Time.time);
        GUI.Label(new Rect(20, 120, 200, 40), "Alive/Total : " + Planet.S.cellsAlive + "/" + Planet.S.cellsTotal);
    }*/

}
