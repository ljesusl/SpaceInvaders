using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollBackground : MonoBehaviour
{

    public float scrollSpeed, tileSize;
    private Transform currrentObject;

    // Start is called before the first frame update
    void Start()
    {
        currrentObject = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {  
        currrentObject.position = new Vector3(
            currrentObject.position.x,
            currrentObject.position.y,
            -Mathf.Repeat(Time.time*scrollSpeed,tileSize)
        );
        
    }
}
