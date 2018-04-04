using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Battlehub.UIControls;

/// <summary>
/// In this demo we use game objects hierarchy as data source (each data item is game object)
/// You can use any hierarchical data with treeview.
/// </summary>
public class SpawnerTreeManager : MonoBehaviour
{
	[LabelOverride("Virtualized Tree View")]
    public VirtualizingTreeView TreeView;

	[LabelOverride("Root Object")]
    public Transform ObjectContainer;

	[LabelOverride("Tree Manager")]
    public CreatorTreeManager treeManager;

	[LabelOverride("Item Icons")]
    public Sprite itemicon;

	[LabelOverride("Creatable prefabs")]
    public List<Transform> creatables;

	[LabelOverride("Object spawn distance")]
    public float spawnDistance = 5;

    // Currently selected item is buffered here
    private Transform currentSelection;

    public static bool IsPrefab(Transform This)
    {
        if (Application.isEditor && !Application.isPlaying)
        {
            throw new InvalidOperationException("Does not work in edit mode");
        }
        return This.gameObject.scene.buildIndex < 0;
    }

    private void Start()
    {
        TreeView.ItemDataBinding += OnItemDataBinding;
        TreeView.SelectionChanged += OnSelectionChanged;
        
        TreeView.Items = creatables;
    }

    private void OnDestroy()
    {
        TreeView.ItemDataBinding -= OnItemDataBinding;
        TreeView.SelectionChanged -= OnSelectionChanged;
    }

    // Callback function from 'CREATE' button
    public void SpawnSelection()
    {
        Vector3 spawnPos = Camera.main.transform.position + Camera.main.transform.forward.normalized * spawnDistance;
        Transform transform = Instantiate(currentSelection, spawnPos, Quaternion.identity, ObjectContainer) as Transform;
        treeManager.ReloadTree();
    }

#region Event Handler
    private void OnSelectionChanged(object sender, SelectionChangedArgs e)
    {
        currentSelection = e.NewItem as Transform ?? currentSelection;
    }

    // This method called for each data item during databinding operation
    // You have to bind data item properties to ui elements in order to display them.
    private void OnItemDataBinding(object sender, VirtualizingTreeViewItemDataBindingArgs e)
    {
        Transform dataItem = e.Item as Transform;
        if (dataItem != null)
        {   
            Creatable creatable = dataItem.GetComponent<Creatable>() ?? Creatable.unknown;
            
            //We display dataItem.name using UI.Text 
            Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);
            text.text = "" + creatable.id + " - " + creatable.name;

            //Load icon from resources
            Image icon = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
            icon.sprite = itemicon;

            //And specify whether data item has children (to display expander arrow if needed)
            e.HasChildren = false;
        }
    }
#endregion

}

