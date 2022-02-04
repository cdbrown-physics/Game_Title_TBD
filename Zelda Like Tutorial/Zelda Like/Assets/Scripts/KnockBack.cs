using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    public float knockTime;
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("breakable") && this.gameObject.CompareTag("Player"))
        {
            collision.GetComponent<Pot>().Hit();
        }

        if (collision.gameObject.CompareTag("enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D objectHit = collision.GetComponent<Rigidbody2D>();
            if (objectHit != null)
            {
                Vector2 difference = objectHit.transform.position - transform.position;
                difference = difference.normalized * thrust;
                objectHit.AddForce(difference, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("enemy"))
                {
                    objectHit.GetComponent<Enemy>().enemyState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(objectHit, knockTime);
                }
                if (collision.gameObject.CompareTag("Player"))
                {
                    objectHit.GetComponent<PlayerMovement>().playerState = PlayerState.stagger;
                    collision.GetComponent<PlayerMovement>().Knock(knockTime);
                }
                
            }
        }
    }

}
