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
    public GameObject Options;
    public GameObject IntroScene;
    public GameObject EndScene;
    public GameObject DeathScreen;

    private GameObject currentlyShowingUI;
    private bool isShowingUi = false;

    public int CarbonGenLevel = 0;

    public bool IsGeneratorBroken = true;


    public enum UI {
        NOTHING,
        CarbonGenerator,
        PrisonerManagement,
        ShipUi,
        Options
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
            case UI.Options:  Options.SetActive(true); currentlyShowingUI = Options; break;
            default:   break;
             
        }

        if (currentlyShowingUI != null) {  CurrentActiveUI = uiToShow; isShowingUi = true; }
        else { CurrentActiveUI = 0; }
    }

    public void OpenCloseOptions() {

        if (currentlyShowingUI != null) {
                currentlyShowingUI.SetActive(false);
              currentlyShowingUI = null;
            isShowingUi = false;

            if (Time.timeScale != 1) { Time.timeScale = 1; }
        }
        else {
            // Open Options
            ShowUI(UI.Options);
            Time.timeScale = 0;
        }
   
        CurrentActiveUI = 0;
    }


    public void CloseCurrentMenu() {
        

         if (currentlyShowingUI != null) {
             if (isShowingUi) { currentlyShowingUI.SetActive(false); }
             CurrentActiveUI = 0;
            currentlyShowingUI = null;
              isShowingUi = false;
         }
         CurrentActiveUI = 0;
    }

    public void QuitToMenu() {
        Time.timeScale = 1;
        EndScene.SetActive(false);
        GameManagerScript.GameManager.SceneManagerScritpto.SwitchScene(0, Vector2.zero);
    }

    public void PrisonScene() { EndScene.SetActive(true); EndScene.GetComponent<EndScene>().DisplayPrison(); }
    public void FreedomeScene() { EndScene.SetActive(true); EndScene.GetComponent<EndScene>().DisplayFreedomeee(); }

    public IEnumerator DisplayDeath() {

        DeathScreen.SetActive(true);

        yield return new WaitForSeconds(2.5f);

        DeathScreen.SetActive(false);
    
    }

}
