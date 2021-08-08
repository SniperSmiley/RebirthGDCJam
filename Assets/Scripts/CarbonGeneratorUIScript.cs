using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarbonGeneratorUIScript : MonoBehaviour {

    public Color EnoughResources;
    public Color NotEnough;

    private int CurrentUpgrade = 0;

    public UpgradeCostsSO[] UpgradeCosts;

    public GameObject BrokenGen;
    public GameObject NotBrokenGen;

    private GameManagerScript script;

    public Resources Upgrade;

    private Resources upgradeRequirments;

    public TextMeshProUGUI WoodTxt;
    public TextMeshProUGUI StoneTxt;
    public TextMeshProUGUI IronTxt;
    public TextMeshProUGUI CopperTxt;
    public TextMeshProUGUI BruxiteTxt;
    public TextMeshProUGUI GoldTxt;
    public TextMeshProUGUI TitaniumTxt;

    public TextMeshProUGUI EnergryText;

    private float UpgradeDelay = .2f;
    private float UpgradeLastTime = 0;

    private void Awake() {

        script = GameManagerScript.GameManager;

        upgradeRequirments = new Resources();
        upgradeRequirments.ResourceArray = UpgradeCosts[CurrentUpgrade].Array;

    }

    private void Update() {
        if (script.UiManagerScripto.IsGeneratorBroken) {
            BrokenGen.SetActive(true); NotBrokenGen.SetActive(false);
        }
        else {
            NotBrokenGen.SetActive(true); BrokenGen.SetActive(false);
        }
        updateText();

    }

    public void AttemptRepair() {
        if (script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Wood] >= 10 && script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone] >= 10) {


            StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UISuccess));
            script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Wood] -= 10;
            script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone] -= 10;

          

            Debug.Log("Gen repaired");
                    script.UiManagerScripto.CarbonGenLevel++;
            script.UiManagerScripto.IsGeneratorBroken = false;

        }
        else {
              StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UIFail));
        }
    }

    public void AttemptUpgrade() {

        if (Time.time - UpgradeLastTime < UpgradeDelay) { StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UIFail));  return; }

        UpgradeLastTime = Time.time;

        Debug.Log("Attempt Upgrade");

        if (!GameManagerScript.GameManager.PlayerResources.CheckIfEnoughResources(upgradeRequirments.ResourceArray)) {  StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UIFail)); return; }


        StartCoroutine(script.AudioManagerScript.PlayEffect(script.AudioManagerScript.UISuccess));

        script.UiManagerScripto.CarbonGenLevel++;

        //if (upgradeRequirments)
        GameManagerScript.GameManager.PlayerResources.SubtractResource(upgradeRequirments.ResourceArray);

        CurrentUpgrade += 1;
        upgradeRequirments.ResourceArray = UpgradeCosts[CurrentUpgrade].Array;



        GameManagerScript.GameManager.CarbonGeneratorEnergy *= 2;

        Debug.Log("Upgrade");

    }

    // public void ClearUpgradeResources() { upgradeRequirments.ResetResources(); }

    public void updateText() {
        // RequirmentsText.text = "Wood: " + UpgradeCosts[CurrentUpgrade].Wood + " Stone: " + UpgradeCosts[CurrentUpgrade].Stone + " Iron: " + UpgradeCosts[CurrentUpgrade].Iron + " Copper: " + UpgradeCosts[CurrentUpgrade].Copper + " Bruxite: " + UpgradeCosts[CurrentUpgrade].Bruxite + " Titanium: " + UpgradeCosts[CurrentUpgrade].Titanium + " Gold: " + UpgradeCosts[CurrentUpgrade].Gold;
        EnergryText.text = "" + GameManagerScript.GameManager.CarbonGeneratorEnergy + " Per Second";

        WoodTxt.text = "Wood: " + UpgradeCosts[CurrentUpgrade].Wood;
        StoneTxt.text = "Stone: " + UpgradeCosts[CurrentUpgrade].Stone;
        CopperTxt.text = "Copper: " + UpgradeCosts[CurrentUpgrade].Copper;
        TitaniumTxt.text = "Titanium: " + UpgradeCosts[CurrentUpgrade].Titanium;
        BruxiteTxt.text = "Bruxite: " + UpgradeCosts[CurrentUpgrade].Bruxite;
        GoldTxt.text = "Gold: " + UpgradeCosts[CurrentUpgrade].Gold;
        IronTxt.text = "Iron: " + UpgradeCosts[CurrentUpgrade].Iron;

        // Colours

        if (UpgradeCosts[CurrentUpgrade].Gold > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Gold]) { GoldTxt.color = NotEnough; } else { GoldTxt.color = EnoughResources; }
        if (UpgradeCosts[CurrentUpgrade].Wood > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Wood]) { WoodTxt.color = NotEnough; } else { WoodTxt.color = EnoughResources; }
        if (UpgradeCosts[CurrentUpgrade].Stone > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Stone]) { StoneTxt.color = NotEnough; } else { StoneTxt.color = EnoughResources; }
        if (UpgradeCosts[CurrentUpgrade].Copper > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Copper]) { CopperTxt.color = NotEnough; } else { CopperTxt.color = EnoughResources; }
        if (UpgradeCosts[CurrentUpgrade].Iron > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Iron]) { IronTxt.color = NotEnough; } else { IronTxt.color = EnoughResources; }
        if (UpgradeCosts[CurrentUpgrade].Titanium > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Titanium]) { TitaniumTxt.color = NotEnough; } else { TitaniumTxt.color = EnoughResources; }
        if (UpgradeCosts[CurrentUpgrade].Bruxite > script.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Buxite]) { BruxiteTxt.color = NotEnough; } else { BruxiteTxt.color = EnoughResources; }
      
        
    }
}
