using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceSaveController : MonoBehaviour
{
    public static ResourceSaveController Instance;

    public List<Resource> allResource = new List<Resource>();
    public List<Resource> currencyCrushResource = new List<Resource>();
    public Dictionary<int, bool> Saving = new Dictionary<int, bool>();

    private void Awake()
    {
        Instance = this;
        if (ES3.KeyExists("currencyCrushResource"))
        {
            Saving = ES3.Load("currencyCrushResource") as Dictionary<int, bool>;
            foreach (Resource i in allResource)
            {
                i.isCrushResource = Saving[allResource.IndexOf(i) + 1];
            }
        }
    }

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            foreach(Resource i in allResource)
            {
                if (!Saving.ContainsKey(allResource.IndexOf(i) + 1))
                {
                    Saving.Add(allResource.IndexOf(i) + 1, i.isCrushResource);
                }
                else
                {
                    Saving[allResource.IndexOf(i) + 1] = i.isCrushResource;
                }
            }
            ES3.Save("currencyCrushResource", Saving);
        }
    }

    public void ReloadSave()
    {
        Saving.Clear();
        ES3.Save("currencyCrushResource", Saving);
    }
}
