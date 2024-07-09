using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{
    public float val = 0.2f;
    Transform child;
    Image img;
    public void SetValue(float v)
    {
        child.transform.localScale=new Vector3(v,1,1);
    }

    public void SetValue(float v, Color newColor)
    { 
        child.transform.localScale = new Vector3(v, 1, 1);
        img.color = newColor;
    }
    // Start is called before the first frame update
    void Start()
    {
        // rectTransform = GetComponentInChildren<RectTransform>();
        UIBehaviour.instance.PBEvent += SetValue;
        UIBehaviour.instance.PBColorEvent += SetValue;
        child=transform.GetChild(0);
        img = child.gameObject.GetComponent<Image>();
        SetValue(val, Color.red);
    }
}
