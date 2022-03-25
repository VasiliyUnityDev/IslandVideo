using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LightHouseController : MonoBehaviour
{
    #region Singleton
    public static LightHouseController Instance;

    private void Awake()
    {
        Instance = this;    
    }
    #endregion
    [SerializeField] private string idLightHouse;
    [SerializeField] List<MeshRenderer> currencyLogs = new List<MeshRenderer>();
    [SerializeField] Material normalMat;
    [SerializeField] GameObject canvas;
    [SerializeField] GameObject bigFire;
    [SerializeField] TextMeshProUGUI textAmmountLog;
    [SerializeField] TextMeshProUGUI textNeedSurvivor;
    [SerializeField] private int ammountBigFire;
    public int needSurvivor;
    [SerializeField] private int maxSurvivor;
    public bool isLightHouseOpen;
    public bool isLevelCompleted;
    private int index;

    private void Start()
    {
        LoadSave();
    }

    private void FixedUpdate()
    {
        textAmmountLog.text = ammountBigFire.ToString();
        textNeedSurvivor.text = needSurvivor.ToString() + "/" + maxSurvivor;
        if (ammountBigFire <= 0 && needSurvivor == maxSurvivor)
        {
            isLightHouseOpen = true;
            canvas.SetActive(false);
            bigFire.SetActive(true);
            Invoke("WinGame", 0.5f);
        }
    }

    public void LightHouseOpen()
    {
        if (!isLightHouseOpen)
        {
            if (ammountBigFire > 0)
            {
                currencyLogs[index].material = normalMat;
                index++;
                ammountBigFire--;
            }
        }
    }

    private void WinGame()
    {
        isLevelCompleted = true;
        UIManager.Instance.JoystickScreen(false);
        UIManager.Instance.GameScreen(false);
        Invoke("WinScreen", 1f);
    }

    private void WinScreen()
    {
        UIManager.Instance.WinScreen(true);
    }

    private void LoadSave()
    {
        ammountBigFire = ES3.Load("ammountBigFire" + idLightHouse, ammountBigFire);
        isLightHouseOpen = ES3.Load("isLightHouseOpen" + idLightHouse, isLightHouseOpen);
        isLevelCompleted = ES3.Load("isLevelCompleted" + idLightHouse, isLevelCompleted);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            ES3.Save("ammountBigFire" + idLightHouse, ammountBigFire);
            ES3.Save("isLightHouseOpen" + idLightHouse, isLightHouseOpen);
            ES3.Save("isLevelCompleted" + idLightHouse, isLevelCompleted);
        }
    }
}
