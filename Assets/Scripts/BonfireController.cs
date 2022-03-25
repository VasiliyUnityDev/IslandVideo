using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class BonfireController : MonoBehaviour
{
    [SerializeField] private string idBonfire;

    [SerializeField] GameObject closeBonfire;
    [SerializeField] GameObject openBonfire;
    [SerializeField] private float needToOpen;
    [SerializeField] private int indexToOpen;

    [SerializeField] Light lightFire;
    [SerializeField] GameObject effectFire;
    [SerializeField] private float rangeFire;

    [SerializeField] GameObject screenFire;
    [SerializeField] Slider sliderFire;
    [SerializeField] Image _image;
    [SerializeField] Gradient gradientSlider;

    [SerializeField] GameObject screenUI;
    [SerializeField] TextMeshProUGUI textNumber;

    [SerializeField] private float maxTime;
    [SerializeField] private float timer;
    [SerializeField] private bool isTimer;

    public bool isBonfireOpen;
    public bool fireOff;

    private void Start()
    {
        LoadSave();
        if (isBonfireOpen) { closeBonfire.SetActive(false); openBonfire.SetActive(true); }
    }

    private void FixedUpdate()
    {
        textNumber.text = needToOpen.ToString();
        screenUI.SetActive(true);

        if (!isBonfireOpen)
            return;
        
        closeBonfire.SetActive(false); openBonfire.SetActive(true);
        screenUI.SetActive(false);

        sliderFire.value = rangeFire;
        _image.color = gradientSlider.Evaluate(sliderFire.value / 20);
        if (rangeFire == 0) {
            lightFire.enabled = false;
            effectFire.SetActive(false);
            fireOff = true;
            return;
        }

        lightFire.enabled = true;
        effectFire.SetActive(true);
        lightFire.range = rangeFire;
        fireOff = false;
    }

    private void Update()
    {
        Timer();
    }

    private void Timer()
    {
        if(isBonfireOpen && rangeFire > 0)
        {
            if(timer < maxTime)
            {
                timer += Time.deltaTime;
                isTimer = false;
            }
            if(timer >= maxTime)
            {
                if (!isTimer)
                {
                    rangeFire -= 1;
                    timer = 0;
                    isTimer = true;
                }
            }
        }
    }

    public void BonfireOpen()
    {
        if (!isBonfireOpen)
        {
            if (needToOpen > 0)
            {
                if (Backpack—ontroller.Instance.indexStuff == indexToOpen)
                {
                    needToOpen--;
                }
            }
            if (needToOpen <= 0)
            {
                isBonfireOpen = true;
            }
        }
    }

    public void FireUp(float value)
    {
        rangeFire += value;
    }

    public void ScreenFire(bool isActive)
    {
        screenFire.SetActive(isActive);
        if (!isActive){
            screenFire.transform.DOScale(0f, 0.6f);
            return;
        }
        screenFire.transform.DOScale(2.3289f, 0.6f);
    }

    private void LoadSave()
    {
        needToOpen = ES3.Load("needToOpen" + idBonfire, needToOpen);
        rangeFire = ES3.Load("rangeFire" + idBonfire, rangeFire);
        isBonfireOpen = ES3.Load("isBonfireOpen" + idBonfire, isBonfireOpen);
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            ES3.Save("needToOpen" + idBonfire, needToOpen);
            ES3.Save("rangeFire" + idBonfire, rangeFire);
            ES3.Save("isBonfireOpen" + idBonfire, isBonfireOpen);
        }
    }
}
