using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnOffAfterDelay : MonoBehaviour
{

    [SerializeField] private float offDelay;
    [SerializeField] private ParticleSystem particles;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(off());
    }

    private IEnumerator off()
    {
        yield return new WaitForSeconds(offDelay);
        particles.Stop();
        yield return new WaitForSeconds(4f);
        particles.Play();
        Destroy(gameObject);
    }
}
