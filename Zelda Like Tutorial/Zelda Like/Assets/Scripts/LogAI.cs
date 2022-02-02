using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LogAI : Enemy
{
    private Rigidbody2D myRigidBody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Vector3 homePosition;
    private Animator animator;
    // Start is called before the first frame update

    void Start()
    {
        currentState = EnemyState.idle;
        myRigidBody = GetComponent<Rigidbody2D>();
        target = GameObject.FindWithTag("Player").transform;
        animator = GetComponent<Animator>();
        animator.SetBool("wakeUp", false);
        homePosition = transform.position;
        attackRadius = 1;
        chaseRadius = 5;

    }

    // Update is called once per frame
    void Update()
    {
        CheckDistance();
        if (currentState == EnemyState.chase)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
        else if (currentState == EnemyState.home)
        {
            transform.position = Vector3.MoveTowards(transform.position, homePosition, moveSpeed * Time.deltaTime);
        }
    }

    // Checks Where the LogBoy is, and changes the state machine to the appropriate state
    void CheckDistance()
    {

        if (Vector3.Distance(target.position, transform.position) <= chaseRadius && 
            Vector3.Distance(target.position, transform.position) >  attackRadius)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            currentState = EnemyState.chase;
            animator.SetBool("wakeUp", true);
            if (target.position.x > transform.position.x)
            {

            }
        }
        else if (Vector3.Distance(target.position, transform.position) <= attackRadius)
        {
            currentState = EnemyState.attack;
        }
        else if (transform.position != homePosition) 
        {
            currentState = EnemyState.home;
        }
        else
        {
            animator.SetBool("wakeUp", false);
            currentState = EnemyState.sleep;
        }
    }
}
