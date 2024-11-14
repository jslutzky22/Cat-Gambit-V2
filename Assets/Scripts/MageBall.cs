using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MageBall : MonoBehaviour
{
    //[SerializeField] private GameObject warningBall;
    [SerializeField] private GameObject ballHolder;
    [SerializeField] private GameObject dangerBall;
    //[SerializeField] private float spawnDelay;
    //[SerializeField] private float deleteDelay;
    //[SerializeField] private bool damageBall;
    // Start is called before the first frame update
    void Start()
    {
       /* if (damageBall == false)
        {

        }
        if (damageBall == true)
        {
            //StartCoroutine(ballEnable());
        }*/
        //StartCoroutine(ballEnable());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void destroyBalls()
    {
        Destroy(ballHolder);
    }

    private void enableDanger()
    {
        dangerBall.SetActive(true);
        gameObject.SetActive(false);
    }


}
