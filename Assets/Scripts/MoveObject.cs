using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour
{
    [SerializeField] private bool horizontal;
    [SerializeField] private float riseSpeed;          
    [SerializeField] private float maxHeight;         
    private Vector2 startPosition;
    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (horizontal == false)
        {
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
            if (transform.position.y >= startPosition.y + maxHeight)
            {
                transform.position = startPosition;
            }
        }
        if (horizontal != false)
        {
            transform.position += Vector3.right * riseSpeed * Time.deltaTime;
            if (transform.position.x >= startPosition.x + maxHeight)
            {
                transform.position = startPosition;
            }
        }

    }

    private void OnDisable()
    {
        transform.position = startPosition;
    }
}
