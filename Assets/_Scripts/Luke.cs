using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Luke : Singleton<Luke>
{
    [SerializeField] private Animator animator;

    public void Open()
    {
        animator.SetTrigger("Open");
        //animator.SetBool("IsOpen", true);
    }

    public void Close()
    {
        animator.SetBool("IsOpen", false);
    }
}
