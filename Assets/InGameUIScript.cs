using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InGameUIScript : MonoBehaviour
{

    public TextMeshProUGUI WoodText;
    public TextMeshProUGUI StoneText;
    public TextMeshProUGUI FoodText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        OnUpdateUI();
    }

    public void OnUpdateUI() {
        WoodText.text = "Wood: "   +   GameManagerScript.GameManager.Wood;
        StoneText.text = "Stone: " +   GameManagerScript.GameManager.Stone;
        FoodText.text = "Food: "   +   GameManagerScript.GameManager.Food;
    }
}
