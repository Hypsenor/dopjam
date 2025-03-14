using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class effectorChange : MonoBehaviour
{
    private PlatformEffector2D effector;
    
    void Start()
    {
       
        effector = GetComponent<PlatformEffector2D>();
    }

    // Update is called once per frame
    void Update()
    {
        

        if(Input.GetKey(KeyCode.S))
            effector.rotationalOffset = 180f;
        
           
        if (Input.GetKey(KeyCode.Space))
            effector.rotationalOffset = 0f;
            

    }
}
