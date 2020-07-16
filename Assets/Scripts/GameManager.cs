using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] Image[] Bar1 = new Image[3];
    [SerializeField] Image[] Bar2 = new Image[3];

    public float greenBar = 0.5f;
    public float upBar = 0f;
    public float botBar = 0.5f;
    public int lvl = 0;

    private void Awake()
    {
        if (FindObjectsOfType<GameManager>().Length > 1)
            Destroy(gameObject);
        else
            DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        Bar1[0] = FindObjectOfType<Bar00>().GetComponent<Image>();
        Bar1[1] = FindObjectOfType<Bar01>().GetComponent<Image>();
        Bar1[2] = FindObjectOfType<Bar02>().GetComponent<Image>();

        Bar2[0] = FindObjectOfType<Bar10>().GetComponent<Image>();
        Bar2[1] = FindObjectOfType<Bar11>().GetComponent<Image>();
        Bar2[2] = FindObjectOfType<Bar12>().GetComponent<Image>();

        greenBar = 0.5f - 0.5f * 0.1f * lvl;
        if (greenBar < 0.1f)
            greenBar = 0.1f;

        if (greenBar < 0.5f)
            botBar = 0.5f + UnityEngine.Random.Range(0f, 0.5f - greenBar);

        upBar = 1 - (botBar + greenBar);
        
        Bar1[1].fillAmount = botBar;
        Bar1[2].fillAmount = upBar;

        Bar2[1].fillAmount = botBar;
        Bar2[2].fillAmount = upBar;

        FindObjectOfType<BorgerController>().maxNeededFry = botBar + greenBar;
        FindObjectOfType<BorgerController>().minNeededFry = botBar;
    }

    public void LoadNextLvl()
    {
        lvl++;
        SceneManager.LoadScene(0);
    }
}
