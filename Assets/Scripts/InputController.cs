using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class InputController : MonoBehaviour
{
    [SerializeField] Rigidbody cubeRB;
    [SerializeField] BorgerController borger;

    [SerializeField] float forceFlip = 1f;
    [SerializeField] float forceAngleFlip = 1f;
    Vector2 startPos;
    Vector2 endPos;
    float startTime;
    float endTime;

    private void Update()
    {
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
        }
    }
}
