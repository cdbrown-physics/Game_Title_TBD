using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    home,
    sleep,
    stagger
}

public class Enemy : MonoBehaviour
{
    public EnemyState enemyState;
    public IntValue maxHealth;
    public int health;
    public string enemyName;
    public int baseAttack;
    public float moveSpeed;
    // Start is called before the first frame update

    private void Awake()
    {
        health = maxHealth.initialValue;
    }

    private void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, int damage)
    {
        TakeDamage(damage);
        StartCoroutine(KnockCo(myRigidbody, knockTime));
    }
    private IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if (myRigidbody != null )
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            enemyState = EnemyState.idle;
        }
    }

}


