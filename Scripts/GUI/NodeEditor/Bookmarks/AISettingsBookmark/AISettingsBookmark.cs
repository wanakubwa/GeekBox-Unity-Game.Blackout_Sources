using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class AISettingsBookmark : ScenarioEditorBookmarkBase
{
    #region Fields

    [SerializeField]
    private ParentEditorInfoElement parentElementPrefab;
    [SerializeField]
    private RectTransform parentsParent;

    [Space]
    [SerializeField]
    private AIParentBehaviorSettings parentsAIBehavior;

    #endregion

    #region Propeties

    public ParentEditorInfoElement ParentElementPrefab { get => parentElementPrefab; }
    public RectTransform ParentsParent { get => parentsParent; }
    private List<ParentEditorInfoElement> SpawnedParentsCollection { 
        get; 
        set; 
    } = new List<ParentEditorInfoElement>();

    public AIParentBehaviorSettings ParentsAIBehavior { 
        get => parentsAIBehavior; 
    }

    #endregion

    #region Methods

    public override void InitializeBookmark()
    {
        base.InitializeBookmark();

        InitializeParentsElements();
        if (SpawnedParentsCollection.Count > 0)
        {
            ParentsAIBehavior.RefreshData(AIManager.Instance.Settings.GetParentAISettingsByParentId(SpawnedParentsCollection.First().CachedParent.ID));
        }
    }

    public void SelectParentElement(ParentEditorInfoElement sender)
    {
        ParentsAIBehavior.RefreshData(AIManager.Instance.Settings.GetParentAISettingsByParentId(sender.CachedParent.ID));
    }

    private void InitializeParentsElements()
    {
        SpawnedParentsCollection.ClearDestroy();

        List<NodeParent> parents = ParentsManager.Instance.ParentsCollection;

        for(int i = 0; i < parents.Count; i++)
        {
            if(parents[i].IDEqual(Constants.NODE_NEUTRAL_PARENT_ID) == false && parents[i].IDEqual(Constants.NODE_PLAYER_PARENT_ID) == false)
            {
                ParentEditorInfoElement newParentInfo = Instantiate(ParentElementPrefab);
                newParentInfo.transform.ResetParent(ParentsParent);
                newParentInfo.SetInfo(parents[i]);

                SpawnedParentsCollection.Add(newParentInfo);
            }
        }
    }

    #endregion

    #region Enums



    #endregion
}