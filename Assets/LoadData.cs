using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.IO;

public class LoadData : MonoBehaviour
{
    [SerializeField]
    TMPro.TextMeshProUGUI info;
    [SerializeField]
    string AddressNameStr;

    GameObject assetObj;
    bool isLoadSucc;

    void Start()
    {
        isLoadSucc = false;
    }

    public void LoadAssetAsync()
    {
        info.text = "[LoadAssetAsync] loading...";
        Addressables.LoadAssetAsync<GameObject>(AddressNameStr).Completed += OnAssetObjLoaded;
    }
    void OnAssetObjLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        info.text = "[LoadAssetAsync] finish!";
        isLoadSucc = true;
        assetObj = asyncOperationHandle.Result;
    }

    public void CreateObjBtn()
    {
        if (!isLoadSucc)
        {
            info.text = "[CreateObjBtn] LOAD FAILD!";
            return;
        }
        info.text = "[CreateObjBtn]!";
        Vector3 pos = new Vector3(Random.Range(5, -5), Random.Range(4, -4), 0);
        Instantiate(assetObj, pos, Quaternion.identity);
    }

    public void ClearAllCache()
    {
        info.text = "[ClearAllCache] clear cache at " + Application.persistentDataPath;
        var list = Directory.GetDirectories(Application.persistentDataPath);

        foreach (var item in list)
        {
            info.text = info.text + "\nDelete: " + item;
            Directory.Delete(item, true);
        }

        Caching.ClearCache();
    }
}
