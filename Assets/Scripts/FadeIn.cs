using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeIn : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(FadeImage(true));

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator FadeImage(bool fadeAway)
    {
        Debug.Log("Fucntioning?");
        // fade from opaque to transparent
        if (fadeAway)
        {
            // loop over 1 second backwards
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                new Color(1, 1, 1, i);
                yield return null;
            }
        }
        // fade from transparent to opaque
        else
        {
            // loop over 1 second
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                  new Color(1, 1, 1, i);
                yield return null;
            }
        }
    }

}
