using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraVariantController : MonoBehaviour
{
    [SerializeField] GameObject playerCam;
    [SerializeField] GameObject fifnishCam;

    private void FixedUpdate()
    {
        if (LightHouseController.Instance.isLevelCompleted)
        {
            playerCam.SetActive(false);
            fifnishCam.SetActive(true);
        }    
    }
}
