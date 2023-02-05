using SaveLoadSystem;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SelectingManager : ManagerSingletonBase<SelectingManager>, IContentLoadable
{
    #region Fields

    #endregion

    #region Propeties

    /// <summary>
    /// Pozycja myszy w swiecie.
    /// </summary>
    public event Action<Vector3> OnMapClick = delegate { };
    public event Action<MapNode> OnMapNodeSelected = delegate { };
    public event Action<MapNode> OnMapNodeDeselected = delegate { };

    public MapNode LastSelectedNode
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public override void AttachEvents()
    {
        base.AttachEvents();

        InputManager.Instance.OnMouseLeftClick += HandleMouseLeftClick;
        MapManager.Instance.OnMapNodeDeleted += OnMapNodeDeletedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (InputManager.Instance != null)
        {
            InputManager.Instance.OnMouseLeftClick -= HandleMouseLeftClick;
        }

        if(MapManager.Instance != null)
        {
            MapManager.Instance.OnMapNodeDeleted -= OnMapNodeDeletedHandler;
        }
    }

    public ISelectable TryGetSelectableOfTypeAtPosition<T>(Vector2 position) where T : ISelectable
    {
        ISelectable selectedObject = TryGetSelectableObjectAtPosition(position);
        if (selectedObject != null && selectedObject is T)
        {
            return selectedObject;
        }

        return null;
    }

    // source: https://answers.unity.com/questions/967170/detect-if-pointer-is-over-any-ui-element.html
    public bool IsPointerOverUI(Vector2 screenPosition)
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = screenPosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);

        return results.Count > 0;
    }

    public void NotifyMapNodeSelected(MapNode node)
    {
        if(LastSelectedNode?.IDEqual(node.ID) == false)
        {
            LastSelectedNode?.SetSelected(false);
        }

        node.SetSelected(true);
        LastSelectedNode = node;

        OnMapNodeSelected(node);

        // Wylaczenia deselekcji na podwojnym kliku.
        //if (LastSelectedNode == null || LastSelectedNode.IDEqual(node.ID) == false)
        //{
        //    node.SetSelected(true);
        //    LastSelectedNode = node;

        //    OnMapNodeSelected(node);
        //}
        //else
        //{
        //    LastSelectedNode = null;
        //    OnMapNodeDeselected(node);
        //}
    }

    public void LoadContent()
    {
        
    }

    public void UnloadContent()
    {
        LastSelectedNode = null;
    }

    private void NotifyMouseLeftClickEmptySpace(Vector2 position)
    {
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(position);
        OnMapClick(worldMousePosition);
    }

    private void HandleMouseLeftClick(Vector2 position)
    {
        if (IsPointerOverUI(position) == false)
        {
            ISelectable selectedObject = TryGetSelectableObjectAtPosition(position);
            if(selectedObject != null)
            {
                CheckSelectedObject(selectedObject);
            }
            else
            {
                NotifyMouseLeftClickEmptySpace(position);
            }
        }
    }

    private void CheckSelectedObject(ISelectable selectable)
    {
        Type selectedType = selectable.GetSelectedType();

        // Kazdy inny typ trzeba dodac nizej. 
        // Mozna lepiej, obsluzyc tutaj ogolnie a obsluga w klasach rozszerzonych przez ISelectable. `FB.
        if(selectedType == typeof(MapNodeVisualization))
        {
            NotifyMapNodeSelected(selectable.GetSelectedObject() as MapNode);
        }
    }

    private ISelectable TryGetSelectableObjectAtPosition(Vector2 position)
    {
        ISelectable selectedObject = null;

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(position), Vector2.zero);
        if (hit.collider != null)
        {
            selectedObject = hit.collider.gameObject.GetComponent<ISelectable>();
        }

        return selectedObject;
    }

    private void OnMapNodeDeletedHandler()
    {
        LastSelectedNode = null;
    }

    #endregion

    #region Enums


    #endregion
}
