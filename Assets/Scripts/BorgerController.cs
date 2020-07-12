using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorgerController : MonoBehaviour
{
    //variables
    [Range(0.1f, 15f)][SerializeField] float frySpeed = 5f;
    float hpDown = 0f;
    float hpUp = 0f;

    bool upSide;
    bool isFrying;

    public Image barDown;
    public Image barUp;

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
                hpUp += Time.deltaTime * frySpeed;
            else
                hpDown += Time.deltaTime * frySpeed;
        }
        if (hpUp > 100f)
            hpUp = 100f;
        if (hpDown > 100f)
            hpDown = 100f;

        barDown.fillAmount = hpDown / 100f;
        barUp.fillAmount = hpUp / 100f;
    }

    public void ChangeSide(bool newSide)
    {
        upSide = newSide;
    }
}
