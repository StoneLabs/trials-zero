﻿using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Battlehub.UIControls;

/// <summary>
/// In this demo we use game objects hierarchy as data source (each data item is game object)
/// You can use any hierarchical data with treeview.
/// </summary>
public class CreatorTreeManager : MonoBehaviour
{
	[LabelOverride("Virtualized Tree View")]
    public VirtualizingTreeView treeView;

	[LabelOverride("Root Object")]
    public Transform objectContainer;

	[LabelOverride("Gizmo Manager")]
    public GizmoManager gizmoManager;

	[LabelOverride("Item Icons")]
    public Sprite itemicon;

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
        treeView.ItemDataBinding += OnItemDataBinding;
        treeView.SelectionChanged += OnSelectionChanged;
        treeView.ItemsRemoved += OnItemsRemoved;
        treeView.ItemExpanding += OnItemExpanding;
        treeView.ItemBeginDrag += OnItemBeginDrag;

        treeView.ItemDrop += OnItemDrop;
        treeView.ItemBeginDrop += OnItemBeginDrop;
        treeView.ItemEndDrag += OnItemEndDrag;
        
        ReloadTree();
    }

    public void ReloadTree()
    {
        treeView.Items = new object[] {};
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in objectContainer)
        {
            children.Add(child.gameObject);
        }

        treeView.Items = children;
    }

    private void OnDestroy()
    {
        treeView.ItemDataBinding -= OnItemDataBinding;
        treeView.SelectionChanged -= OnSelectionChanged;
        treeView.ItemsRemoved -= OnItemsRemoved;
        treeView.ItemExpanding -= OnItemExpanding;
        treeView.ItemBeginDrag -= OnItemBeginDrag;
        treeView.ItemBeginDrop -= OnItemBeginDrop;
        treeView.ItemDrop -= OnItemDrop;
        treeView.ItemEndDrag -= OnItemEndDrag;
    }

#region Event Handler
    private void OnSelectionChanged(object sender, SelectionChangedArgs e)
    {
        #if UNITY_EDITOR
            //Do something on selection changed (just syncronized with editor's hierarchy for demo purposes)
            UnityEditor.Selection.objects = e.NewItems.OfType<GameObject>().ToArray();
        #endif

        if (e.NewItem != null && e.NewItem is GameObject)
        {
            GameObject gameObject = (GameObject)e.NewItem;
            gizmoManager.SetGizmo(gameObject.transform);
        }
    }

    private void OnItemBeginDrop(object sender, ItemDropCancelArgs e)
    {
        //object dropTarget = e.DropTarget;
        //if(e.Action == ItemDropAction.SetNextSibling || e.Action == ItemDropAction.SetPrevSibling)
        //{
        //    e.Cancel = true;
        //}

    }

    private void OnItemExpanding(object sender, VirtualizingItemExpandingArgs e)
    {
        //get parent data item (game object in our case)
        GameObject gameObject = (GameObject)e.Item;
        if(gameObject.transform.childCount > 0)
        {
            //get children
            List<GameObject> children = new List<GameObject>();

            for (int i = 0; i < gameObject.transform.childCount; ++i)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;

                children.Add(child);
            }
            
            //Populate children collection
            e.Children = children;
        }
    }

    private void OnItemsRemoved(object sender, ItemsRemovedArgs e)
    {
        //Destroy removed dataitems
        for (int i = 0; i < e.Items.Length; ++i)
        {
            GameObject go = (GameObject)e.Items[i];
            if(go != null)
            {
                Destroy(go);
            }
        }
        gizmoManager.ResetGizmo();
    }

    /// <summary>
    /// This method called for each data item during databinding operation
    /// You have to bind data item properties to ui elements in order to display them.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void OnItemDataBinding(object sender, VirtualizingTreeViewItemDataBindingArgs e)
    {
        GameObject dataItem = e.Item as GameObject;
        if (dataItem != null)
        {   
            //We display dataItem.name using UI.Text 
            Text text = e.ItemPresenter.GetComponentInChildren<Text>(true);
            text.text = dataItem.name;

            //Load icon from resources
            Image icon = e.ItemPresenter.GetComponentsInChildren<Image>()[4];
            icon.sprite = itemicon;

            //And specify whether data item has children (to display expander arrow if needed)
            
            e.HasChildren = dataItem.transform.childCount > 0;
            
        }
    }

    private void OnItemBeginDrag(object sender, ItemArgs e)
    {
        //Could be used to change cursor
    }

    private void OnItemDrop(object sender, ItemDropArgs e)
    {
        if(e.DropTarget == null)
        {
            return;
        }

        Transform dropT = ((GameObject)e.DropTarget).transform;
        
        //Set drag items as children of drop target
        if (e.Action == ItemDropAction.SetLastChild)
        {
            for (int i = 0; i < e.DragItems.Length; ++i)
            {
                Transform dragT = ((GameObject)e.DragItems[i]).transform;
                dragT.SetParent(dropT, true);
                dragT.SetAsLastSibling();
            }
        }

        //Put drag items next to drop target
        else if (e.Action == ItemDropAction.SetNextSibling)
        {
            for (int i = e.DragItems.Length - 1; i >= 0; --i)
            {
                Transform dragT = ((GameObject)e.DragItems[i]).transform;
                int dropTIndex = dropT.GetSiblingIndex();
                if (dragT.parent != dropT.parent)
                {
                    dragT.SetParent(dropT.parent, true);
                    dragT.SetSiblingIndex(dropTIndex + 1);
                }
                else
                {
                    int dragTIndex = dragT.GetSiblingIndex();
                    if (dropTIndex < dragTIndex)
                    {
                        dragT.SetSiblingIndex(dropTIndex + 1);
                    }
                    else
                    {
                        dragT.SetSiblingIndex(dropTIndex);
                    }
                } 
            }
        }

        //Put drag items before drop target
        else if (e.Action == ItemDropAction.SetPrevSibling)
        {
            for (int i = 0; i < e.DragItems.Length; ++i)
            {
                Transform dragT = ((GameObject)e.DragItems[i]).transform;
                if (dragT.parent != dropT.parent)
                {
                    dragT.SetParent(dropT.parent, true);
                }

                int dropTIndex = dropT.GetSiblingIndex();
                int dragTIndex = dragT.GetSiblingIndex();
                if(dropTIndex > dragTIndex)
                {
                    dragT.SetSiblingIndex(dropTIndex - 1);
                }
                else
                {
                    dragT.SetSiblingIndex(dropTIndex);
                }
            }
        }
    }

    private void OnItemEndDrag(object sender, ItemArgs e)
    {            
    }
#endregion

}

