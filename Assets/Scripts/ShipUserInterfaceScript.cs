using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShipUserInterfaceScript : MonoBehaviour {

    public Color NotEnough;
    public Color EnoughResources;

    public GameObject Repaired;
    public GameObject Broken;

    private GameManagerScript script;

    public UpgradeCostsSO RepairCosts;

    public TextMeshProUGUI EnergyTxt;
    public TextMeshProUGUI WoodTxt;
    public TextMeshProUGUI StoneTxt;
    public TextMeshProUGUI IronTxt;
    public TextMeshProUGUI CopperTxt;
    public TextMeshProUGUI BruxiteTxt;
    public TextMeshProUGUI GoldTxt;
    public TextMeshProUGUI TitaniumTxt;


    private void Awake() {
        script = GameManagerScript.GameManager;
    }


    // Start is called before the first frame update
    void Start() {

        Broken.SetActive(true);
        Repaired.SetActive(false);

        EnergyTxt.text = "Energy: " + CorrectUiValue(RepairCosts.Energy);
        WoodTxt.text = "Wood: " + CorrectUiValue(RepairCosts.Wood);
        StoneTxt.text = "Stone: " + CorrectUiValue(RepairCosts.Stone);
        IronTxt.text = "Iron: " + CorrectUiValue(RepairCosts.Iron);
        CopperTxt.text = "Copper: " + CorrectUiValue(RepairCosts.Copper);
        BruxiteTxt.text = "Bruxite: " + CorrectUiValue(RepairCosts.Bruxite);
        GoldTxt.text = "Gold: " + CorrectUiValue(RepairCosts.Gold);
        TitaniumTxt.text = "Titanium: " + CorrectUiValue(RepairCosts.Titanium);
    }

    // Update is called once per frame
    void Update() {

        if (RepairCosts.Gold > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Gold]) { GoldTxt.color = NotEnough; } else { GoldTxt.color = EnoughResources; }
        if (RepairCosts.Energy > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Energy]) { EnergyTxt.color = NotEnough; } else { EnergyTxt.color = EnoughResources; }
        if (RepairCosts.Wood > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Wood]) { WoodTxt.color = NotEnough; } else { WoodTxt.color = EnoughResources; }
        if (RepairCosts.Stone > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone]) { StoneTxt.color = NotEnough; } else { StoneTxt.color = EnoughResources; }
        if (RepairCosts.Iron > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Iron]) { IronTxt.color = NotEnough; } else { IronTxt.color = EnoughResources; }
        if (RepairCosts.Copper > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Copper]) { CopperTxt.color = NotEnough; } else { CopperTxt.color = EnoughResources; }
        if (RepairCosts.Bruxite > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Buxite]) { BruxiteTxt.color = NotEnough; } else { BruxiteTxt.color = EnoughResources; }
        if (RepairCosts.Titanium > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Titanium]) { TitaniumTxt.color = NotEnough; } else { TitaniumTxt.color = EnoughResources; }
          

    }

    public void AttemptToRepairShip() {

        if (script.PlayerResources.CheckIfEnoughResources(RepairCosts.Array)) {


            StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UISuccess));
            script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Wood] -= 10;
            script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone] -= 10;



            Debug.Log("Gen repaired");
            script.UiManagerScripto.IsGeneratorBroken = false;

            
            Broken.SetActive(false);
            Repaired.SetActive(true);

        }
        else {
            StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UIFail));
        }


        Debug.Log("Tried to repair ship!");

    }


    private string CorrectUiValue(float val) {
        float test;
        string newVal = "Error";
        // 1k   000
        // 10k  000
        // 100k  000
        // 1M    000 000

        if (val >= 1000000000) {
            newVal = Mathf.Round(val / 1000000000).ToString() + "B";
        }

        else if (val >= 1000000) {
            newVal = Mathf.Round(val / 1000000).ToString() + "M";   ///.ToString("B") + "M";
        }
        else if (val >= 1000) {
            newVal = Mathf.Round(val / 1000).ToString() + "K";
        }

        else {
            newVal = Mathf.Round(val).ToString();
        }

        return newVal;
    }


}
