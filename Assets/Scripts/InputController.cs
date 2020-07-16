using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] Rigidbody cubeRB;
    [SerializeField] BorgerController borger;

    [SerializeField] float forceFlip = 0.0001f; //were 0.0001 with an old input
    [SerializeField] float forceAngleFlip = 1f; //were 1 with an old input

    float timeToFlip = 0.9f;
    float timerToFlip = 0f;
    bool ableToFlip = true;

    /* //Old Input variables
    Vector2 startPos;
    Vector2 endPos;
    float startTime;
    float endTime;
    */

    public void Start()
    {
        borger = FindObjectOfType<BorgerController>();
        cubeRB = borger.gameObject.GetComponent<Rigidbody>();  
    }

    private void Update()
    {
        //LEGACY INPUT
        /* 
        if (Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0); //Get first touch, doesn't matter how many there are
            if (t.phase == TouchPhase.Began)
            {
                startPos = t.position;
                startTime = Time.time;
            }
            if(t.phase == TouchPhase.Ended)
            {
                endPos = t.position;
                endTime = Time.time;

                if (endTime - startTime < 1f && borger.isFrying)
                {
                    cubeRB.AddForce(0f, forceFlip * (endPos.y - startPos.y) * (endPos - startPos).magnitude * (1 - (endTime - startTime)), 0f);
                    cubeRB.AddTorque(forceAngleFlip * (endPos.y - startPos.y), 0f, 0f);
                    startTime = 0f;
                    endTime = 0f;
                }
            }
        }*/

        if (timerToFlip < timeToFlip)
            timerToFlip += Time.deltaTime;
        else
            ableToFlip = true;

        if(Input.touches.Length > 0)
        {
            Touch t = Input.GetTouch(0);

            if(t.phase == TouchPhase.Began && borger.isFrying && ableToFlip)
            {
                cubeRB.AddForce(0f, forceFlip, 0f);
                cubeRB.AddTorque(forceAngleFlip, 0f, 0f);
                timerToFlip = 0f;
                ableToFlip = false;
            }
        }
    }
}
