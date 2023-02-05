using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System.Linq;

public class ParentsCreatorBookmark : ScenarioEditorBookmarkBase
{
    #region Fields

    [SerializeField]
    private RectTransform parentsContent;
    [SerializeField]
    private ParentEditorInfoElement parentInfoPrefab;

    [Title("Panels")]
    [SerializeField]
    private ParentEditorPanel parentEditor;

    #endregion

    #region Propeties

    public RectTransform ParentsContent { get => parentsContent; }
    public ParentEditorInfoElement ParentInfoPrefab { get => parentInfoPrefab; }

    private List<ParentEditorInfoElement> SpawnedParentsCollection
    {
        get;
        set;
    } = new List<ParentEditorInfoElement>();

    public ParentEditorPanel ParentEditor { get => parentEditor; }

    #endregion

    #region Methods

    public override void InitializeBookmark()
    {
        base.InitializeBookmark();
        RefreshParentsContent();
        OnSelectedParent(SpawnedParentsCollection.FirstOrDefault());
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        ParentsManager.Instance.OnParentsCollectionChanged += RefreshParentsContent;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        ParentsManager.Instance.OnParentsCollectionChanged -= RefreshParentsContent;
    }

    public void AddNewParent()
    {
        ParentsManager.Instance.CreateNewParent();
    }

    public void RemoveParent(ParentEditorInfoElement sender)
    {
        ParentsManager.Instance.TryRemoveParent(sender.CachedParent.ID);
    }

    public void OnSelectedParent(ParentEditorInfoElement sender)
    {
        ParentEditor.RefreshPanel(sender.CachedParent);
    }

    private void RefreshParentsContent()
    {
        SpawnedParentsCollection.ClearDestroy();

        List<NodeParent> scenarioParents = GetAvaibleParents();
        for(int i =0; i < scenarioParents.Count; i++)
        {
            ParentEditorInfoElement spawnedParentInfo = Instantiate(ParentInfoPrefab);
            spawnedParentInfo.transform.ResetParent(ParentsContent);
            spawnedParentInfo.SetInfo(scenarioParents[i]);

            SpawnedParentsCollection.Add(spawnedParentInfo);
        }
    }

    private List<NodeParent> GetAvaibleParents()
    {
        return ParentsManager.Instance.ParentsCollection;
    }

    #endregion

    #region Enums



    #endregion
}
