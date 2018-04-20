using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class AppHelper : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static void updateFontSize()
    {

        int font = PlayerPrefs.GetInt("font_size");

        if (font != 0)
        {

            var textComponents = Component.FindObjectsOfType<Text>();

            foreach (var component in textComponents)
            {
                component.fontSize = font;
            }
        }
    }

    #region Depricated
    //private const string EditorPrefsKey = "Utilities.FontReplacer";
    //private const string MenuItemName = "Utilities/Replace Fonts...";

    //private Font _src;
    //private Font _dest;
    //private bool _includePrefabs;

    //public void OnEnable()
    //{
    //    var path = EditorPrefs.GetString(EditorPrefsKey + ".src");
    //    if (path != string.Empty)
    //        _src = AssetDatabase.LoadAssetAtPath<Font>(path) ?? Resources.GetBuiltinResource<Font>(path);

    //    path = EditorPrefs.GetString(EditorPrefsKey + ".dest");
    //    if (path != string.Empty)
    //        _dest = AssetDatabase.LoadAssetAtPath<Font>(path) ?? Resources.GetBuiltinResource<Font>(path);

    //    _includePrefabs = EditorPrefs.GetBool(EditorPrefsKey + ".includePrefabs", false);
    //}


    //public static void ReplaceFonts(int nSize, bool includePrefabs = false)
    //{
    //    var sceneMatches = 0;
    //    for (var i = 0; i < SceneManager.sceneCount; i++)
    //    {
    //        var scene = SceneManager.GetSceneAt(i);
    //        var gos = new List<GameObject>(scene.GetRootGameObjects());
    //        foreach (var go in gos)
    //        {
    //            sceneMatches += ReplaceFonts(nSize, go.GetComponentsInChildren<Text>(true));
    //        }
    //    }

    //    if (includePrefabs)
    //    {
    //        var prefabMatches = 0;
    //        var prefabs =
    //            AssetDatabase.FindAssets("t:Prefab")
    //                .Select(guid => AssetDatabase.LoadAssetAtPath<GameObject>(AssetDatabase.GUIDToAssetPath(guid)));
    //        foreach (var prefab in prefabs)
    //        {
    //            prefabMatches += ReplaceFonts(nSize, prefab.GetComponentsInChildren<Text>(true));
    //        }

    //        Debug.LogFormat("Replaced {0} font(s), {1} in scenes, {2} in prefabs", sceneMatches + prefabMatches, sceneMatches, prefabMatches);
    //    }
    //    else
    //    {
    //        Debug.LogFormat("Replaced {0} font(s) in scenes", sceneMatches);
    //    }
    //}

    //private static int ReplaceFonts(int newSize, IEnumerable<Text> texts)
    //{
    //    var matches = 0;
    //    foreach (var text in texts)
    //    {
    //        text.fontSize = newSize;
    //        matches++;
    //    }
    //    return matches;
    //}

    //private static string GetAssetPath(Object assetObject, string defaultExtension)
    //{
    //    var path = AssetDatabase.GetAssetPath(assetObject);
    //    if (path.StartsWith("Library/", System.StringComparison.InvariantCultureIgnoreCase))
    //        path = assetObject.name + "." + defaultExtension;
    //    return path;
    //}
    #endregion
}
