using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UpgradeCosts", menuName = "ScriptableObjects/UpgradeCostsSO", order = 1)]
public class UpgradeCostsSO : ScriptableObject
{
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

   
    public float[] Array;

    private void OnValidate() {
            Array = new float[] {
            Wood,
            Stone,
            Food,
            Carbon,
            Iron,
            Copper,
            Titanium,
            Bruxite,
            Gold,
            Energy
        }; 
    }


}
