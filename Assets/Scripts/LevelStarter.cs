using UnityEngine;
using UnityEngine.EventSystems;

public class LevelStarter : MonoBehaviour, IPointerClickHandler
{
    public LevelController Controller;
    public GameObject GreetPanel;

    public void OnPointerClick(PointerEventData eventData)
    {
        this.enabled = false;
        GreetPanel.SetActive(false);
        Controller.StartLevel();
    }
}