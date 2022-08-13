using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

public class Entry : MonoBehaviour
{
    // Start is called before the first frame update
    async void Start()
    {
        string NextSceneAddress = "Assets/Res/Scene/Loby.unity";
        await Addressables.LoadSceneAsync(NextSceneAddress).Task;
    }
}
