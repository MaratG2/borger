using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorgerSideTrigger : MonoBehaviour
{
    [SerializeField] bool upSide = false;

    private void OnTriggerEnter(Collider other)
    {
        GetComponentInParent<BorgerController>().ChangeSide(upSide);
    }
}
