using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f;

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {

            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

            if (rb != null)
            {
                rb.velocity = new Vector2(rb.velocity.x, 0); 
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }

    }
}
