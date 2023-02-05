using UnityEngine;
using System.Collections;

[RequireComponent(typeof(NodeEditorUIModel), typeof(NodeEditorUIView))]
public class NodeEditorUIController : UIController
{
    #region Fields

    [SerializeField]
    private ScenarioEditorBookmarkBase.BookmarkType defaultBookmark = ScenarioEditorBookmarkBase.BookmarkType.SCENARIO_CREATOR;

    #endregion

    #region Propeties

    NodeEditorUIModel CurrentModel
    {
        get; set;
    }

    public ScenarioEditorBookmarkBase.BookmarkType DefaultBookmark { get => defaultBookmark; }

    #endregion

    #region Methods

    public override void AttachEvents()
    {
        base.AttachEvents();

        //todo : przeniesc tu eventy;
    }

    public override void Initialize()
    {
        base.Initialize();

        CurrentModel = GetModel<NodeEditorUIModel>();
        CurrentModel.OpenBookmarkOfType(DefaultBookmark);

        SelectingManager.Instance.OnMapClick += OnMouseClickEmptyMap;
        SelectingManager.Instance.OnMapNodeSelected += OnMapNodeSelectedHandle;

        InputManager.Instance.OnMouseLeftHold += OnMouseClickHoldHandle;
        InputManager.Instance.OnMouseLeftUp += OnMouseClickUpHandle;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if(SelectingManager.Instance != null)
        {
            SelectingManager.Instance.OnMapClick -= OnMouseClickEmptyMap;
            SelectingManager.Instance.OnMapNodeSelected -= OnMapNodeSelectedHandle;
        }

        if(InputManager.Instance != null)
        {
            InputManager.Instance.OnMouseLeftHold -= OnMouseClickHoldHandle;
            InputManager.Instance.OnMouseLeftUp -= OnMouseClickUpHandle;
        }
    }

    public void OpenBookmark(BookmarkButton sender)
    {
        CurrentModel.OpenBookmarkOfType(sender.Bookmark);
    }

    private void OnMouseClickEmptyMap(Vector3 worldPosition)
    {
        CurrentModel.NotifyMouseClickEmptyMap(worldPosition);
    }

    private void OnMapNodeSelectedHandle(MapNode node)
    {
        CurrentModel.NotifyMapNodeSelected(node);
    }

    private void OnMouseClickHoldHandle(Vector2 worldPosition)
    {
        CurrentModel.NotifyMouseLeftClickHold(worldPosition);
    }

    private void OnMouseClickUpHandle(Vector2 worldPosition)
    {
        CurrentModel.NotifyOnMouseLeftClickUp(worldPosition);
    }

    #endregion

    #region Enums



    #endregion
}
