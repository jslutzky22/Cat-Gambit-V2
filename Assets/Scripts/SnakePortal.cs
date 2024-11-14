using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakePortal : MonoBehaviour
{
    [SerializeField] private GameObject snake;
    [SerializeField] private float snakeDelay;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void snakeSpawn()
    {
        StartCoroutine(fireSnake());
        //Instantiate(sorrowCard, cardSpawnPos.transform.position, Quaternion.identity);
        //canAttack = true;
    }
    private IEnumerator fireSnake()
    {
        yield return new WaitForSeconds(snakeDelay);
        Instantiate(snake, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(1f);
        gameObject.SetActive(false);
    }
}
