using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public enum PlayerState
{
    Idle,
    Walk,
    Run,
    Attack,
    Aim,
    Crouch,
    JumpAttack,
    OnShield,
    Dodge,
    Air,
    AirAim,
    DodgeAttack,
    KnockBack,
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
    [HideInInspector]
    public Vector3 hitDir;
    public PlayerState state;

    PlayerAnimationHandler animationrHandler;
    State currentState;
    List<State> states = new();

    [SerializeField] bool onGUI;
    public LayerMask groundLayer;
    public bool isKnockBack;

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
        states.Add(new Attack(this, animationrHandler));
        states.Add(new Aim(this, animationrHandler));
        states.Add(new Crouch(this, animationrHandler));
        states.Add(new JumpAttack(this, animationrHandler));
        states.Add(new OnShield(this, animationrHandler));
        states.Add(new Dodge(this, animationrHandler));
        states.Add(new Air(this, animationrHandler));
        states.Add(new AirAim(this, animationrHandler));
        states.Add(new DodgeAttack(this, animationrHandler));
        states.Add(new KnockBack(this, animationrHandler));
        SetStateIdle();
    }
    void Update()
    {
        currentState?.Update();
    }
    void OnGUI()
    {
        if (onGUI)
        {
            GUIStyle guiStyle = new GUIStyle();
            guiStyle.fontSize = 16;
            guiStyle.normal.textColor = Color.green;

            string statusText = "Player State: " + state.ToString();
            GUI.Label(new Rect(10, 10, 300, 20), statusText, guiStyle);
        }
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
    public void PlayerJump()
    {
        animationrHandler.SetTrigger("Jump");
        Vector3 jumpForce = Vector3.up * jumpPower;
        rb.AddForce(jumpForce, ForceMode.VelocityChange);

        ChangeState(PlayerState.Air);
    }
    public void TargetingBool()
    {
        animationrHandler.SetBool("Targeting", !animationrHandler.GetBool("Targeting"));
    }
    public void SetStateAir()
    {
        ChangeState(PlayerState.Air);
    }
    public void Hit(Vector3 dir, int damage)
    {
        if (!isKnockBack)
        {
            hitDir = dir;
            GetComponent<IDamageable>().TakeDamage(damage);
            ChangeState(PlayerState.KnockBack);
            var hp = GetComponent<PlayerStats>().HP;
            UIManager.instance.UpdateHearts(hp);
            if (hp <= 0)
            {
                UIManager.instance.UpdateHearts(0);
                Die();
            }
        }
    }
    public void Die()
    {
        animationrHandler.SetTrigger("Die");
        Destroy(this);
    }
}
