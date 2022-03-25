using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Translate(-speed, 0f, 0f);    
    }
}
