using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;



public class LoadData : MonoBehaviour
{
    [SerializeField]
    Button LoadObjBtn;

    [SerializeField]
    string AddressNameStr;

    GameObject assetObj;
    bool isLoadSucc;

    void Start()
    {
        LoadObjBtn.interactable = false;
        isLoadSucc = false;
        Addressables.LoadAssetAsync<GameObject>(AddressNameStr).Completed += OnAssetObjLoaded;
        LoadObjBtn.onClick.AddListener(CreateObjBtn);
    }

    void OnAssetObjLoaded(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        LoadObjBtn.interactable = true;
        isLoadSucc = true;
        assetObj = asyncOperationHandle.Result;
    }

    void CreateObjBtn()
    {
        Vector3 pos = new Vector3(Random.Range(5, -5), Random.Range(4, -4), 0);
        Instantiate(assetObj, pos, Quaternion.identity);
    }
}
