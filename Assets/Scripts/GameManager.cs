using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    [SerializeField] private Vector2 lastCheckPointPos;
    bool waiting;
    private AudioSource audioSource;
    public Vector2 LastCheckPointPos { get => lastCheckPointPos; set => lastCheckPointPos = value; }
    public static GameManager Instance { get => instance; set => instance = value; }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            //audioSource.Stop();
            //audioSource.Pause();
        }

        if (SceneManager.GetActiveScene().name == "level 2")
        {
            if (audioSource != null && !audioSource.isPlaying)  
            {
                audioSource.Play(); 
            }
        }
        if (SceneManager.GetActiveScene().name == "MenuScene")
        {
            LastCheckPointPos = transform.position;
        }

    }

    public void Stop(float duration)
    {
        if (waiting)
            return;
        Time.timeScale = 0.0f;
        StartCoroutine(wait(duration));
    }

    IEnumerator wait(float duration)
    {
        waiting = true;
        yield return new WaitForSecondsRealtime(duration);
        Time.timeScale = 1.0f;
        waiting = false;
    }
    public void UnparentWithDelay(Transform objectToUnparent)
    {
        StartCoroutine(UnparentAfterDelay(objectToUnparent));
    }

    private IEnumerator UnparentAfterDelay(Transform objectToUnparent)
    {
        yield return new WaitForEndOfFrame(); 
        if (objectToUnparent != null)
        {
            objectToUnparent.SetParent(null);
        }
    }

}
