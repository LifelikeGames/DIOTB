using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoAndTitle : MonoBehaviour
{
    [SerializeField] private float timeToDisplay = 3;
    void Start()
    {
        StartCoroutine(DisappearAfter());
        
        IEnumerator DisappearAfter()
        {
            Time.timeScale = 0;
            yield return new WaitForSecondsRealtime(timeToDisplay);
            Time.timeScale = 1;
            Destroy(gameObject);
        }
    }

    
}
