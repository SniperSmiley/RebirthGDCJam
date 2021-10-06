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

    public GameObject Helf1;
    public GameObject Helf2;
    public GameObject Helf3;

    // Start is called before the first frame update
    void Start() {

    }

    // Update is called once per frame
    void Update() {
        OnUpdateUI();
    }

    public void OnUpdateUI() {
        WoodText.text = "Wood: " +  UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Wood]);
        StoneText.text = "Stone: " + UiTextCorrection.CorrectUiValue( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone]);
        FoodText.text = "Food: " + UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Food]);
        IronText.text = "Iron: " + UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Iron]);
        CopperText.text = "Copper: " + UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Copper]);
        BruxiteText.text = "Bruxite: " + UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Buxite]);
        TitaniumText.text = "Titanium: " + UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Titanium]);
        GoldText.text = "Gold: " + UiTextCorrection.CorrectUiValue(GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Gold]);
        CarbonText.text = "Carbon: " +UiTextCorrection.CorrectUiValue( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Carbon]);   
        EnergyText.text = "Energy: " +UiTextCorrection.CorrectUiValue( GameManagerScript.GameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy]);  
    }

    
    public void UpdateHelfUi(float helf) {
        Debug.Log("Update helf");

        switch (helf) {
            case 0: SetAllHeartsActive(false); break;
            case 1: SetAllHeartsActive(true); Helf3.SetActive(false); Helf2.SetActive(false); break;
            case 2: SetAllHeartsActive(true); Helf3.SetActive(false); break;
            case 3: SetAllHeartsActive(true);  break;
        }
    }

    private void SetAllHeartsActive(bool _active) {
        Helf1.SetActive(_active);
        Helf2.SetActive(_active);
        Helf3.SetActive(_active);
    }

}
