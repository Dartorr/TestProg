using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class PlayerModelChange : MonoBehaviour
{
    public List<GameObject> gameObjects;
    int current=0;
    Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();    
        PlayerEvents.instance.Change += Instance_Change;
    }

    private void Instance_Change(string s)
    {
        int n = 0;
        switch (s)
        {
            case "Бомж": { n = 0;  break; }
            case "Бедняк": { n = 1; break; }
            case "Обеспеченный": { n = 2; break; }
            case "Богатый": { n = 3; break; }
            case "Миллионер": { n = 4; break; }
            default: break;
        }
        gameObjects[n].SetActive(true);
        gameObjects[current].SetActive(false);
        current = n;
        GetComponent<Animator>().SetTrigger("Jump");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
