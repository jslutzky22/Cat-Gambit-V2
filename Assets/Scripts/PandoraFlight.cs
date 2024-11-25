using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PandoraFlight : MonoBehaviour
{
    [SerializeField] private float speed = 5f; 
    private float moveDuration = 3f; 
    private Vector3 movementDirection = new Vector3(1f, 0.5f, 0f).normalized;
    private float elapsedTime = 0f;
    private bool isMoving = true;

    void Update()
    {
        if (isMoving)
        {
            // Move the GameObject
            transform.Translate(movementDirection * speed * Time.deltaTime);

            // Update the elapsed time
            elapsedTime += Time.deltaTime;

            // Check if move duration has passed
            if (elapsedTime >= moveDuration)
            {
                gameObject.SetActive(false);
            }
        }
    }
}
