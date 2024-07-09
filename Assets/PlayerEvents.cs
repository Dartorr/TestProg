using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEvents : MonoBehaviour
{
    public static PlayerEvents instance;

    public delegate void Events();
    public delegate void FloatEvents(float f);
    public delegate void StringEvents(string s);
    public event FloatEvents turnEvent;
    public event Events GameOver;
    public event StringEvents Signal;
    public event StringEvents Change;
    private void Awake() { instance = this; }
    public void _GameOver() { GameOver(); }
    public void _turnEvent(float f) { turnEvent(f); }
    public void _Signal(string s) { Signal(s); }
    public void _Change(string s) {  Change(s); }
}
