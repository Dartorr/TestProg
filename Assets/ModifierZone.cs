using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierZone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) LevelBehaviour.instance.changeMod();
    }
}
