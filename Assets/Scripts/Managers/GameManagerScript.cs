using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript GameManager;

    public List<Prisoner> Prisoners; // new List<Prisoner>();
    public PrisonerSpawnerScript PrisSpawner;

    private bool discoveredIron = false;
    private bool discoveredCopper = false;
    private bool discoveredBruxite = false;
    private bool discoveredTitanium = false;
    private bool discoveredGold = false;


    public List<PrisonerAction> PrisonerActions = new List<PrisonerAction>();

    public Resources PlayerResources;
    public UpgradeCostsSO StartResources;


    public CharacterStats PlayerStats = new CharacterStats(100, 1, 1, 1);

    public AudioManager AudioManagerScript;
    public InputManager InputManagerScript;
    public UiManagerScript UiManagerScripto;
    public SceneManagerScript SceneManagerScritpto;
    public ResourceChangeDisplayScript resourceChangeDisplayScripto = null;

    public float CarbonGeneratorEnergy = 2f;
    private float LastTime = 0;

    private void Awake() {

        Prisoners = new List<Prisoner>();


        PlayerResources = new Resources();
        PlayerResources.ResourceArray = StartResources.Array;

        if (GameManager != null) { Destroy(gameObject); }
        else {
            GameManager = this;
            DontDestroyOnLoad(gameObject);
        }

        AudioManagerScript = GetComponentInChildren<AudioManager>();

    }

    private void Update() {
        // Update the generators production
        if (UiManagerScripto.IsGeneratorBroken) { return; }

        if (Time.time - LastTime > 1) {
            if (PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Carbon] >= 1) {
                PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Carbon] -= 1;
                PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Energy] += CarbonGeneratorEnergy;
                LastTime = Time.time;
            }

            for (int i = 0; i < PrisonerActions.Count; i++) {
                if (Time.time - PrisonerActions[i]._lastUpdated > PrisonerActions[i].Delay) {
                    Debug.Log("Prisoner " + PrisonerActions[i].PrisIndex);
                    PlayerResources.ResourceArray[(int)PrisonerActions[i].resource] += PrisonerActions[i].Change;
                    PrisonerActions[i]._lastUpdated = Time.time;
                }
            }



        }

        // Check if new resource gathered.
        if (!discoveredIron) { if (PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Iron] > 0) { discoveredIron = true; UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().MakeButtonActive(Resources.ResourcesIndex.Iron); } }
        if (!discoveredCopper) { if (PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Copper] > 0) { discoveredCopper = true; UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().MakeButtonActive(Resources.ResourcesIndex.Copper); } }
        if (!discoveredBruxite) { if (PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Buxite] > 0) { discoveredBruxite = true; UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().MakeButtonActive(Resources.ResourcesIndex.Buxite); } }
        if (!discoveredTitanium) { if (PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Titanium] > 0) { discoveredTitanium = true; UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().MakeButtonActive(Resources.ResourcesIndex.Titanium); } }
        if (!discoveredGold) { if (PlayerResources.ResourceArray[(int)Resources.ResourcesIndex.Gold] > 0) { discoveredGold = true; UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().MakeButtonActive(Resources.ResourcesIndex.Gold); } }


    }

    
    // Simplifies adding resources to inventory.
    public void AddResourceToInventory(Resources.ResourcesIndex resource, float quantity) {
        GameManager.PlayerResources.ResourceArray[(int)resource] += quantity;
    }


}


public class PrisonerAction {

    public float _lastUpdated = 0;
    public float Delay = 2;
    public Resources.ResourcesIndex resource = Resources.ResourcesIndex.Wood;
    public float Change = 0f;
    public int PrisIndex = 0;
    public float BaseChange = 5;
}


public class CharacterStats {

    public CharacterStats(float _health, float _resource, float _attackdmg, float _attackSpeed) {
        Heath = _health;
        ResourceGatheringLevel = _resource;
        AttackDmg = _attackdmg;
        AttackSpeed = _attackSpeed;
    }

    public float Heath;
    public float ResourceGatheringLevel;
    public float AttackDmg;
    public float AttackSpeed;
}

public class Resources {

    public Resources() {
        ResourceArray = new float[10];
        //Debug.Log("REs" + ResourceArray.Length);
    }

    public enum ResourcesIndex {
        Wood,
        Stone,
        Food,
        Carbon,
        Iron,
        Copper,
        Titanium,
        Buxite,
        Gold,
        Energy

    };

    public float[] ResourceArray = new float[10];

    /*
    public float Wood = 1;
    public float Stone = 2;
    public float Food = 3f;
    public float Carbon = 4;
    public float Iron = 5;
    public float Copper = 6;
    public float Titanium = 7;
    public float Bruxite = 8;
    public float Gold = 9;
    public float Energy = 10;*/

    public void ResetResources() {
        for (int i = 0; i < ResourceArray.Length; i++) { ResourceArray[i] = 0; }
    }

    public bool CheckIfEnoughResources(float[] resRequirements) {

        bool AllGood = true;
        for (int i = 0; i < ResourceArray.Length; i++) { if (ResourceArray[i] < resRequirements[i]) { Debug.Log("AAA"); AllGood = false; } }

        return AllGood;

    }

    public void SubtractResource(float[] resource) {
        for (int i = 0; i < ResourceArray.Length; i++) {
            ResourceArray[i] -= resource[i];
        }
    }

}


