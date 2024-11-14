using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoneProjectile : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float force;
    [SerializeField] private float shotDeleteDelay;
    [SerializeField] private bool isSorrowCard;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shotDelete());
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * force;

        float rot = Mathf.Atan2(-direction.y, -direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, rot); //Eventually add + degrees needed to the rot once have sprites
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
       
        if (collision.gameObject.tag == "ground" && isSorrowCard == false)
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator shotDelete()
    {
        yield return new WaitForSeconds(shotDeleteDelay);
        Destroy(gameObject);
    }

}
