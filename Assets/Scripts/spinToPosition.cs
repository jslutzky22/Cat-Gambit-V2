using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spinToPosition : MonoBehaviour
{
    //[SerializeField] private GameObject targetPosition; 
    [SerializeField] private float speed = 5f;
    private bool shouldMove;
    Animator m_Animator;

    private void Start()
    {
        m_Animator = gameObject.GetComponent<Animator>();
    }

    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Debug.Log("Going!");
            shouldMove = true;
            m_Animator.SetBool("Touched", true);
        }
    }

    private void Update()
    {
        if (shouldMove)
        {


            transform.position += Vector3.up * speed * Time.deltaTime;

          
            StartCoroutine(VanishAfterDelay());
            

        }
    }
    private IEnumerator VanishAfterDelay()
    {

        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);

    }

}



