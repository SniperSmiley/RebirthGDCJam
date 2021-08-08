using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerDeath : MonoBehaviour
{
    BoxCollider2D playerCollider;
    GameManagerScript gameManager;

    public GameObject gameObject;

    public float energyDeathDecrease = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        playerCollider = GetComponent<BoxCollider2D>();
        gameManager = GetComponent<GameManagerScript>();
    }

    private void Update()
    {
        Die();
    }

    private void Die()
    {
       
        if (playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy"))&& SceneManager.GetActiveScene().buildIndex!=2)
        {
         /*
            if (gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] >= 100)
            {
                gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] -= energyDeathDecrease * gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy];
            }
            else if (gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] < 100)
            {
               gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] = 0;
            }*/

            SceneManager.LoadScene(2);


        }
        else if(playerCollider.IsTouchingLayers(LayerMask.GetMask("Enemy")))
        {
            Debug.Log("Bug collides");
            gameObject.transform.position = new Vector3(0, 0, 0);
            /*
            if (gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] >= 100)
            {
               gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] -= energyDeathDecrease * gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy];
            }
            else if (gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] < 100)
            {
                gameManager.PlayerResources.ResourceArray[(int) Resources.ResourcesIndex.Energy] = 0;
            }*/
        }

        else { return; }
        
    }
}
