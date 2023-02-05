using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using System;

public class NodesCreatorBookmark : ScenarioEditorBookmarkBase
{
    #region Fields

    [SerializeField]
    private Toggle addModeToggle;
    [SerializeField]
    private Toggle removeModeToggle;
    [SerializeField]
    private Toggle editModeToggle;
    [SerializeField]
    private Toggle moveModeToggle;

    [Title("Panels")]
    [SerializeField]
    private NodeEditPanel nodePanel;

    #endregion

    #region Propeties

    public Toggle AddModeToggle { get => addModeToggle; }
    public Toggle RemoveModeToggle { get => removeModeToggle; }
    public Toggle EditModeToggle { get => editModeToggle; }
    public Toggle MoveModeToggle { get => moveModeToggle; }
    public NodeEditPanel NodePanel { get => nodePanel; }

    private MapNode CachedSelectedNode
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void InitializeBookmark()
    {
        base.InitializeBookmark();

        OnEditToggleValueChanged(EditModeToggle.isOn);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        EditModeToggle.onValueChanged.AddListener(OnEditToggleValueChanged);
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        EditModeToggle.onValueChanged.RemoveListener(OnEditToggleValueChanged);
    }

    public override void OnMouseLeftEmptyClickHandle(Vector3 worldPosition)
    {
        base.OnMouseLeftEmptyClickHandle(worldPosition);

        if(AddModeToggle.isOn == true)
        {
            NodeParent parent = ParentsManager.Instance.ParentsCollection.GetElementByID(Constants.NODE_NEUTRAL_PARENT_ID);

            Vector3 nodePosition = new Vector3(Mathf.RoundToInt(worldPosition.x), Mathf.RoundToInt(worldPosition.y), Constants.DEFAULT_NODE_Z_POSITION);
            MapNode node = MapManager.Instance.CreateNode(nodePosition, parent);
            node.Initialize();

            node.Values.SetChargeValue(Constants.NODE_CREATE_DEFAULT_CHARGE);
            parent.NodesIdCollection.Add(node.NodeId);
        }
    }

    public override void OnMapNodeClickHandle(MapNode selectedNode)
    {
        base.OnMapNodeClickHandle(selectedNode);

        CachedSelectedNode = selectedNode;

        // Tutaj najlepiej dodac osobne klasy zamiast tego elsifa.
        if (RemoveModeToggle.isOn == true)
        {
            RemoveSelectedNode(selectedNode);
        }
        else if (EditModeToggle.isOn == true)
        {
            EditSelectedNode(selectedNode);
        }
    }

    public override void OnMouseLeftClickHold(Vector3 worldPosition)
    {
        base.OnMouseLeftClickHold(worldPosition);

        if (MoveModeToggle.isOn == true && CachedSelectedNode != null)
        {
            CameraManager.Instance.SetIsCameraMove(false);
            CachedSelectedNode.SetMapPosition(worldPosition);
        }
    }

    public override void OnMouseLeftClickUp(Vector3 worldPosition)
    {
        base.OnMouseLeftClickUp(worldPosition);

        CachedSelectedNode = null;
        CameraManager.Instance.SetIsCameraMove(true);
    }

    private void RemoveSelectedNode(MapNode selectedNode)
    {
        MapManager.Instance.DeleteMapNodeByID(selectedNode.NodeId);
    }

    private void EditSelectedNode(MapNode selectedNode)
    {
        NodePanel.RefreshData(selectedNode);
    }

    private void OnEditToggleValueChanged(bool isOn)
    {
        NodePanel.SetDefaultData();
        NodePanel.gameObject.SetActive(isOn);
    }

    #endregion

    #region Enums



    #endregion
}
