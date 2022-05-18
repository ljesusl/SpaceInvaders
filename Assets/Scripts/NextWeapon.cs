using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextWeapon : MonoBehaviour {
    // public GameObject explosion,explosionPlayer;
    private GameObject cloneExplosion;
    public int ScoreValue;
    private GameController gameController;
    private PlayerController playerController;


    private void Start() {

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject !=null) {
            gameController = GameControllerObject.GetComponent<GameController>();
        }

        GameObject PlayerControllerObject = GameObject.FindWithTag("Player");
        if (PlayerControllerObject !=null) {
            playerController = PlayerControllerObject.GetComponent<PlayerController>();
        }
      
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if(gameObject.tag == "Powerup1" && other.tag == "Player")
        {
            playerController.UpdateWeapon(1);
            Destroy(gameObject);


        }
        if(gameObject.tag == "Powerup2" && other.tag == "Player")
        {
            playerController.UpdateWeapon(2);
            
            Destroy(gameObject);


        }



        
    }
}
