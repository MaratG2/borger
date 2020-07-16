using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorgerController : MonoBehaviour
{
    //variables
    [Tooltip("0 - normal, -1 - lost, 1 - won")][SerializeField] int gameCondition = 0; 
    [Range(0.1f, 15f)][SerializeField] float frySpeed = 5f;

    [Range(0f, 1f)] [SerializeField] float minNeededFry = 0.7f;
    [Range(0f, 1f)] [SerializeField] float maxNeededFry = 0.85f;

    float hpDown = 0f;
    float hpUp = 0f;
    float maxHP = 100f;

    [HideInInspector] public bool isFrying;
    bool upSide; 

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
        if (isFrying && gameCondition == 0)
        {
            if (upSide)
                hpUp += Time.deltaTime * frySpeed;
            else
                hpDown += Time.deltaTime * frySpeed;
        }

        if (hpUp > maxHP)
            hpUp = maxHP;
        if (hpDown > maxHP)
            hpDown = maxHP;

        barDown.fillAmount = hpDown / maxHP;
        barUp.fillAmount = hpUp / maxHP;

        if (hpUp / maxHP > maxNeededFry || hpDown / maxHP > maxNeededFry)
            gameCondition = -1;
        
    }

    public void ChangeSide(bool newSide)
    {
        upSide = newSide;

        if (hpUp / maxHP >= minNeededFry && hpUp / maxHP <= maxNeededFry && hpDown / maxHP >= minNeededFry && hpDown / maxHP <= maxNeededFry)
        {
            gameCondition = 1;
        }
    }
}
