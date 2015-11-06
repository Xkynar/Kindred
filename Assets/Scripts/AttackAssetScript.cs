using UnityEngine;
using System.Collections;
using System.IO;
using System;

#if UNITY_EDITOR
using UnityEditor;

class AttackAssetScript : Editor {

    [MenuItem("Tools/Refresh Attacks")]
    public static void RefreshAttacks()
    {
        AssetDatabase.Refresh();
    }

    [MenuItem("Tools/Create Attacks")]
    public static void CreateAttacks()
    {
        string path = Application.dataPath + "/Scripts/Attacks/";

        string[] attacksFull = Directory.GetFiles(path);

        foreach(string str in attacksFull)
        {
            String attackName = str.Substring(str.LastIndexOf("/") + 1);

            if (attackName.EndsWith(".cs"))
            {
                attackName = attackName.Remove(attackName.Length - 3);

                var attack = ScriptableObject.CreateInstance(attackName);

                var exists = AssetDatabase.LoadAssetAtPath("Assets/Attack/" + attackName + ".asset", Type.GetType("BaseAttack"));

                if (!exists)
                {
                    AssetDatabase.CreateAsset(attack, "Assets/Attack/" + attackName + ".asset");
                    AssetDatabase.SaveAssets();
                }
            }
        }
    }


}
#endif