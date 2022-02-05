using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    walk,
    attack,
    interact,
    stagger,
    idle
}

public class Player : MonoBehaviour
{
    public PlayerState playerState;
    public float speed = 4.0f;
    private Rigidbody2D myRigidBody;
    private Vector3 change;
    private Animator animator;
    private float attackAnimationTime = 0.30f;
    public IntValue currentHealth;
    public Signal playerHealthSignal;
    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && playerState != PlayerState.attack && playerState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (playerState == PlayerState.walk || playerState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    void UpdateAnimationAndMove()
    {
        change = Vector3.zero;
        float moveInputX = Input.GetAxisRaw("Horizontal");
        float moveInputY = Input.GetAxisRaw("Vertical");
        change.x = moveInputX;
        change.y = moveInputY;

        if (change != Vector3.zero && (playerState == PlayerState.walk || playerState == PlayerState.idle) )
        {
            MovePlayer();
            animator.SetFloat("moveX", moveInputX);
            animator.SetFloat("moveY", moveInputY);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }
    void MovePlayer() 
    {
        change.Normalize();
        transform.Translate( change * speed * Time.deltaTime);
    }

    private IEnumerator AttackCo()
    {
        animator.SetBool("attacking", true);
        playerState = PlayerState.attack;
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(attackAnimationTime);
        playerState = PlayerState.walk;
    }

    public void Knock(float knockTime, int damage)
    {
        currentHealth.initialValue -= damage;
        if (currentHealth.initialValue > 0)
        {
            playerHealthSignal.Raise();
            StartCoroutine(KnockCo(knockTime));
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (myRigidBody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidBody.velocity = Vector2.zero;
            playerState = PlayerState.idle;
        }
    }
}
