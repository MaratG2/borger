using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BorgerController : MonoBehaviour
{
    //variables
    [Tooltip("0 - normal, -1 - lost, 1 - won")][SerializeField] int gameCondition = 0; 
    float frySpeed = 5f;

    [Range(0f, 1f)] public float minNeededFry = 0.7f;
    [Range(0f, 1f)] public float maxNeededFry = 0.85f;

    float hpDown = 0f;
    float hpUp = 0f;
    float maxHP = 100f;

    [HideInInspector] public bool isFrying;
    bool upSide;
    bool inWinCR;

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

    private void Start()
    {
        FindObjectOfType<GameManager>().Start();
        FindObjectOfType<InputController>().Start();
        frySpeed = UnityEngine.Random.Range(7, 16);
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
            if (!inWinCR)
                StartCoroutine(WinCR());
        }
    }

    private IEnumerator WinCR()
    {
        inWinCR = true;
        yield return new WaitForSecondsRealtime(0.7f);
        FindObjectOfType<GameManager>().LoadNextLvl();
    }
}
