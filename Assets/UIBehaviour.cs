using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIBehaviour : AbstractFSM
{
    public static UIBehaviour instance;
    public delegate void PBDelegate(float v);
    public delegate void PBColorDelegate(float v, Color newclr);
    public event PBDelegate PBEvent;
    public event PBColorDelegate PBColorEvent;
    public TextMeshProUGUI status;
    public TextMeshProUGUI money;
    public TextMeshProUGUI moneyWin;
    public TextMeshProUGUI moneyResult;
    public TextMeshProUGUI modifierPlay;
    public TextMeshProUGUI modifierWin;
    public GameObject moneyPopupGO;

    public GameObject Play;
    public GameObject Menu;
    public GameObject GameOver;
    public GameObject Win;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        ChangeState(new StateMenu());
    }


    public void setNewStatus(string text, Color color)
    {
        status.text = text;
        status.color = color;
    }
    public void setMoney(string m, string result)
    {
        money.text = m;
        moneyWin.text = m;
        moneyResult.text = result;
    }

    public void setMod(string m)
    {
        modifierPlay.text = m;
        modifierWin.text = m;
    }
    public void setNewPBValue(float value) => PBEvent(value);
    public void setNewPBValue(float value, Color newclr) => PBColorEvent(value, newclr);
    public void moneyPopup(float value)
    {
        var a = Instantiate(moneyPopupGO, transform);
        a.GetComponent<DestroyAfterTime>().SetValues(value, 2f);
    }

    public void changeMod(string text)
    {
        modifierPlay.text = text;
        modifierWin.text = text;
    }

    public class StateMenu : AState
    {
        public override void Enter()
        {
            PlayerEvents.instance._Signal("StateMenu");
            instance.Menu.SetActive(true);
        }

        public override void Exit() => instance.Menu.SetActive(false);
    }

    public class StatePlay : AState
    {
        public override void Enter()
        {
            PlayerEvents.instance._Signal("StatePlay");
            instance.Play.SetActive(true);
        }
        public override void Exit() => instance.Play.SetActive(false);
    }

    public class StateGameOver : AState
    {
        public override void Enter()
        {
            PlayerEvents.instance._Signal("StateGameOver");
            instance.GameOver.SetActive(true);
        }
        public override void Exit() => instance.GameOver.SetActive(false);
    }
    public class StateWin : AState
    {
        public override void Enter()
        {
            PlayerEvents.instance._Signal("StateWin");
            instance.Win.SetActive(true);
        }
        public override void Exit() => instance.Win.SetActive(false);
    }

    public void Signal(string signal)
    {
        switch (signal)
        {
            case "Play":
                {
                    ChangeState(new StatePlay());
                    break;
                }
            case "Menu":
                {
                    ChangeState(new StateMenu());
                    break;
                }
            case "GameOver":
                {
                    ChangeState(new StateGameOver());
                    break;
                }
            case "Win":
                {
                    ChangeState(new StateWin());
                    break;
                }
            case "Reload":
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                }
            case "LoadNext":
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                    break;
                }
            default:
                break;
        }
    }
}
