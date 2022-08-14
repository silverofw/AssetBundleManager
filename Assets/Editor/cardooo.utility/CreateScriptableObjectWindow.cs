using System;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.ProjectWindowCallback;
using UnityEngine;

internal class EndNameEdit : EndNameEditAction
{
	#region implemented abstract members of EndNameEditAction
	public override void Action(int instanceId, string pathName, string resourceFile)
	{
		AssetDatabase.CreateAsset(EditorUtility.InstanceIDToObject(instanceId), AssetDatabase.GenerateUniqueAssetPath(pathName));
	}

	#endregion
}

/// <summary>
/// Scriptable object window.
/// </summary>
public class CreateScriptableObjectWindow : EditorWindow
{
	private string assemblyName = "Assembly-CSharp";
	private int selectedIndex;
	private string[] names;
	private Type[] types;

	public Type[] Types
	{
		get { return types; }
		set
		{
			types = value;
			names = types.Select(t => t.FullName).ToArray();
		}
	}

	//[MenuItem("Assets/Create/Cardooo/ScriptableObject #m", false, 21640000)]
	[MenuItem("Assets/Create/Cardooo/ScriptableObject")]
	public static void OpenWindow()
	{
		GetWindow(typeof(CreateScriptableObjectWindow));
	}

	void Awake()
    {
		titleContent = new GUIContent("CreateScriptableObject", "Welcome~");
		Load();
	}
	void Load()
	{
		var assembly = Assembly.Load(new AssemblyName(assemblyName));

		// Get all classes derived from ScriptableObject
		var allScriptableObjects = (from t in assembly.GetTypes()
									where t.IsSubclassOf(typeof(ScriptableObject))
									select t).ToArray();
		Types = allScriptableObjects;
	}

    public void OnGUI()
	{
		GUILayout.Label("--[ Select Assembly ]--");
		assemblyName = EditorGUILayout.TextField("AssemblyName: ", assemblyName);

		if (GUILayout.Button("Load"))
		{
			Load();
		}
		GUILayout.BeginHorizontal();
		if (GUILayout.Button("Assembly-CSharp"))
		{
			assemblyName = "Assembly-CSharp";
			Load();
		}
		if (GUILayout.Button("Assembly-CSharp-Editor"))
		{
			assemblyName = "Assembly-CSharp-Editor";
			Load();
		}
		GUILayout.EndHorizontal();

		GUILayout.Label("--[ Create ScriptableObject]--");
		selectedIndex = EditorGUILayout.Popup("Types: ", selectedIndex, names);

		if (GUILayout.Button("Create"))
		{
			var asset = ScriptableObject.CreateInstance(types[selectedIndex]);
			ProjectWindowUtil.StartNameEditingIfProjectWindowExists(
				asset.GetInstanceID(),
				ScriptableObject.CreateInstance<EndNameEdit>(),
				string.Format("{0}.asset", names[selectedIndex]),
				AssetPreview.GetMiniThumbnail(asset),
				null);

			Close();
		}
	}
}
