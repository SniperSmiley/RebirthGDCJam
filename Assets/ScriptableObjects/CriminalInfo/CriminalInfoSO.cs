using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "CriminalInfo", menuName = "ScriptableObjects/CriminalInfoSO", order = 1)]
public class CriminalInfoSO : ScriptableObject
{
    public int ID;
    public string Name;
    public string Description;
    public string[] Crimes;
    public Sprite img;
}
