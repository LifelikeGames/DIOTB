using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotation : MonoBehaviour
{
    void Start()
    {
        foreach (Transform child in transform)
        {
            var x = Random.Range(-45, 45);
            var y = Random.Range(-45, 45);
            var z = Random.Range(-45, 45);
            child.Rotate(x,y,z);
        }
    }
}
