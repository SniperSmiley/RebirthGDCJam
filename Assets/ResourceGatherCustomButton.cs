using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGatherCustomButton : MonoBehaviour {

    
    private float LastClick;
    private Button ButtonGO;
    

    public int ResourceToSet;

    private void Awake() {
        ButtonGO = GetComponent<Button>();
        ButtonGO.onClick.AddListener(OnClick);
    }

    // Start is called before the first frame update
    void Start() {
    

    }

    private void OnEnable() {
      //uttonGO.interactable = true;
    }


    public void OnClick() {

        if (Time.time - LastClick < 0.5f) { return; }

        LastClick = Time.time;

        // Debug.Log("TEst " + Info.name);

        GameManagerScript.GameManager.UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().OnResourceGatherSettingSet((Resources.ResourcesIndex) ResourceToSet, ButtonGO);



    }
}
