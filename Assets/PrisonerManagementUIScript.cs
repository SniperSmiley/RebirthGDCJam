using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class PrisonerManagementUIScript : MonoBehaviour
{
    public GameObject MugShots;
    public GameObject RapSheet;

    public TextMeshProUGUI MugDescriptionText;
    public TextMeshProUGUI MugCrimes;
    public TextMeshProUGUI Nametxt;
    public Image MugImageDisplay;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MugClicked(CriminalInfoSO info) {
        MugShots.SetActive(false);

        MugDescriptionText.text = info.Description;

        string crimes = "";
        foreach(string txt in info.Crimes) {
            crimes += txt + " \n";
        }

        MugCrimes.text = crimes;
        MugImageDisplay.sprite = info.img;
        Nametxt.text =  "name: " + info.name;

        RapSheet.SetActive(true);
    }

    public void BackTOMugs() {
        RapSheet.SetActive(false);
        MugShots.SetActive(true);
    }

        
}
