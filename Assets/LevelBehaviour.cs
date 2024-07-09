using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelBehaviour : MonoBehaviour
{
    public static LevelBehaviour instance;
    float currentPoints;
    public float maxPoints;
    float[] stages = new float[] { 0, 0.2f, 0.4f, 0.6f, 0.8f, 1f };
    int currentStage = 2;
    int currentModifier = 0;

    public void AddPoints(float p)
    {
        currentPoints += p;
        UIBehaviour.instance.moneyPopup(p);

        CheckStage();
        //Debug.Log(currentPoints);
    }

    public void changeMod()
    {
        currentModifier = currentModifier + 1;
        UIBehaviour.instance.setMod(currentModifier.ToString());
        UIBehaviour.instance.setMoney(currentPoints.ToString(), (currentPoints * currentModifier).ToString());
    }

    void CheckStage()
    {
        if (currentPoints < 1)
        {
            GameOver();
            return;
        }
        UIBehaviour.instance.setMoney(currentPoints.ToString(), (currentPoints*currentModifier).ToString());
        int n = 1;
        while (n < stages.Length)
        {
            if (currentPoints > (maxPoints * stages[n - 1]) && currentPoints < (maxPoints * stages[n])) break;
            n++;
        }
        if (currentStage == n)
        {
            UIBehaviour.instance.setNewPBValue(currentPoints / maxPoints);
            return;
        }
        currentStage = n;
        string name = "";
        Color color = Color.red;
        switch (n)
        {
            case 1: { name = "Бомж"; color = Color.red; break; }
            case 2: { name = "Бедняк"; color = Color.magenta; break; }
            case 3: { name = "Обеспеченный"; color = Color.yellow; break; }
            case 4: { name = "Богатый"; color = Color.green; break; }
            case 5: { name = "Миллионер"; color = Color.cyan; break; }
            case 6: { name = "Миллионер"; color = Color.cyan; break; }
            default: name = "Бомж"; color = Color.red; break;
        }
        UIBehaviour.instance.setNewStatus(name, color);
        UIBehaviour.instance.setNewPBValue(currentPoints / maxPoints, color);
        PlayerEvents.instance._Change(name);
        //Debug.Log(name);
    }

    void GameOver()
    {
        UIBehaviour.instance.Signal("GameOver");
        PlayerEvents.instance._GameOver();
    }

    public void Win()
    {
        UIBehaviour.instance.Signal("Win");
        PlayerEvents.instance._Signal("Win");
    }

    private void Start()
    {
        instance = this;
        changeMod();
        AddPoints(maxPoints * stages[1]);
    }
}
