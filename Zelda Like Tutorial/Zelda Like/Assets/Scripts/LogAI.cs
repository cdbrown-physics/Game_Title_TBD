using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogAI : Enemy
{
    private Rigidbody2D myRigidbody;
    public Transform target;
    public float chaseRadius;
    public float attackRadius;
    public Transform homePosition;
    public Animator animator;

    // Use this for initialization
    void Start()
    {
        enemyState = EnemyState.idle;
        myRigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        target = GameObject.FindWithTag("Player").transform;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius
            && (enemyState == EnemyState.walk || enemyState == EnemyState.idle) )
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
        }
    }

    private void ChangeState(EnemyState newState)
    {
        if (newState != enemyState)
        {
            enemyState = newState;
        }
    }
}