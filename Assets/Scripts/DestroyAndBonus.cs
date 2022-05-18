using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAndBonus : MonoBehaviour {
    public GameObject explosion,explosionPlayer;
    private GameObject cloneExplosion;
    public int ScoreValue;
    private GameController gameController;
    public GameObject[] shot;
    public int chance;
   
    public Transform shotSpawn;


    private void Start() {

        GameObject GameControllerObject = GameObject.FindWithTag("GameController");
        if (GameControllerObject !=null) {
            gameController = GameControllerObject.GetComponent<GameController>();
        }
        
        
      
    }
 
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            cloneExplosion = Instantiate(explosionPlayer,GetComponent<Rigidbody>().position,GetComponent<Rigidbody>().rotation) as GameObject;

            gameController.GameOver();

            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(cloneExplosion,1f);
        }

        if(other.tag == "Bolt" && gameObject.tag =="BonusEnemy")
        {
            cloneExplosion = Instantiate(explosion,GetComponent<Rigidbody>().position,GetComponent<Rigidbody>().rotation);

            if (Random.Range(0,99) < chance){
                GameObject bonus = shot[Random.Range(0,shot.Length)];
                Instantiate(bonus,shotSpawn.position,shotSpawn.rotation);
            }
            
            

            Destroy(other.gameObject);
            Destroy(gameObject);
            Destroy(cloneExplosion,1f);

            gameController.AddScore(ScoreValue);
        }

        
    }
}
