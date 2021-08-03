using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    public static GameManagerScript GameManager;

    public float Wood = 1;
    public float Stone = 2;
    public float Food = 3f;
    public float Carbon = 4;
    public float Iron = 5;
    public float Copper = 6;
    public float Titanium = 7;
    public float Bruxite = 8;
    public float Gold = 9;
    public float Energy = 10;

 

    public CharacterStats PlayerStats = new CharacterStats( 100, 1, 1, 1 );
        
    public AudioManager AudioManagerScript;

    private void Awake() {
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
