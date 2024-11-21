using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEnd : MonoBehaviour
{
    
    [SerializeField] private string sceneName;
    private GameManager gm;
    [SerializeField] private GameObject levelTransition;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gm.LastCheckPointPos = new Vector2(0, 0);
            StartCoroutine(levelDelay());

            
            //gm.LastCheckPointPos = new Vector2(0, 0);

        }
    }

    private IEnumerator levelDelay()
    {
        levelTransition.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneName);
    }

}
