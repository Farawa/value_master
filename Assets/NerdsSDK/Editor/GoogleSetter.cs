#if false //UNITY_IOS
using UnityEngine;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.iOS.Xcode;
using System.IO;
using System.Collections.Generic;

public class GoogleSetter : MonoBehaviour
{
    [PostProcessBuild]
    static void OnPostprocessBuild(BuildTarget buildTarget, string path)
    {
        // Read plist
        var plistPath = Path.Combine(path, "Info.plist");
        var plist = new PlistDocument();
        plist.ReadFromFile(plistPath);

        // Update value
        PlistElementDict rootDict = plist.root;
        rootDict.SetString("GADApplicationIdentifier", "ca-app-pub-2129001407304490~1817143509");

        // Write plist
        File.WriteAllText(plistPath, plist.WriteToString());
    }
}
#endif