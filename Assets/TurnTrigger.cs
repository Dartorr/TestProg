using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TurnTrigger : MonoBehaviour
{
    public float turnAngle = 90f;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) PlayerEvents.instance._turnEvent(turnAngle);
    }
}
