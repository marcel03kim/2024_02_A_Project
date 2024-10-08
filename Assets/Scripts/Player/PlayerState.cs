using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected PlayerController playerController;

    public PlayerState(PlayerStateMachine stateMachine)
    {
        this.stateMachine = stateMachine;
        this.playerController = stateMachine.playerController;
    }

    //���� �޼���� : ���� Ŭ�������� �ʿ信 ���� �������̵�
    public virtual void Enter() { }
    public virtual void Exit() { }
    public virtual void Update() { }
    public virtual void FixedUpdate() { }

    protected void CheckTransitions()
    {
        if(playerController.isGrounded())
        {
            if(Input.GetKeyDown(KeyCode.Space))
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                stateMachine.TransitionToState(new MovingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new IdleState(stateMachine));
            }
        }
        else
        {
            if(playerController.GetVerticalVelocity() > 0)
            {
                stateMachine.TransitionToState(new JumpingState(stateMachine));
            }
            else
            {
                stateMachine.TransitionToState(new FallingState(stateMachine));
            }
        }
    }
}

//IdleState : �÷��̾ ������ �ִ� ����

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update() 
    {
        CheckTransitions();
    }

}

//MovingState : �÷��̾ �̵� ���� ����

public class MovingState : PlayerState
{
    public MovingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();
    }

    public override void FixedUpdate()
    {
        playerController.HandleMovement();
    }
}

//JumpingState : �÷��̾ ���� ���� ��
public class JumpingState : PlayerState
{
    public JumpingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();
    }

    public override void FixedUpdate()
    {
        playerController.HandleJump();
    }
}

//FallingState : �÷��̾ ������ ��
public class FallingState : PlayerState
{
    public FallingState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update()
    {
        CheckTransitions();
    }

    public override void FixedUpdate()
    {
        playerController.isFalling();
    }
}