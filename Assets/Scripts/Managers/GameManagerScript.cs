using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript GameManager;


    public Resources PlayerResources;
    public UpgradeCostsSO StartResources;


    public CharacterStats PlayerStats = new CharacterStats(100, 1, 1, 1);

    public AudioManager AudioManagerScript;
    public InputManager InputManagerScript;
    public UiManagerScript UiManagerScripto;

    public float CarbonGeneratorEnergy = 2f;

    private void Awake() {

        PlayerResources = new Resources();
        PlayerResources.ResourceArray = StartResources.Array;

        if (GameManager != null) { Destroy(gameObject); }
        else {
            GameManager = this;
            DontDestroyOnLoad(gameObject);
        }

        AudioManagerScript = GetComponentInChildren<AudioManager>();

    }
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
        Debug.Log("REs" + ResourceArray.Length);
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
