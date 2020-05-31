using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityStateController : MonoBehaviour
{
    [SerializeField]
    private bool isInit;
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool isIdle;

    public void SetIsInit(bool isInit)
    {
        this.isInit = isInit;
    }
    public bool IsInit()
    {
        return isInit;
    }

    public void SetIsGrounded(bool isGrounded)
    {
        this.isGrounded = isGrounded;
    }
    public bool IsGrounded()
    {
        return isGrounded;
    }

    public void SetIsIdle(bool isIdle)
    {
        this.isIdle = isIdle;
    }
    public bool IsIdle()
    {
        return isIdle;
    }
}
