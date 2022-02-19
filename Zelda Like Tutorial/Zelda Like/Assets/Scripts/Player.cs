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
    public VectorValue startingPosition;
    public Inventory playerInventory;
    public SpriteRenderer recievedItemSprite;
    // Start is called before the first frame update
    void Start()
    {
        playerState = PlayerState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerState == PlayerState.interact)
        {
            return;
        }
        if (Input.GetButton("Fire1") && playerState != PlayerState.attack && playerState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if (playerState == PlayerState.walk || playerState == PlayerState.idle)
        {
            UpdateAnimationAndMove();
        }
    }

    public void RaiseItem()
    {
        if (playerInventory.currentItem != null)
        {
            if (playerState != PlayerState.interact)
            {
                animator.SetBool("recieveItem", true);
                playerState = PlayerState.interact;
                recievedItemSprite.sprite = playerInventory.currentItem.itemSprite;
            }
            else
            {
                animator.SetBool("recieveItem", false);
                playerState = PlayerState.idle;
                recievedItemSprite.sprite = null;
                playerInventory.currentItem = null;
            }
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
        if (playerState != PlayerState.interact)
        {
            playerState = PlayerState.walk;
        }
    }

    public void Knock(float knockTime, int damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if (currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
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
