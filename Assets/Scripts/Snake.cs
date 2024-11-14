using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Snake : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float Speed;
    [SerializeField] private AudioClip snakeNoise;
    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log("SnakeSpawned");
        rb.velocity = new Vector2(-Speed, rb.velocity.y);
        StartCoroutine(destroyDelay());
        AudioSource.PlayClipAtPoint(snakeNoise, transform.position);
    }
    private IEnumerator destroyDelay()
    {
        yield return new WaitForSeconds(10f);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
