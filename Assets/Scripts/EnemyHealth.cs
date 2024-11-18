using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] protected int health;
    [SerializeField] private int maxHealth;
    // Start is called before the first frame update
    void Start()
    {
        health = maxHealth;
    }
 

    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "EDamage")
        {
            takeDamage(1);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    public virtual void takeDamage(int damage)
    {
        health -= damage;
        FindObjectOfType<GameManager>().Stop(0.075f);
        if (damage > 1.1)
        {
            CinemaMachineShake.Instance.ShakeCamera(20, .1f);
        }
        else
        {
            CinemaMachineShake.Instance.ShakeCamera(5, .1f);
        }
    }
    public virtual void Die()
    {
        //Debug.Log("Dies!");
        //Destroy(gameObject);
    }
}
