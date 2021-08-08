using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class InGameUIScript : MonoBehaviour {

    public TextMeshProUGUI FoodText;
    public TextMeshProUGUI StoneText;
    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI IronText;
    public TextMeshProUGUI CopperText;
    public TextMeshProUGUI BruxiteText;
    public TextMeshProUGUI TitaniumText;
    public TextMeshProUGUI GoldText;
    public TextMeshProUGUI CarbonText;
    public TextMeshProUGUI EnergyText;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        OnUpdateUI();
    }

    public void OnUpdateUI() {
        WoodText.text = "Wood: " +  CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Wood]);
        StoneText.text = "Stone: " + CorrectUiValue( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone]);
        FoodText.text = "Food: " + CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Food]);
        IronText.text = "Iron: " + CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Iron]);
        CopperText.text = "Copper: " + CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Copper]);
        BruxiteText.text = "Bruxite: " + CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Buxite]);
        TitaniumText.text = "Titanium: " + CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Titanium]);
        GoldText.text = "Gold: " + CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Gold]);
        CarbonText.text = "Carbon: " +CorrectUiValue( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon]);   
        EnergyText.text = "Energy: " +CorrectUiValue( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy]);  
    }

    private string CorrectUiValue(float val) {
        float test;
        string newVal = "Error";
        // 1k   000
        // 10k  000
        // 100k  000
        // 1M    000 000

        if (val >= 1000000000) {
            newVal = Math.Round(val / 1000000000, 1).ToString() + "B"; 
        }

        else if (val >= 1000000) {
            newVal = Math.Round(val / 1000000, 1).ToString() + "M";   ///.ToString("B") + "M";
        }
        else if (val >= 1000) {
              newVal = Math.Round(val / 1000 , 1).ToString() + "K";  
        }

        else {
            newVal =  Math.Round(val,1).ToString();
        }

        return newVal;
    }
}
