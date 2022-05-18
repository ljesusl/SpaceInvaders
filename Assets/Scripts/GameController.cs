using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameController : MonoBehaviour 
{
    public GameObject hazard,hazardBonus;
    public Vector3 spawnValues;
    public int hazardCount,ScoreValue;
    public float spawnWait,waveWait;
    // public float startWait;

    public Text scoreText,levelText;
    private int score,scoreLvl,level;
    private float fixedUpdateCount = 0;

    public Text gameOverText;

    public GameObject restartButton,startGameButton;

    private bool isBusy;


    private bool gameover, restart;
    public GameObject sphere;
    float xMax, xMin, zMax, zMin;
    public Vector3 leftTop;
    public Vector3 leftBot;
    public Vector3 rightBot;
    public Vector3 rightTop;
    Vector3 spawnPosition;

    private void Start(){
        Camera cam = Camera.main;

        Vector3 cameraToObject = transform.position - cam.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;
        rightBot = cam.ViewportToWorldPoint(new Vector3(1, 0, distance));
        rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
        xMax = rightBot.x-0.5f;
        xMin = -rightBot.x+0.5f;
        zMax = rightTop.z-3f;
        zMin = rightBot.z+1f;
        sphere.transform.localScale = new Vector3(-rightBot.x*2, 0, rightTop.z+10);
        sphere.transform.position = new Vector3(0, 0, (rightTop.z+10f)/3.5f);


        gameover = false;
        restart = false;
        restartButton.SetActive(false); 
        // startGameButton.SetActive(true);

        gameOverText.text = "";
        isBusy = false;

        level = 1;
        score = 0;
        UpdateScore();
    }
    
    
    public void Update(){
        if(gameover){
                restartButton.SetActive(true);
                // restartText.text = "Press 'R' to restart";
                restart = true;
                
            }       
        
    }

    void FixedUpdate () 
    { 
        if (score == scoreLvl && isBusy)
        {
            isBusy = false;
            level += 1;
            UpdateLevel();
            StartCoroutine(SpawnWaves());
        }
 
    }

    IEnumerator SpawnWaves()
    {
        // yield return new WaitForSeconds(startWait);

        yield return new WaitForSeconds(waveWait);
        if(gameover){
            restartButton.SetActive(true);
            restart = true;
        }
        for(int i = 0;i < hazardCount-1;i++)
        {
            spawnPosition = new Vector3(Random.Range(xMin,xMax),0,zMax+Random.Range(0,10));
            Quaternion spawnRotation = Quaternion.identity;

            Instantiate(hazard,spawnPosition,spawnRotation); 

            // yield return new WaitForSeconds(Random.Range(0.3f, spawnWait));
        }
        // Vector3 spawnPosition1 = new Vector3(Random.Range(-spawnValues.x,spawnValues.x),spawnValues.y,spawnValues.z+Random.Range(-10,0));
        Vector3 spawnPosition1 = new Vector3(Random.Range(xMin,xMax),0,zMax+Random.Range(1,9));
        Quaternion spawnRotation1 = Quaternion.identity;
        Instantiate(hazardBonus,spawnPosition1,spawnRotation1); 


        scoreLvl += ScoreValue * hazardCount;
        hazardCount += 5;
        StopCoroutine(SpawnWaves());

    }

    void UpdateLevel(){
        levelText.text = "Level: " + level;
    }

    void UpdateScore(){
        scoreText.text = "Score: " + score;
    }

    public void AddScore(int newScoreValue){
        isBusy = true;
        score +=newScoreValue;
        UpdateScore();
    }

    public void GameOver(){
        gameOverText.text = "Game Over!";
        gameover = true;
    }

    public void RestartGame(){
        SceneManager.LoadScene("main",LoadSceneMode.Single); 
       
        // StartCoroutine(SpawnWaves());
        // Start();
        // StartCoroutine(SpawnWaves());
    }
    public void StartGame(){
        StartCoroutine(SpawnWaves());
        startGameButton.SetActive(false);
    }

    
  
}
