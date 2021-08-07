using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Prisoner {
    public GameObject MugImageGO;
    // public int Level = 1;
    public CriminalInfoSO Data;
    public int Level = 1;
    public int ResourceGainRate = 100;
    public int ResourceGainPercentage = 0;
    public bool Awake = false;
    public Button resourceButtonSelected = null;
}


public class PrisonerManagementUIScript : MonoBehaviour {

    public Button IronButton;
    public Button CopperButton;
    public Button BruxiteButton;
    public Button Titaniumbutton;
    public Button GoldButton;


    public GameObject PowerOn;
    public GameObject PowerOff;

    public GameObject Buttons;

    public Color AwkenedColour;

    private float CurrentCostToAwake = 100;

    public GameObject MugShots;
    public GameObject RapSheet;

    public TextMeshProUGUI MugDescriptionText;
    public TextMeshProUGUI MugCrimes;
    public TextMeshProUGUI Nametxt;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI ResourceGainText;
    public TextMeshProUGUI FoodRequiredForLevelUp;
    public TextMeshProUGUI CostToAwake;

    public Image MugImageDisplay;

    public GameObject[] Mugs;
    public CriminalInfoSO[] Crims;

    public List<Prisoner> Prisoners;

    public GameObject Awake;
    public GameObject Asleep;

    private Prisoner CurrentPrisoner;
    private int CurrentPrisonerIndex = 0;


    // Start is called before the first frame update
    void Start() {
        Prisoners = new List<Prisoner>();

        for (int i = 0; i < Crims.Length; i++) {
            Prisoner pris = new Prisoner();
            pris.Data = Crims[i];
            pris.MugImageGO = Mugs[i];
            Prisoners.Add(pris);
        }

        PowerOn.SetActive(false);
        PowerOff.SetActive(true);
        MugShots.SetActive(false);
        RapSheet.SetActive(false);
        Asleep.SetActive(false);
        Awake.SetActive(false);


        Debug.Log(Prisoners.Count);

        /*
        foreach (GameObject mug in Mugs) {
            Prisoner pris = new Prisoner();
           // pris.Data = mug.GetComponent<UiButtonScript>().Info;
            pris.MugImageGO = mug;
        }*/
    }

    // Update is called once per frame
    void Update() {

        if (PowerOff.activeSelf) {
            if (!GameManagerScript.GameManager.UiManagerScripto.IsGeneratorBroken) {
                PowerOff.SetActive(false);
                PowerOn.SetActive(true);
                MugShots.SetActive(true);
            }
        }

    }

    private void resetButtons() {
       
        foreach (Button but in Buttons.GetComponentsInChildren<Button>()) {
            if (but.colors.disabledColor != Color.black) { continue;  }
            
            but.interactable = true;
        }
    }

    public void MugClicked(int id) {


        if (Prisoners[id] == null) { Debug.LogError("HMMM"); }

        CurrentPrisoner = Prisoners[id];
        CurrentPrisonerIndex = id;

        if (CurrentPrisoner.Awake) { Asleep.SetActive(false); Awake.SetActive(true); }
        else { Asleep.SetActive(true); Awake.SetActive(false); }

        StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(GameManagerScript.GameManager.AudioManagerScript.UISuccess));

        MugShots.SetActive(false);

        UpdateCrimText();

        resetButtons();

        // Update option is awake
        if (Prisoners[CurrentPrisonerIndex].Awake) {
            // Set old button on if active?
            if (Prisoners[CurrentPrisonerIndex].resourceButtonSelected != null) {
                Prisoners[CurrentPrisonerIndex].resourceButtonSelected.interactable = false;
            }
        }


