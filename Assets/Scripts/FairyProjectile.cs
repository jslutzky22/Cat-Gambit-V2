using System.Collections;
using UnityEngine;

public class FairyProjectile : MonoBehaviour
{

    [SerializeField] private float shotDeleteDelay;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(shotDelete());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }

    private IEnumerator shotDelete()
    {
        yield return new WaitForSeconds(shotDeleteDelay);
        Destroy(gameObject);
    }
}
