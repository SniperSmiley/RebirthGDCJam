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
        WoodText.text = "Wood: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Wood];
        StoneText.text = "Stone: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone];
        FoodText.text = "Food: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Food];
        IronText.text = "Iron: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Iron];
        CopperText.text = "Copper: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Copper];
        BruxiteText.text = "Bruxite: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Buxite];
        TitaniumText.text = "Titanium: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Titanium];
        GoldText.text = "Gold: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Gold];
        CarbonText.text = "Carbon: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon];   
        EnergyText.text = "Energy: " + GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy];  
    }
}
