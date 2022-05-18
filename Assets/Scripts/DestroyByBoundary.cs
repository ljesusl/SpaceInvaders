using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByBoundary : MonoBehaviour
{

    public GameController gameController;

    private PlayerController playerController;
    GameObject PlayerControllerObject;
    public GameObject explosionPlayer;
    private GameObject cloneExplosion;
    private Vector3 vv;



    private void Start() {
        PlayerControllerObject = GameObject.FindWithTag("Player");
    }

    


    private void OnTriggerExit(Collider other)
    {

        if(other.tag == "Enemy" || other.tag == "BonusEnemy")
        {
            gameController.GameOver();
            if(PlayerControllerObject != null){
                cloneExplosion = Instantiate(explosionPlayer,PlayerControllerObject.transform.position,PlayerControllerObject.transform.rotation) as GameObject;
                Destroy(PlayerControllerObject);
                Destroy(cloneExplosion,1f); 
            }
            
        }
        // Destroy(other.gameObject);
        
    }
}
