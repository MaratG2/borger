using System;
using DG.Tweening;
using UnityEngine;

public class Flipper : MonoBehaviour
{
    public InputController Input;
    public Transform BorgerTransform;
    public LevelController LevelController;

    public event Action TakeOffEvent;
    public event Action PloppedEvent;
    public event Action FlyAwayEvent;

    private void Start()
    {
        LevelController.LevelStartEvent += StartPlop;
        LevelController.LevelWinEvent += EndPlop;
    }

    public void StartPlop()
    {
        BorgerTransform.position = Vector3.up * 10;
        BorgerTransform.DOMoveY(0, .5f).onComplete = () => Plop();
    }

    public void EndPlop()
    {
        TakeOffEvent?.Invoke();
        BorgerTransform.DOMoveY(10, .5f).onComplete = () =>
        {
            FlyAwayEvent?.Invoke();
            RestartLevel();
        };
    }

    public void Flip()
    {
        TakeOffEvent?.Invoke();

        BorgerTransform.DOJump(Vector3.zero, 4, 1, 1);
        BorgerTransform.DOLocalRotate(BorgerTransform.localRotation.eulerAngles + Vector3.right * 180, 1).onComplete =
            () => Plop();
    }

    private void RestartLevel()
    {
        LevelController.StartLevel();
    }

    private void Plop()
    {
        PloppedEvent?.Invoke();
    }
}