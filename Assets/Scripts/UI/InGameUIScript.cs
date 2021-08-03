using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
        WoodText.text = "Wood: " + GameManagerScript.GameManager.Wood;
        StoneText.text = "Stone: " + GameManagerScript.GameManager.Stone;
        FoodText.text = "Food: " + GameManagerScript.GameManager.Food;
        IronText.text = "Iron: " + GameManagerScript.GameManager.Iron;
        CopperText.text = "Copper: " + GameManagerScript.GameManager.Copper;
        BruxiteText.text = "Bruxite: " + GameManagerScript.GameManager.Bruxite;
        TitaniumText.text = "Titanium: " + GameManagerScript.GameManager.Titanium;
        GoldText.text = "Gold: " + GameManagerScript.GameManager.Gold;
        CarbonText.text = "Carbon: " + GameManagerScript.GameManager.Carbon;   
        EnergyText.text = "Energy: " + GameManagerScript.GameManager.Energy;  
    }
}
