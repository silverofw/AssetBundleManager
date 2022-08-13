using UnityEngine;
using System.Collections;
using System.IO;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using UnityEngine.AddressableAssets;
using System.Threading.Tasks;

public class Loby : MonoBehaviour
{
    public TMPro.TextMeshProUGUI m_TextMeshPro;

    AppDomain appdomain;

    MemoryStream fs;
    MemoryStream p;

    async void Start()
    {
        appdomain = await GetAppDomain();

        InitializeILRuntime();
        OnHotFixLoaded();
    }

    async Task<AppDomain> GetAppDomain()
    {
        AppDomain domain = new AppDomain();

        TextAsset asset = await Addressables.LoadAssetAsync<TextAsset>("Assets/Res/Hotfix/HotFix.dll.bytes").Task;
        byte[] dll = asset.bytes;
        asset = await Addressables.LoadAssetAsync<TextAsset>("Assets/Res/Hotfix/HotFix.pdb.bytes").Task;
        byte[] pdb = asset.bytes;

        fs = new MemoryStream(dll);
        p = new MemoryStream(pdb);
        try
        {
            domain.LoadAssembly(fs, p, new ILRuntime.Mono.Cecil.Pdb.PdbReaderProvider());
        }
        catch
        {
            Debug.LogError("¸ê®Æ¸ÑªR¿ù»~!");
        }

        return domain;
    }

    void InitializeILRuntime()
    {
#if DEBUG && (UNITY_EDITOR || UNITY_ANDROID || UNITY_IPHONE)
        appdomain.UnityMainThreadID = System.Threading.Thread.CurrentThread.ManagedThreadId;
#endif
    }

    void OnHotFixLoaded()
    {
        object obj = appdomain.Instantiate("HotFix.Class1");

        IType type = appdomain.LoadedTypes["HotFix.Class1"];
        IMethod method = type.GetMethod("GetCompanyName", 0);
        using (var ctx = appdomain.BeginInvoke(method))
        {
            ctx.PushObject(obj);
            ctx.Invoke();
            string id = ctx.ReadValueType<string>();
            m_TextMeshPro.text = id;
        }
    }

    private void OnDestroy()
    {
        if (fs != null)
            fs.Close();
        if (p != null)
            p.Close();
        fs = null;
        p = null;
    }
}
