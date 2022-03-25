using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BridgeController : MonoBehaviour
{
    [SerializeField] private string idBridge;
    [SerializeField] List<MeshRenderer> currencyBricks = new List<MeshRenderer>();
    [SerializeField] Material normalMat;
    [SerializeField] Collider _collider;
    [SerializeField] ParticleSystem zoneUnstack;
    [SerializeField] DOTweenAnimation openScale;
    [SerializeField] GameObject sliderScreen;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    [SerializeField] private float needToOpen;
    private int index;
    public bool isBridgeOpen;

    private void Start()
    {
        LoadSave();
        if (isBridgeOpen) {
            _collider.enabled = false;
            sliderScreen.SetActive(false);
            zoneUnstack.Stop();
            foreach(var i in currencyBricks)
            {
                i.material = normalMat;
            }
        }
    }

    private void FixedUpdate()
    {
        textMeshProUGUI.text = needToOpen.ToString();
    }

    public void BridgeOpen()
    {
        if (!isBridgeOpen)
        {
            if (needToOpen > 0)
            {
                currencyBricks[index].material = normalMat;
                index++;
                needToOpen--;
            }
            if (needToOpen <= 0)
            {
                _collider.enabled = false;
                sliderScreen.SetActive(false);
                zoneUnstack.Stop();
                openScale.DOPlay();
                isBridgeOpen = true;
            }
        }
    }

    private void LoadSave()
    {
        needToOpen = ES3.Load("needToOpen" + idBridge, needToOpen);
        isBridgeOpen = ES3.Load("isBridgeOpen" + idBridge, isBridgeOpen);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            ES3.Save("needToOpen" + idBridge, needToOpen);
            ES3.Save("isBridgeOpen" + idBridge, isBridgeOpen);
        }
    }
}
