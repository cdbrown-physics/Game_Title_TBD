using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float thrust;
    // Start is called before the first frame update


    public void OnTriggerEnter2D(Collider2D collition)
    {
        if (collition.gameObject.CompareTag("enemy"))
        {
            Rigidbody2D enemy = collition.GetComponent<Rigidbody2D>();
            if (enemy != null)
            {
                Debug.Log("Hitting the log boy");
                enemy.isKinematic = false;
                Vector2 difference = enemy.transform.position - transform.position;
                difference = difference.normalized * thrust;
                Debug.Log(difference);
                enemy.AddForce(difference, ForceMode2D.Impulse);
                enemy.isKinematic = true;
            }
        }
    }

}
