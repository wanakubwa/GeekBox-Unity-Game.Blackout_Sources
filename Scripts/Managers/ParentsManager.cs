using UnityEngine;
using System.Collections.Generic;
using System;
using SaveLoadSystem;

public class ParentsManager : SingletonScenarioSaveableManager<ParentsManager, ParentsManagerMemento>, IContentLoadable
{
    #region Fields

    [SerializeField]
    private List<NodeParent> parentsCollection = new List<NodeParent>();

    #endregion

    #region Propeties

    public event Action OnParentsCollectionChanged = delegate { };

    public List<NodeParent> ParentsCollection { 
        get => parentsCollection; 
        private set => parentsCollection = value; 
    }

    #endregion

    #region Methods

    public List<MapNode> GetAllParentsNodesExceptNeutral(int exceptId)
    {
        List<MapNode> output = new List<MapNode>();

        for (int i = 0; i < ParentsCollection.Count; i++)
        {
            if (ParentsCollection[i].IDEqual(exceptId) == false && ParentsCollection[i].IDEqual(Constants.NODE_NEUTRAL_PARENT_ID) == false)
            {
                output.AddRange(ParentsCollection[i].GetMapNodesReferences());
            }
        }

        return output;
    }

    public int GetAllParentsNodesCountExceptNeutral(int exceptId)
    {
        int count = Constants.DEFAULT_VALUE;

        for(int i =0; i < ParentsCollection.Count; i++)
        {
            if(ParentsCollection[i].IDEqual(exceptId) == false && ParentsCollection[i].IDEqual(Constants.NODE_NEUTRAL_PARENT_ID) == false)
            {
                count += ParentsCollection[i].NodesIdCollection.Count;
            }
        }

        return count;
    }

    public int[] GetParentsIds()
    {
        int[] ids = new int[ParentsCollection.Count];
        for (int i = 0; i < ParentsCollection.Count; i++)
        {
            ids[i] = ParentsCollection[i].ID;
        }

        return ids;
    }

    public override void CreateNewScenario()
    {
        ParentsCollection.Clear();
        ParentsCollection = ParentsFactory.GetDefaultParentsCollection();
    }

    public override void LoadManager(ParentsManagerMemento memento)
    {
        DeserializeParentsCollection(memento.ParentsCollectionSaved);
    }

    public void DeleteNodeFromParentByID(int parentID, MapNode node)
    {
        NodeParent parent = ParentsCollection.GetElementByID(parentID);
        if(parent != null)
        {
            parent.RemoveNode(node);
        }
    }

    public void AddNodeToParent(int parentID, MapNode node)
    {
        NodeParent parent = ParentsCollection.GetElementByID(parentID);
        if (parent != null)
        {
            parent.AddNode(node);
        }
    }

    public void SwapNodeParents(int oldParentID, int newParentID, MapNode node)
    {
        DeleteNodeFromParentByID(oldParentID, node);
        AddNodeToParent(newParentID, node);
    }

    public void CreateNewParent()
    {
        ParentsCollection.Add(ParentsFactory.GetNewParent(ParentsCollection));
        OnParentsCollectionChanged();
    }

    public void TryRemoveParent(int id)
    {
        NodeParent parent = ParentsCollection.GetElementByID(id);
        if(parent != null && parent.IsPlayerOrNeutralParent() == false)
        {
            parent.CleanData();
            ParentsCollection.Remove(parent);
        }

        OnParentsCollectionChanged();
    }

    public void LoadContent()
    {
        foreach (NodeParent parent in ParentsCollection)
        {
            parent.Initialize(GameEventsManager.Instance.CheckGameOverState);
        }
    }

    public void UnloadContent()
    {
        ParentsCollection.ClearClean();
    }


    private void DeserializeParentsCollection(List<NodeParent> parentsSaved)
    {
        ParentsCollection.ClearClean();

        for(int i =0; i < parentsSaved.Count; i++)
        {
            NodeParent parentcpy;
            if (parentsSaved[i].ID == Constants.NODE_PLAYER_PARENT_ID)
            {
                parentcpy = new PlayerNodeParent(parentsSaved[i]);
            }
            else
            {
                parentcpy = new NodeParent(parentsSaved[i]);
            }

            ParentsCollection.Add(parentcpy);
        }
    }

    #endregion

    #region Enums


    #endregion
}
