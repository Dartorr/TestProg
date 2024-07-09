using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : AbstractFSM
{
    public int maxX = 1;
    public float speed = 1f;
    public float rotation = 90;
    public float angSpeed = 20f;
    public GameObject child;
    public GameObject model;
    public Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        PlayerEvents.instance.turnEvent += _Turn;
        PlayerEvents.instance.GameOver += GameOver;
        PlayerEvents.instance.Signal += Signal;
        ChangeState(new MoveForward(this));
    }

    private void Signal(string s)
    {
        switch (s)
        {
            case "StateMenu":
                {
                    ChangeState(new Menu(this));
                    break;
                }
            case "StatePlay":
                {
                    ChangeState(new MoveForward(this));
                    break;
                }
            case "Win":
                {
                    ChangeState(new StateWin(this));
                    break;
                }
            default:
                break;
        }
    }

    private void GameOver()
    {
        ChangeState(new gameOver(this));
    }

    public void ChangeChildPosition(float X)
    {
        Vector3 vector3 = child.transform.localPosition;
        vector3.x = X * maxX;
        child.transform.localPosition = vector3;
    }

    public void _Turn(float angle)
    {
        StopCoroutine(turnEnum(angle));
        StartCoroutine(turnEnum(angle));
    }

    IEnumerator turnEnum(float angle)
    {
        float current = 0;
        float start = rotation;
        while (true)
        {
            current = Mathf.Lerp(current, angle * 1.05f, angSpeed * Time.deltaTime);
            rotation = start + current;
            if (Mathf.Abs(current) >= Mathf.Abs(angle)) break;
            yield return new WaitForEndOfFrame();
            animator.SetFloat("Rotation", rotation);
        }
        rotation = start + angle;
        animator.SetFloat("Rotation", rotation);
    }

    public class MoveForward : AState
    {
        Movement This;
        Rigidbody rigidbody;
        public override void Enter()
        {
            This.model.GetComponent<Animator>().SetBool("Walk", true);
            rigidbody = This.gameObject.GetComponent<Rigidbody>();
            This.StartCoroutine(onUpdate());
        }

        IEnumerator onUpdate()
        {
            while (true)
            {
                This.gameObject.GetComponent<Rigidbody>().velocity = This.transform.forward * This.speed;
                yield return new WaitForFixedUpdate();
            }

        }

        public override void Exit()
        {
            This.model.GetComponent<Animator>().SetBool("Walk", false);
            This.StopAllCoroutines();
            This.gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

        public MoveForward(Movement _This)
        {
            This = _This;
        }
    }
    public class Menu : AState
    {
        Movement This;
        public override void Enter()
        { 
            This.model.transform.Rotate(0, 180, 0);
            base.Enter();
        }

        public override void Exit()
        {
            This.model.transform.Rotate(0, 180, 0);
            base.Exit();
        }

        public Menu(Movement @this)
        {
            This = @this;
        }
    }

    public class gameOver : AState
    {
        Movement This;
        public override void Enter()
        {
            This.model.GetComponent<Animator>().SetTrigger("Dead");
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public gameOver(Movement @this)
        {
            This = @this;
        }
    }
    public class StateWin : AState
    {
        Movement This;
        public override void Enter()
        { 
            This.model.transform.Rotate(0, 180, 0);
            base.Enter();
        }

        public override void Exit()
        {
            base.Exit();
        }

        public StateWin(Movement @this)
        {
            //This.model.transform.Rotate(0, 180, 0);
            This = @this;
        }
    }

}
