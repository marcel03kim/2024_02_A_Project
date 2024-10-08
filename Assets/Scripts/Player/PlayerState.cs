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

    //가상 메서드들 : 하위 클래스에서 필요에 따라 오버라이드
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

//IdleState : 플레이어가 정지해 있는 상태

public class IdleState : PlayerState
{
    public IdleState(PlayerStateMachine stateMachine) : base(stateMachine) { }

    public override void Update() 
    {
        CheckTransitions();
    }

}

//MovingState : 플레이어가 이동 중인 상태

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

//JumpingState : 플레이어가 점프 중일 때
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

//FallingState : 플레이어가 떨어질 때
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