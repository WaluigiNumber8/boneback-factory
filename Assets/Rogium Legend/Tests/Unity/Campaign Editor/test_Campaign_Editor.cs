using NUnit.Framework;
using Rogium.Editors.Campaign;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Campaign_Editor
{
    private CampaignEditorOverseerMono campaignEditor;
    private LibraryOverseer lib;
    
    private PackList packs;
    private IList<AssetBase> foundAssets;

    [SetUp]
    public void Setup()
    {
        TestBuilder.SetupCanvas(out GameObject canvas);
        GameObject campaignEditorPrefab = Object.Instantiate(AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_CampaignEditor.prefab"));

        packs = LibraryOverseer.Instance.GetPacksCopy;
        lib = LibraryOverseer.Instance;
        campaignEditor = campaignEditorPrefab.GetComponent<CampaignEditorOverseerMono>();
    }

    [UnityTest]
    public IEnumerator fill_with_all_available_packs()
    {
        lib.ActivateCampaignEditor(0);
        yield return new WaitForSeconds(0.1f);
        campaignEditor.FillMenu();
        yield return new WaitForSeconds(0.1f);
        Assert.Equals(lib.PackCount, campaignEditor.SelectionPicker.SelectionCount);
    }
    
    [UnityTest]
    public IEnumerator grab_selected_data_properly()
    {
        lib.ActivateCampaignEditor(0);
        campaignEditor.FillMenu();
        yield return new WaitForSeconds(0.1f);
        campaignEditor.SelectionPicker.WhenAssetSelected(lib.GetPacksCopy[0]);
        campaignEditor.SelectionPicker.WhenAssetSelected(lib.GetPacksCopy[1]);
        yield return new WaitForSeconds(0.1f);
        campaignEditor.SelectionPicker.ConfirmSelection();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(2, foundAssets.Count);
        Assert.AreEqual(packs[0].ID, foundAssets[0].ID);
        
    }

    [UnityTest]
    public IEnumerator select_packs_used_in_loaded_campaign_correctly()
    {
        lib.ActivateCampaignEditor(0);
        yield return new WaitForSeconds(0.1f);
        
    }

    [UnityTest]
    public IEnumerator load_second_campaign_correctly()
    {
        lib.ActivateCampaignEditor(0);
        campaignEditor.FillMenu();
        yield return new WaitForSeconds(0.1f);
        CampaignEditorOverseer.Instance.CompleteEditing();
        lib.ActivateCampaignEditor(0);
        campaignEditor.FillMenu();
    }
    
    private void StoreSelectedAssets(IList<AssetBase> foundAssets)
    {
        this.foundAssets = foundAssets;
    }
}