using UnityEngine;

public class Fairy : MonoBehaviour
{

    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private float bulletSpeed;
    private GameObject closestEnemy;
    [SerializeField] private float distanceToShoot;
    [SerializeField] private AudioClip blastNoise;
   
    void Start()
    {
       
    }




    // Update is called once per frame
    void Update()
    {

    }
    void FindClosestEnemy()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");  
        float closestDistance = Mathf.Infinity;  
        closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy; 
            }
        }

    }
    void shoot()
    {
        FindClosestEnemy();
        if (closestEnemy != null)
        {
            float distance = Vector2.Distance(transform.position, closestEnemy.transform.position);
            if (distance < distanceToShoot)
            {
                GameObject firedBullet = Instantiate(bullet, bulletPos.position, Quaternion.identity);

                
                Vector2 direction = (closestEnemy.transform.position - bulletPos.position).normalized;

             
                firedBullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            }
        }

        AudioSource.PlayClipAtPoint(blastNoise, transform.position);

    }
    void Destroy()
    {
        Destroy(gameObject);
    }
}
