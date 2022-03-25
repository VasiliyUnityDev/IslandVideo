using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stuff : MonoBehaviour
{
    public void DestroyStuff()
    {
        Destroy(gameObject, 0.5f);
    }
}
