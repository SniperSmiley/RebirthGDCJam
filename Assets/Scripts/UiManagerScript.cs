using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManagerScript : MonoBehaviour
{
    public UI CurrentActiveUI;


    public GameObject CarbonGeneratorUI;

    private GameObject currentlyShowingUI;
    private bool isShowingUi = false;

    public bool IsGeneratorBroken = true;

    public enum UI {
        NOTHING,
        CarbonGenerator
    };


    public void ShowUI(UI uiToShow) {

        // Disable them first.
        if (isShowingUi) { currentlyShowingUI.SetActive(false); }

        switch (uiToShow) {
            case UI.CarbonGenerator: CarbonGeneratorUI.SetActive(true); currentlyShowingUI = CarbonGeneratorUI;  break;
            default:   break;
             
        }

        if (currentlyShowingUI != null) {  CurrentActiveUI = uiToShow; }
        else { CurrentActiveUI = 0; }
    }

    public void CloseUI() {
        if (currentlyShowingUI != null) {
                currentlyShowingUI.SetActive(false);
              currentlyShowingUI = null;
        }
   
        CurrentActiveUI = 0;
    }





    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
