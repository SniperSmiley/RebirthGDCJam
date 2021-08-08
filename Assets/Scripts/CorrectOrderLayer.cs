using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CorrectOrderLayer : MonoBehaviour
{

    public Transform SortingPos;
    public float BaseSortLayer = 500f;
    public SpriteRenderer rend;

    private void FixedUpdate() {
        UpdateLayerOrder();
    }

    private void UpdateLayerOrder() {
        // Times 10 to make it have more layes of accuracy. Instead of per grid space.
        rend.sortingOrder = Mathf.RoundToInt((BaseSortLayer - SortingPos.position.y) * 10);
    }

}
