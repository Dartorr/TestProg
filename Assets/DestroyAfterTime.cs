using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DestroyAfterTime : MonoBehaviour
{
    public float Time = 1f;
    // Start is called before the first frame update
    public void SetValues(float value, float Time)
    {
        Animator animator = GetComponent<Animator>();
        TextMeshProUGUI textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        Color color=Color.red;
        if (value > 0f)
        {
            animator.SetTrigger("Good");
            color = Color.green;
        }
        else
        {
            animator.SetTrigger("Bad");
            color = Color.red;
        }
        textMeshProUGUI.color = color;
        textMeshProUGUI.text=value.ToString();
        Destroy(gameObject, Time);
    }
}
