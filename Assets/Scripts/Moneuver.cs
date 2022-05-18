using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Moneuver : MonoBehaviour
{
    public Vector2 startWait,maneuverTime,maneuverWait;
    public float maneuverSpeed,dodge;
    public Boundary boundary;
    public float tilt;
    private float currrentSpeed,targetManeuver;
    public Vector3 leftTop;
    public Vector3 leftBot;
    public Vector3 rightBot;
    public  Vector3 rightTop;
    float xMax, xMin, zMax, zMin;

   private void Start(){
        Camera cam = Camera.main;

        Vector3 cameraToObject = transform.position - cam.transform.position;
        float distance = -Vector3.Project(cameraToObject, Camera.main.transform.forward).y;
        rightBot = cam.ViewportToWorldPoint(new Vector3(1, 0, distance));
        rightTop = cam.ViewportToWorldPoint(new Vector3(1, 1, distance));
        xMax = rightBot.x-0.5f;
        xMin = -rightBot.x+0.5f;
        zMax = rightTop.z+10f;
        zMin = rightBot.z+-1f;

       currrentSpeed =GetComponent<Rigidbody>().velocity.z;
       StartCoroutine (Evade());
       
   }

   IEnumerator Evade(){
       yield return new WaitForSeconds(
           Random.Range(
               startWait.x,
               startWait.y
           )
       );

       while(true){
           targetManeuver = Random.Range(1,dodge) * -Mathf.Sign(transform.position.x);

            yield return new WaitForSeconds(
                Random.Range(
                    maneuverTime.x,
                    maneuverTime.y
                )
           );

           targetManeuver = 0; 

           yield return new WaitForSeconds(
                Random.Range(
                    maneuverWait.x,
                    maneuverWait.y
                )
           );
            
       }
   }

    private void FixedUpdate(){
        float newManeuver = Mathf.MoveTowards
        (
            GetComponent<Rigidbody>().velocity.x,
            targetManeuver,
            maneuverSpeed * Time.deltaTime
        );


        GetComponent<Rigidbody>().velocity = new Vector3(newManeuver,0.0f,currrentSpeed);

        GetComponent<Rigidbody>().position = new Vector3
            (
                Mathf.Clamp(GetComponent<Rigidbody>().position.x,xMin,xMax),
                0.0f,
                Mathf.Clamp(GetComponent<Rigidbody>().position.z,zMin,zMax)
            );

        GetComponent<Rigidbody>().rotation = Quaternion.Euler(
            0f,
            0f,
            GetComponent<Rigidbody>().velocity.x * -tilt);
    }

}
