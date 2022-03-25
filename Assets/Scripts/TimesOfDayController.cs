using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TimesOfDayController : MonoBehaviour
{
    #region Singelton
    public static TimesOfDayController Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] Gradient directionalLightGradient;
    [SerializeField] Gradient anbientLightGradient;

    [SerializeField, Range(1, 3000)] float timeDay = 60;
    [SerializeField, Range(0f, 1f)] float timeProgress;
    [SerializeField] private float valueRotateX;

    [SerializeField] Light dirLight;
    public bool isNight;

    Vector3 defaultAngles;

    private void Start()
    {
        defaultAngles = dirLight.transform.localEulerAngles;
    }

    private void FixedUpdate()
    {
        if(timeProgress <= 0.4f)
        {
            isNight = true;
            timeDay = 120;
        }
        if(timeProgress >= 0.4f && timeProgress < 0.71f)
        {
            isNight = false;
            timeDay = 190;
        }
    }
    public bool i;
    private void Update()
    {
        if (Application.isPlaying)
        {
            if (!i)
            {
                timeProgress += Time.deltaTime / timeDay;
            }
            if(i)
            {
                timeProgress -= Time.deltaTime / timeDay;
            }
        }

        if (timeProgress > 0.591f)
        {
            i = true;
        }
        if(timeProgress <= 0)
        {
            i = false;
        }

        dirLight.color = directionalLightGradient.Evaluate(timeProgress);
        RenderSettings.ambientLight = anbientLightGradient.Evaluate(timeProgress);

        dirLight.transform.localEulerAngles = new Vector3(valueRotateX * timeProgress - 90, defaultAngles.y, defaultAngles.z);
    }
}
