using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateController : MonoBehaviour
{
    private bool isInit;
    public void SetIsInit(bool isInit) => this.isInit = isInit;
    public bool IsInit() => isInit;

    private bool isGrounded;
    public void SetIsGrounded(bool isGrounded) => this.isGrounded = isGrounded;
    public bool IsGrounded() => isGrounded;

    private bool isIdle;
    public void SetIsIdle(bool isIdle) => this.isIdle = isIdle;
    public bool IsIdle()=> isIdle;

    private bool isGrabing;
    public void SetIsGrabing(bool isGrabing) => this.isGrabing = isGrabing;
    public bool IsGrabing() => isGrabing;

    private bool isPushing;
    public void SetIsPushing(bool isPushing)
    {
        this.isPushing = isPushing;
        this.canMove = (isPushing) ? false : true;
    }
    public bool IsPushing() => isPushing;

    private bool isInteracting;
    public void SetIsInteracting(bool isInteracting)
    {
        this.isInteracting = isInteracting;
        //Need need to deactivate canMove
    }
    public bool IsInteracting() => isInteracting;

    private bool canMove = true;
    public bool CanMove() => canMove;
}