        RapSheet.SetActive(true);
    }

    public void BackTOMugs() {
        StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(GameManagerScript.GameManager.AudioManagerScript.UISuccess));
        RapSheet.SetActive(false);
        MugShots.SetActive(true);
    }

    public void AttemptLevelUpCharacter() {

        float FoodRequired = (CurrentPrisoner.Level + 1) * 10;

        if (GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Food] > FoodRequired) {

            StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(GameManagerScript.GameManager.AudioManagerScript.UISuccess));
            Prisoners[CurrentPrisonerIndex].Level += 1;

            if (Prisoners[CurrentPrisonerIndex].ResourceGainPercentage == 0) { Prisoners[CurrentPrisonerIndex].ResourceGainPercentage = 50; }
            else {
                Prisoners[CurrentPrisonerIndex].ResourceGainPercentage = (2 * Prisoners[CurrentPrisonerIndex].ResourceGainPercentage);
            }

            Debug.Log("TEST: " + Prisoners[CurrentPrisonerIndex].ResourceGainPercentage);
            Prisoners[CurrentPrisonerIndex].ResourceGainRate += Prisoners[CurrentPrisonerIndex].ResourceGainPercentage;

            GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Food] -= FoodRequired;

            // Actually increase change
            for (int i = 0; i < GameManagerScript.GameManager.PrisonerActions.Count; i++) {
                if (GameManagerScript.GameManager.PrisonerActions[i].PrisIndex == CurrentPrisonerIndex) {
                    GameManagerScript.GameManager.PrisonerActions[i].Change = Prisoners[CurrentPrisonerIndex].ResourceGainRate * GameManagerScript.GameManager.PrisonerActions[i].BaseChange / 100f; //  GameManagerScript.GameManager.PrisonerActions[i].BaseChange * (Prisoners[CurrentPrisonerIndex].ResourceGainPercentage / 100f);
                    break;
                }

            }

            UpdateCrimText();
        }

        else {
            StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(GameManagerScript.GameManager.AudioManagerScript.UIFail));
        }

    }

    public void UpdateCrimText() {

        try {
            CostToAwake.text = "Requires: " + CurrentCostToAwake + " Energy to awaken";
            MugDescriptionText.text = Prisoners[CurrentPrisonerIndex].Data.Description;

            string crimes = "";
            foreach (string txt in Prisoners[CurrentPrisonerIndex].Data.Crimes) {
                crimes += txt + " \n";
            }

            LevelText.text = "Current Level: " + Prisoners[CurrentPrisonerIndex].Level;
            ResourceGainText.text = "Resource Gain: " + Prisoners[CurrentPrisonerIndex].ResourceGainRate + "%";
            MugCrimes.text = crimes;
            MugImageDisplay.sprite = Prisoners[CurrentPrisonerIndex].Data.img;
            Nametxt.text = "name: " + CurrentPrisoner.Data.Name;
            FoodRequiredForLevelUp.text = "Food To LVL Up: " + (Prisoners[CurrentPrisonerIndex].Level + 1) * 10;
        }
        catch (Exception e) {
            Debug.Log("ERROR: " + e.Message);
            GameManagerScript.GameManager.UiManagerScripto.CloseCurrentMenu();
            throw;
        }



    }

    public void AttemptToWakeUp() {

        if (Prisoners[CurrentPrisonerIndex].Awake) { return; }

        if (GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Energy] >= CurrentCostToAwake) {
            StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(GameManagerScript.GameManager.AudioManagerScript.UISuccess));
            OnWakeUp();
        }
        else {
            StartCoroutine(GameManagerScript.GameManager.AudioManagerScript.PlayEffect(GameManagerScript.GameManager.AudioManagerScript.UIFail));
        }
    }


    public void OnWakeUp() {

        Debug.Log("WAKE UP!! ");
        Prisoners[CurrentPrisonerIndex].Awake = true;
        GameManagerScript.GameManager.PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Energy] -= CurrentCostToAwake;
        Asleep.SetActive(false); Awake.SetActive(true);
        Prisoners[CurrentPrisonerIndex].MugImageGO.GetComponent<Image>().color = AwkenedColour;
        CurrentCostToAwake *= 2.5f;

        // Setting up action
        PrisonerAction newAction = new PrisonerAction();
        newAction.Change = 4;
        newAction.BaseChange = 4;
        newAction.Delay = 5;
        newAction.PrisIndex = CurrentPrisonerIndex;
        newAction.resource = Resources.ResourcesIndex.Wood;

        GameManagerScript.GameManager.PrisonerActions.Add(newAction);
    }

    public void OnResourceGatherSettingSet(Resources.ResourcesIndex res, Button but) {
        for (int i = 0; i < GameManagerScript.GameManager.PrisonerActions.Count; i++) {
            if (GameManagerScript.GameManager.PrisonerActions[i].PrisIndex == CurrentPrisonerIndex) {
                GameManagerScript.GameManager.PrisonerActions[i].resource = res;

                // Set old button on if active?
                if (Prisoners[CurrentPrisonerIndex].resourceButtonSelected != but && Prisoners[CurrentPrisonerIndex].resourceButtonSelected != null) {
                    Prisoners[CurrentPrisonerIndex].resourceButtonSelected.interactable = true;
                }

                Prisoners[CurrentPrisonerIndex].resourceButtonSelected = but;
                but.interactable = false;
                //but.enabled = false;

                //but.gameObject.SetActive(false);



                break;
            }
        }
    }


    public void MakeButtonActive(Resources.ResourcesIndex res) {

        var newColorBlock = IronButton.colors;
        newColorBlock.disabledColor = Color.black;

        switch (res) {
            case Resources.ResourcesIndex.Iron: IronButton.colors = newColorBlock; IronButton.interactable = true; break;
            case Resources.ResourcesIndex.Copper: CopperButton.colors = newColorBlock; CopperButton.interactable = true; break;
            case Resources.ResourcesIndex.Buxite: BruxiteButton.colors = newColorBlock; BruxiteButton.interactable = true; break;
            case Resources.ResourcesIndex.Titanium: Titaniumbutton.colors = newColorBlock; Titaniumbutton.interactable = true; break;
            case Resources.ResourcesIndex.Gold: GoldButton.colors = newColorBlock; GoldButton.interactable = true; break;

        }
    }


}
