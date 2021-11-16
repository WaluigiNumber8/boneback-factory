using System.Collections;
using NUnit.Framework;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_CampaignEditor
{
    [SetUp]
    public void Setup()
    {
        TestBuilder.SetupCanvas(out GameObject canvas);
        GameObject campaignEditor = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_CampaignEditor.prefab");
        campaignEditor.transform.parent = canvas.transform;

    }

    [UnityTest]
    public IEnumerator fill_menu_with_3_packs()
    {
        
        yield return new WaitForSeconds(0.1f);
    }
}