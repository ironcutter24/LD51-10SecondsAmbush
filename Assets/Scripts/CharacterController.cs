using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    [SerializeField] Animator anim;
    [SerializeField] float moveSpeed = 20f;

    Vector2 moveDirection = Vector2.zero;
    Vector2 lookDirection = Vector2.right;


    public static CharacterController Instance;

    Rigidbody2D rb;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        moveDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        if (!Mathf.Approximately(moveDirection.x, 0f))
        {
            lookDirection = moveDirection.normalized;
            FlipOnX(anim.gameObject.transform, lookDirection.x < 0f);
        }

        anim.SetFloat("moveSpeed", moveDirection.magnitude);
    }

    void FixedUpdate()
    {
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
}
