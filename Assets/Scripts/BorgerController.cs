using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorgerController : MonoBehaviour
{
    //variables
    float hpDown = 0f;
    float hpUp = 0f;

    bool upSide;
    bool isFrying;

    private void OnCollisionEnter(Collision collision)
    {
        isFrying = true;
    }
    private void OnCollisionStay(Collision collision)
    {
        isFrying = true;
    }
    private void OnCollisionExit(Collision collision)
    {
        isFrying = false;
    }

    private void Update()
    {
        if(isFrying)
        {
            if (upSide)
                hpUp += Time.deltaTime;
            else
                hpDown += Time.deltaTime;
        }
    }

    public void ChangeSide(bool newSide)
    {
        upSide = newSide;
    }
}
