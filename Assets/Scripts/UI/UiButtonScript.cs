using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiButtonScript : MonoBehaviour
{
    private Button ButtonGO;
    // public CriminalInfoSO Info;

    public int Id;

    private float LastClick;

    private void Start() {
         ButtonGO = GetComponent<Button>();
         ButtonGO.onClick.AddListener(OnClick);
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    public void OnClick() {

        if (Time.time - LastClick < 0.5f) { return;  }

        LastClick = Time.time;

       // Debug.Log("TEst " + Info.name);

        GameManagerScript.GameManager.UiManagerScripto.PrisonnerManagementUI.GetComponent<PrisonerManagementUIScript>().MugClicked(Id);



    }
}
