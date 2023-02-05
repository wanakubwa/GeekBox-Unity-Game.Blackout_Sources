using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NodeEditorUIModel : UIModel
{
    #region Fields

    [SerializeField]
    private List<ScenarioEditorBookmarkBase> bookmarks = new List<ScenarioEditorBookmarkBase>();

    #endregion

    #region Propeties

    public List<ScenarioEditorBookmarkBase> Bookmarks { get => bookmarks; }

    public ScenarioEditorBookmarkBase CurrentBookmark
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public void NotifyMouseClickEmptyMap(Vector3 position)
    {
        if(CurrentBookmark != null)
        {
            CurrentBookmark.OnMouseLeftEmptyClickHandle(position);
        }
    }

    public void NotifyMapNodeSelected(MapNode node)
    {
        if (CurrentBookmark != null)
        {
            CurrentBookmark.OnMapNodeClickHandle(node);
        }
    }

    public void NotifyMouseLeftClickHold(Vector3 position)
    {
        if (CurrentBookmark != null)
        {
            CurrentBookmark.OnMouseLeftClickHold(Camera.main.ScreenToWorldPoint(position));
        }
    }

    public void NotifyOnMouseLeftClickUp(Vector3 position)
    {
        if (CurrentBookmark != null)
        {
            CurrentBookmark.OnMouseLeftClickUp(Camera.main.ScreenToWorldPoint(position));
        }
    }

    public void OpenBookmarkOfType(ScenarioEditorBookmarkBase.BookmarkType bookmarkType)
    {
        if(CurrentBookmark != null && CurrentBookmark.CurrentType == bookmarkType)
        {
            return;
        }

        ScenarioEditorBookmarkBase bookmarkBase = GetBookmarkByType(bookmarkType);
        if(bookmarkBase != null)
        {
            ShrinkAllBookmarks();
            bookmarkBase.ExpandBookmark();
            CurrentBookmark = bookmarkBase;
        }
    }

    private void Update()
    {
        if(CurrentBookmark != null)
        {
            CurrentBookmark.CustomUpdate();
        }
    }

    private void ShrinkAllBookmarks()
    {
        for (int i = 0; i < Bookmarks.Count; i++)
        {
            Bookmarks[i].ShrinkBookmark();
        }
    }

    private ScenarioEditorBookmarkBase GetBookmarkByType(ScenarioEditorBookmarkBase.BookmarkType bookmarkType)
    {
        for (int i = 0; i < Bookmarks.Count; i++)
        {
            if (Bookmarks[i].CurrentType == bookmarkType)
            {
                return Bookmarks[i];
            }
        }

        return null;
    }

    #endregion

    #region Enums



    #endregion
}
