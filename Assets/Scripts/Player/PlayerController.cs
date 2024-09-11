using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Jump,
    Attack,
    Aim,
    Crouch,
    JumpAttack,
    OnShield,
    Dodge,
}

public class PlayerController : MonoBehaviour
{
    #region Speed
    public float crouchSpeed = 2f;
    public float aimSpeed = 3f;
    public float walkSpeed = 5f;
    public float runSpeed = 10f;
    public float rotSpeed = 10f;
    public float jumpPower = 5f;
    public float dashPower = 5f;
    #endregion

    [HideInInspector]
    public Rigidbody rb;
    public PlayerState state;
    PlayerAnimationHandler animationrHandler;
    State currentState;

    public LayerMask groundLayer;
    List<State> states = new();

    void Awake()
    {
        animationrHandler = GetComponent<PlayerAnimationHandler>();
        rb = GetComponent<Rigidbody>();
    }
    void Start()
    {
        states.Add(new Idle(this, animationrHandler));
        states.Add(new Walk(this, animationrHandler));
        states.Add(new Run(this, animationrHandler));
        states.Add(new Jump(this, animationrHandler));
        states.Add(new Attack(this, animationrHandler));
        states.Add(new Aim(this, animationrHandler));
        states.Add(new Crouch(this, animationrHandler));
        states.Add(new JumpAttack(this, animationrHandler));
        states.Add(new OnShield(this, animationrHandler));
        states.Add(new Dodge(this, animationrHandler));
        SetStateIdle();
    }

    void Update()
    {
        currentState?.Update();
    }
    void OnGUI()
    {
        GUIStyle guiStyle = new GUIStyle();
        guiStyle.fontSize = 16;
        guiStyle.normal.textColor = Color.green;

        string statusText = "Player State: " + state.ToString();
        GUI.Label(new Rect(10, 10, 300, 20), statusText, guiStyle);
    }
    public void ChangeState(PlayerState newState)
    {
        currentState?.Exit();
        currentState = states[(int)newState];
        currentState.Enter();

        state = newState;
    }
    public void SetStateIdle()
    {
        ChangeState(PlayerState.Idle);
    }
}
