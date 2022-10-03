using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utility.Patterns;

public class CharacterController : Singleton<CharacterController>
{
    [SerializeField] Animator anim;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] Collider2D physicCol;
    [SerializeField] PlayerHUD playerHUD;
    [SerializeField] float moveSpeed = 20f;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.right;

    bool lockControls = false;

    void Update()
    {
        if (lockControls || isAnimated) return;

        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!Mathf.Approximately(moveDirection.x, 0f))
        {
            SetLookDirection(moveDirection);
        }

        anim.SetFloat("moveSpeed", moveDirection.magnitude);
    }

    void FixedUpdate()
    {
        if (lockControls || isAnimated) return;

        rb.MovePosition(rb.position + moveDirection.normalized * moveSpeed * Time.deltaTime);
    }

    void FlipOnX(Transform trs, bool state)
    {
        float xAbs = Mathf.Abs(trs.localScale.x);
        trs.localScale = new Vector3(
            state ? -xAbs : xAbs,
            trs.transform.localScale.y,
            trs.transform.localScale.z
            );
    }

    public void SetDeath(bool state)
    {
        SetLockControls(state);
        physicCol.enabled = !state;
        anim.SetBool("isDead", state);

        if (!state)
        {
            playerHUD.RestoreHearts();
            playerHUD.RestoreMatValues();
        }
    }

    public void SetLockControls(bool state)
    {
        lockControls = state;
    }

    #region Cutscenes animation

    bool isAnimated = false;

    public void GoTo(Vector3 target, Action callback)
    {
        isAnimated = true;
        Vector2 move = target - transform.position;
        SetLookDirection(move);
        anim.SetFloat("moveSpeed", moveSpeed);

        transform.DOMove(target, move.magnitude / moveSpeed)
            .SetEase(Ease.Linear)
            .OnComplete(() =>
            {
                anim.SetFloat("moveSpeed", 0f);
                isAnimated = false;
                callback();
            });
    }

    #endregion

    void SetLookDirection(Vector2 dir)
    {
        lookDirection = dir.normalized;
        FlipOnX(anim.gameObject.transform, lookDirection.x < 0f);
    }
}
