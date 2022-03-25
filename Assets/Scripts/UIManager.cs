using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    #region Singelton
    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("Screens")]
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject joystickScreen;
    [SerializeField] GameObject gameScreen;
    [SerializeField] GameObject reloadScreen;
    
    [Header("Buttons")]
    [SerializeField] GameObject houseButton;

    public void HouseButtonActive(bool isActive)
    {
        houseButton.SetActive(isActive);
    }

    public void SleepActive()
    {
        PlayerController.Instance.SleepPlayer();
        houseButton.SetActive(false);
    }

    public void NextLevel()
    {
        ReloadScreen(true);
    }

    public void JoystickScreen(bool isActive)
    {
        joystickScreen.SetActive(isActive);
    }

    public void GameScreen(bool isActive)
    {
        gameScreen.SetActive(isActive);
    }

    public void WinScreen(bool isActive)
    {
        winScreen.SetActive(isActive);
    }

    private void ReloadScreen(bool isActive)
    {
        reloadScreen.SetActive(isActive);
        Invoke("LoadLevel", 2f);
    }

    private void LoadLevel()
    {
        SceneManager.LoadScene(0);
    }
}
