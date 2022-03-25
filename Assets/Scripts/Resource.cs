using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG;
using DG.Tweening;

public class Resource : MonoBehaviour
{
    [SerializeField] GameObject crushResource;
    [SerializeField] GameObject prefabRemainder;
    [SerializeField] GameObject pointRemainder;
    [SerializeField] DOTweenAnimation spine;
    [SerializeField] private int healthResource;
    public int indexResource;
    public bool isCrushResource;

    private void Start()
    {
        if (isCrushResource)
        {
            Instantiate(prefabRemainder, pointRemainder.transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    private void FixedUpdate()
    {
        if(healthResource <= 0)
        {
            Instantiate(prefabRemainder, pointRemainder.transform.position, Quaternion.identity);
            crushResource.SetActive(true);
            crushResource.transform.parent = null;
            isCrushResource = true;
            Destroy(gameObject);
        }
    }

    public void PunchPlayer(int numberMinus)
    {
        healthResource -= numberMinus;
        spine.DORestart();
    }
}
