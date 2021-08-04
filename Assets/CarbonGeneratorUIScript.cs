using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarbonGeneratorUIScript : MonoBehaviour
{
    private int CurrentUpgrade = 0;

    public UpgradeCostsSO[] UpgradeCosts;

    public GameObject BrokenGen;
    public GameObject NotBrokenGen;

    private GameManagerScript script;

    public Resources Upgrade;

    private Resources upgradeRequirments;

    public TextMeshProUGUI RequirmentsText;
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
            BrokenGen.SetActive(true);    NotBrokenGen.SetActive(false);
        }
        else {
            NotBrokenGen.SetActive(true);    BrokenGen.SetActive(false);
        }
        updateText();

    }

    public void AttemptRepair() {
        if (script.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Wood] >= 10 && script.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Stone] >= 10) {
            Debug.Log("Gen repaired");
            script.UiManagerScripto.IsGeneratorBroken = false;

        }
    }

    public void AttemptUpgrade() {

        if (Time.time - UpgradeLastTime < UpgradeDelay) { return;  }

        UpgradeLastTime = Time.time;

         Debug.Log("Attempt Upgrade");

        if (!GameManagerScript.GameManager.PlayerResources.CheckIfEnoughResources(upgradeRequirments.ResourceArray)) { return;  }


        //if (upgradeRequirments)
        CurrentUpgrade += 1;
        upgradeRequirments.ResourceArray = UpgradeCosts[CurrentUpgrade].Array;
        GameManagerScript.GameManager.PlayerResources.SubtractResource(upgradeRequirments.ResourceArray);
        

        GameManagerScript.GameManager.CarbonGeneratorEnergy *= 2;

        Debug.Log("Upgrade");

    }

   // public void ClearUpgradeResources() { upgradeRequirments.ResetResources(); }

    public void updateText() {
        RequirmentsText.text = "Wood: " + UpgradeCosts[CurrentUpgrade].Wood + " Stone: " + UpgradeCosts[CurrentUpgrade].Stone + " Iron: " + UpgradeCosts[CurrentUpgrade].Iron + " Copper: " + UpgradeCosts[CurrentUpgrade].Copper + " Bruxite: " + UpgradeCosts[CurrentUpgrade].Bruxite + " Titanium: " + UpgradeCosts[CurrentUpgrade].Titanium + " Gold: " + UpgradeCosts[CurrentUpgrade].Gold;
        EnergryText.text = "( " + GameManagerScript.GameManager.CarbonGeneratorEnergy + " Per Second )";
    }
}
