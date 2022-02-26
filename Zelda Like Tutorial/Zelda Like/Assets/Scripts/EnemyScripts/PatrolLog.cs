using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : LogAI
{
    public Transform[] path;
    public int currentPoint;
    public Transform currentGoal;
    public float roundingDistance;

    public override void CheckDistance()
    {
        // Chase the player if in range
        if (Vector3.Distance(target.position, transform.position) <= chaseRadius
            && Vector3.Distance(target.position, transform.position) > attackRadius
            && (enemyState == EnemyState.walk || enemyState == EnemyState.idle))
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
            Vector3 temp = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);

            changeAnim(temp - transform.position);
            myRigidbody.MovePosition(temp);
            ChangeState(EnemyState.walk);
            
        }
        else if (Vector3.Distance(target.position, transform.position) > chaseRadius)
        {
            animator.SetBool("wakeUp", true);
            // Check when we get close enough to the end point
            if (Vector3.Distance(transform.position, currentGoal.position) > roundingDistance)
            {
                Vector3 temp = Vector3.MoveTowards(transform.position, path[currentPoint].position, moveSpeed * Time.deltaTime);
                changeAnim(temp - transform.position);
                myRigidbody.MovePosition(temp);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if (currentPoint == path.Length -1)
        {
            // Resetting paths
            currentPoint = 0;
            currentGoal = path[0];
        }
        else
        {
            // Set up going to the next point
            currentPoint++;
            currentGoal = path[currentPoint];
        }
    }
}
