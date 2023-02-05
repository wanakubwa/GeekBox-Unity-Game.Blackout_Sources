using UnityEngine;

public class ScenarioEditorBookmarkBase : MonoBehaviour
{
    #region Fields
    [SerializeField]
    private BookmarkType currentType;

    [Space]
    [SerializeField]
    private RectTransform content;

    #endregion

    #region Propeties

    public RectTransform Content { get => content; }
    public BookmarkType CurrentType { get => currentType; }

    #endregion

    #region Methods

    public virtual void OnMouseLeftEmptyClickHandle(Vector3 worldPosition)
    {

    }

    public virtual void OnMapNodeClickHandle(MapNode selectedNode)
    {

    }

    public virtual void OnMouseLeftClickHold(Vector3 screenPosition)
    {

    }

    public virtual void OnMouseLeftClickUp(Vector3 screenPosition)
    {

    }

    public virtual void ExpandBookmark()
    {
        Content.gameObject.SetActive(true);
        InitializeBookmark();
        AttachEvents();
    }

    public virtual void ShrinkBookmark()
    {
        Content.gameObject.SetActive(false);
        DettachEvents();
    }

    public virtual void InitializeBookmark()
    {

    }

    public virtual void AttachEvents()
    {

    }

    public virtual void DettachEvents()
    {

    }

    public virtual void CustomUpdate()
    {

    }

    #endregion

    #region Enums

    public enum BookmarkType
    {
        SCENARIO_CREATOR,
        PARENTS_CREATOR,
        NODES_CREATOR,
        CONNECTIONS_CREATOR,
        AI_SETTINGS,
        OTHER_SETTINGS,
        SCENARIO_DATA
    }

    #endregion
}
