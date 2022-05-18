using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary {
    public float xMin,xMax,zMin,zMax;

}

public class PlayerController : MonoBehaviour
{

    public float speed = 10;
    public float tilt;
    public Boundary boundary;

    public GameObject[] shot;
    public Transform shotSpawn;

    public float fireRate = 0.5f;
    public float nextFire = 0.0f;

    public TP touchPad;
    public AreaButton areaButton;
    private int weapon = 0;
    public Vector3 leftTop;
    public Vector3 leftBot;
    public Vector3 rightBot;
    public  Vector3 rightTop;
    public GameObject sphere;
    

    private void Start(){
        Camera cam = Camera.main;

        Vector3 cameraToObject = transform.position - cam.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;
        rightBot = cam.ViewportToWorldPoint(new Vector3(1, 0, distance));
        rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
        boundary.xMax = rightBot.x-0.5f;
        boundary.xMin = -rightBot.x+0.5f;
        boundary.zMax = rightTop.z-3f;
        boundary.zMin = rightBot.z+1f;
        // sphere.transform.localScale = new Vector3(-rightBot.x*2, 0, rightTop.z+10);
        // sphere.transform.position = new Vector3(0, 0, (rightTop.z+10f)/3.5f);


    }


    public void Update()
    {
        if(areaButton.CanFire() && Time.time >nextFire)
        {
            nextFire = Time.time + fireRate;
            Instantiate(shot[weapon], shotSpawn.position, shotSpawn.rotation);
            GetComponent<AudioSource>().Play();
        }

       
    }

    private void FixedUpdate()
    {
        Vector3 direction = touchPad.GetDirection();

        // float moveHorizontal = Input.GetAxis("Horizontal");
        // float moveVertical = Input.GetAxis("Vertical");

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(
            0f,
            0f,
            GetComponent<Rigidbody>().velocity.x * -tilt);

        GetComponent<Rigidbody>().velocity = new Vector3(direction.x, 0f, direction.y) * speed;


        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x,boundary.xMin,boundary.xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z,boundary.zMin,boundary.zMax)
            //     Mathf.Clamp(GetComponent<Rigidbody>().position.x,rightBot.x+0.5,-rightBot.x-0.5),
            //     0.0f,
            //     Mathf.Clamp(GetComponent<Rigidbody>().position.z,rightTop.z-0.5,rightBot.z+0.5)
            );
    }

    public void UpdateWeapon(int Weapon){
        weapon = Weapon;
    }

    public void Boundary(float xMax,float xMin, float zMax, float zMin ){
        boundary.xMax = xMax;
        boundary.xMin = xMin;
        boundary.zMax = zMax;
        boundary.zMin = zMin;

    }


}
