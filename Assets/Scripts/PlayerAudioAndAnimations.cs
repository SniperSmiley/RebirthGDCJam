using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioAndAnimations : MonoBehaviour {

    public Animator Animtor;

    public AudioClip HitTree;
    public AudioClip HitTree1;
    public AudioClip HitTree2;

    public AudioClip HitRock;
    public AudioClip GrabFruit;
    public AudioClip GrabLeaf;

    private AudioManager audioScript;

    private void Start() {
        audioScript = GameManagerScript.GameManager.AudioManagerScript;
    }






    public void PlayCorrectAudio(IsInteractable interact) {

        if (interact.transform.tag == "Tree") {

             Animtor.SetTrigger("IsMiningWood");
            switch (Random.Range(0, 3)) {
                case 0: StartCoroutine(audioScript.PlayEffect(HitTree)); break;
                case 1: StartCoroutine(audioScript.PlayEffect(HitTree2)); break;
                case 2: StartCoroutine(audioScript.PlayEffect(HitTree1)); break;
            }


        }
        else if (interact.transform.tag == "Rock") {         Animtor.SetTrigger("IsMiningStone"); StartCoroutine(audioScript.PlayEffect(HitRock)); }
        else if (interact.transform.tag == "Fruit") { StartCoroutine(audioScript.PlayEffect(GrabFruit)); }
        else if (interact.transform.tag == "Leaf") { StartCoroutine(audioScript.PlayEffect(GrabLeaf)); }
        else if (interact.transform.tag == "Enemy") { Animtor.SetTrigger("Sword"); StartCoroutine(audioScript.PlayEffect(GrabLeaf)); }



    }
}
