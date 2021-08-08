using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiManagerScript : MonoBehaviour
{
    public UI CurrentActiveUI;


    public GameObject CarbonGeneratorUI;
    public GameObject PrisonnerManagementUI;
    public GameObject ShipUI;
    public GameObject PlayerUI;

    private GameObject currentlyShowingUI;
    private bool isShowingUi = false;

    public bool IsGeneratorBroken = true;

    public enum UI {
        NOTHING,
        CarbonGenerator,
        PrisonerManagement,
        ShipUi
    };


    public void OnPlayingUI(bool On) {
        if (On) { PlayerUI.SetActive(true); }
        else {
            PlayerUI.SetActive(false);
        }
    }


    public void ShowUI(UI uiToShow) {

        // Disable them first.
        if (isShowingUi) { currentlyShowingUI.SetActive(false); }

        switch (uiToShow) {
            case UI.CarbonGenerator: CarbonGeneratorUI.SetActive(true); currentlyShowingUI = CarbonGeneratorUI;  break;
            case UI.PrisonerManagement: PrisonnerManagementUI.SetActive(true); currentlyShowingUI = PrisonnerManagementUI;  break;
            case UI.ShipUi: ShipUI.SetActive(true); currentlyShowingUI = ShipUI;  break;
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


    public void CloseCurrentMenu() {
         if (isShowingUi) { currentlyShowingUI.SetActive(false); }
         CurrentActiveUI = 0;
    }



}
