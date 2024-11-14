using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private int startingPoint;
    [SerializeField] private Transform[] points;

    private int i;
    private Rigidbody2D rb;

    void Start()
    {
        transform.position = points[startingPoint].position;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Vector2.Distance(rb.position, points[i].position) < 0.02f)
        {
            i++;
            if (i == points.Length)
            {
                i = 0;
            }
        }
        transform.position = Vector2.MoveTowards(rb.position, points[i].position, speed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform != null && GameManager.Instance != null)
        {
           
            GameManager.Instance.UnparentWithDelay(collision.transform);
        }
    }

    private IEnumerator UnparentAfterDelay(Transform objectToUnparent)
    {
        yield return new WaitForEndOfFrame(); 
        objectToUnparent.SetParent(null);
    }

}

