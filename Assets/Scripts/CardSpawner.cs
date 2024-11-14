using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardSpawner : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private GameObject spawnSpot;
    [SerializeField] private float cardSpeed;
    [SerializeField] private bool upCard;
    [SerializeField] private bool downCard;
    [SerializeField] private bool rightCard;
    [SerializeField] private bool leftCard;
    [SerializeField] private float cardTime;
    [SerializeField] private GameObject monster;
    private bool enemyHit;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(cardBreak());
        if (rightCard == true)
        {
            rb.velocity = new Vector2(cardSpeed, rb.velocity.y);
        }
        if (leftCard == true)
        {
            rb.velocity = new Vector2(-cardSpeed, rb.velocity.y);
        }
        if (upCard == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, cardSpeed);
        }
        if (downCard == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -cardSpeed);
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*if (rightCard == true)
        {
            rb.velocity = new Vector2(cardSpeed, rb.velocity.y);
        }
        if (leftCard == true)
        {
            rb.velocity = new Vector2(-cardSpeed, rb.velocity.y);
        }
        if (upCard == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, cardSpeed);
        }
        if (downCard == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, -cardSpeed);
        }*/
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "ground")
        {
            enemyHit = true;
            Instantiate(monster, spawnSpot.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private IEnumerator cardBreak()
    {
        
        yield return new WaitForSeconds(cardTime);
        if (enemyHit == false)
        {
            Instantiate(monster, spawnSpot.transform.position, Quaternion.identity);
        }
        //Instantiate(monster, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
