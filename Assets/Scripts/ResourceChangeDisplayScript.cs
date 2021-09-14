using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextProperties {

    public GameObject TextObject;
    public float TimeCreated;
    public bool Active = false;

    private TextMeshProUGUI actualText;

    public void Establish(Transform parent, GameObject textTemplate) {
        TextObject = GameObject.Instantiate(textTemplate);
        TextObject.transform.position = Vector2.zero;
        TextObject.SetActive(false);
        TextObject.transform.SetParent(parent);

        actualText = TextObject.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void EnableText(Vector2 pos, string text) {

        Debug.Log("Enabling text");

        TextObject.transform.position = pos;
        actualText.text = text; // resource.ToString() + " + " + amount;

        TextObject.SetActive(true);
        TimeCreated = Time.time;
        Active = true;
    }

    public bool OutOfTime(float Duration) {
        if (Time.time - TimeCreated >= Duration) { return true; }
        return false;
    }

    public void DisableText() {
        TextObject.transform.position = Vector2.zero; TimeCreated = 0;
        TextObject.SetActive(false);
        Active = false;
    }
}


public class ResourceChangeDisplayScript : MonoBehaviour {

    public Transform Parent;
    public GameObject TextTemplate;
    public float Duration = 3;

    public int StartTextPoolSize = 5;

    // All Text objects created
    private List<TextProperties> TextPool = new List<TextProperties>();
    private int NumActive = 0;
    private int ChangeInActive = 0;

    private void Awake() {

        GameManagerScript.GameManager.resourceChangeDisplayScripto = this;
        for (int i = 0; i < StartTextPoolSize; i++) { GenerateTextObject(); }
    }

    void GenerateTextObject() {
        TextProperties newText = new TextProperties();
        newText.Establish(Parent, TextTemplate);
        TextPool.Add(newText);
    }

    // Update is called once per frame
    void Update() {

        // Check through every text currently active 
        if (NumActive == 0) { return;  }

        // Iterate over every active text, check if there time is up.
        for (int i = 0; i < TextPool.Count; i++) {
            if (!TextPool[i].Active) { continue; }
            if (TextPool[i].OutOfTime(Duration)) { TextPool[i].DisableText(); ChangeInActive++; }
        }
       
        NumActive -= ChangeInActive; ChangeInActive = 0;
    }

    public void DisplayChange(string text, Vector2 pos) {

        // Loop through all the available text
        bool available = false; TextProperties TextToChange = null;

        for (int i = 0; i < TextPool.Count; i++) {
            if (!TextPool[i].Active) { TextToChange = TextPool[i]; available = true; break; }
        }
        
        // Ensure there is an available text object
        if (!available) {  GenerateTextObject(); TextToChange = TextPool[TextPool.Count - 1];  Debug.Log("OHNO"); }

        TextToChange.EnableText(pos, text);  //resource.ToString() + " + " + amount);

        NumActive++;
    }
}
