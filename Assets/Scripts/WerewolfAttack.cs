using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WerewolfAttack : MonoBehaviour
{
   [SerializeField] private LayerMask enemyLayers;
   [SerializeField] private Transform attackPoint;
   [SerializeField] private float attackRange;
   [SerializeField] private int attackDamage;
    [SerializeField] private bool isDragon;
    [SerializeField] private GameObject fire;
    [SerializeField] private AudioClip attackNoise;
    private bool fireShot;
   


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<EnemyHealth>().takeDamage(attackDamage);
        }
        if (isDragon == true && fireShot == false)
        {
            fireShot = true;
            fire.SetActive(true);
            
        }
        
        AudioSource.PlayClipAtPoint(attackNoise, transform.position);

    }

    void delete()
    {
        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
        {
            return;
        }
        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }

}
