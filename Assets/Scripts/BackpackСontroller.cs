using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class BackpackСontroller : MonoBehaviour
{
    #region Singelton
    public static BackpackСontroller Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [Header("BackpackStuff")]
    [SerializeField] List<GameObject> prefabsStuff = new List<GameObject>();
    [SerializeField] List<GameObject> currencyStuff = new List<GameObject>();
    public int indexStuff;
    public bool isMaxBackpack;

    [Header("Stuck&Unstuck")]
    [SerializeField] Transform pointBackpack;
    [SerializeField] Transform pointFollowStuff;
    [SerializeField] private int maxAmmountStuff;
    [SerializeField] private float offsetStuffY;

    [Header("UIBackpack")]
    [SerializeField] TextMeshProUGUI textCounterStuff;
    [SerializeField] TextMeshProUGUI textStickman;

    [Header("EffectBackpack")]
    [SerializeField] ParticleSystem effectMax;

    private void FixedUpdate()
    {
        textCounterStuff.text = "" + currencyStuff.Count;
        textStickman.text = LightHouseController.Instance.needSurvivor + "/1";
        if(currencyStuff.Count >= maxAmmountStuff){
            if (!isMaxBackpack)
            {
                effectMax.Play();
                isMaxBackpack = true;
            }
        }
        else{
            isMaxBackpack = false;
        }
    }

    private void Update()
    {
        StuffFollow();
    }

    public IEnumerator NewStuff(int index)
    {
        GameObject newStuff = Instantiate(prefabsStuff[index], transform.position, Quaternion.identity);
        currencyStuff.Add(newStuff);
        yield return new WaitForSeconds(0.1f);
    }

    public void UnstackStuff(GameObject pointUnstack, int indexUnstack)
    {
        if (currencyStuff.Count > 0)
        {
            for (int i = currencyStuff.Count - 1; i >= 0; i--)
            {
                currencyStuff[i].transform.SetParent(null);
                currencyStuff[i].transform.DOMove(transform.position + Vector3.up * 4f, 0.25f);
                currencyStuff[i].transform.DOJump(pointUnstack.transform.position, 4, 1, 0.3f).SetDelay(0.25f);
                if (indexUnstack == 0)
                {
                    if (pointUnstack.GetComponent<BonfireController>().isBonfireOpen)
                    {
                        pointUnstack.GetComponent<BonfireController>().FireUp(1);
                    }
                    if (!pointUnstack.GetComponent<BonfireController>().isBonfireOpen)
                    {
                        pointUnstack.GetComponent<BonfireController>().BonfireOpen();
                    }
                }
                if (indexUnstack == 1)
                {
                    if (!pointUnstack.GetComponent<BridgeController>().isBridgeOpen)
                    {
                        pointUnstack.GetComponent<BridgeController>().BridgeOpen();
                    }
                }
                if(indexUnstack == 2)
                {
                    if (!pointUnstack.GetComponent<LightHouseController>().isLightHouseOpen)
                    {
                        pointUnstack.GetComponent<LightHouseController>().LightHouseOpen();
                    }
                }
                currencyStuff[i].GetComponent<Stuff>().DestroyStuff();
                currencyStuff.Remove(currencyStuff[i]);
                return;
            }
        }
    }

    private void StuffFollow()
    {
        if (currencyStuff.Count > 0)
        {
            var startPos = pointBackpack.position;
            float index = 0;
            foreach (var item in currencyStuff)
            {
                Vector3 newPosStuff = startPos + Vector3.up * offsetStuffY * currencyStuff.IndexOf(item);
                item.transform.position = Vector3.Lerp(item.transform.position, newPosStuff, Time.deltaTime / index * 115);
                item.transform.rotation = pointBackpack.transform.rotation;
                pointFollowStuff.transform.position = item.transform.position;
                index++;
            }
        }
    }
}
