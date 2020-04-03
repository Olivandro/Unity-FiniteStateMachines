using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController_FSM : MonoBehaviour
{
    #region Player Variables

    public float jumpForce;
    public Transform head;
    public Transform weapon01;
    public Transform weapon02;

    public Sprite idleSprite;
    public Sprite duckingSprite;
    public Sprite jumpingSprite;
    public Sprite spinningSprite;

    private SpriteRenderer face;
    private Rigidbody rbody;

    public Rigidbody Rigidbody
    {
        get { return rbody; }
    }

    #endregion

    #region State Variables

    private PlayerBaseState currentState;
    public PlayerBaseState CurrentState
    {
        get { return currentState; }
    }

    public readonly PlayerIdleState IdleState = new PlayerIdleState();
    public readonly PlayerJumpingState JumpingState = new PlayerJumpingState();
    public readonly PlayerDuckingState DuckingState = new PlayerDuckingState();
    // public readonly PlayerSpinningState SpinningState = new PlayerSpinningState();

    #endregion

    #region Private Methods
        
    private void Awake()
    {
        face = GetComponentInChildren<SpriteRenderer>();
        rbody = GetComponent<Rigidbody>();
        SetExpression(idleSprite);
    }

    private void Start() 
    {
        TransitionToState(IdleState);
    }

    // Update is called once per frame
    private void Update()
    {
        currentState.Update(this);
    }

    private void OnCollisionEnter(Collision other) 
    {
        currentState.OnCollisionEnter(this);
    }

    #endregion
    

    public void TransitionToState(PlayerBaseState state)
    {
        currentState = state;
        currentState.EnterState(this);
    }

    public void SetExpression(Sprite newExpression)
    {
        face.sprite = newExpression;
    }
}
