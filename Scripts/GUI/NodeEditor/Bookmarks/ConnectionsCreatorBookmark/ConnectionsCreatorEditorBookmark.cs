using UnityEngine;
using Sirenix.OdinInspector;
using UnityEngine.UI;
using System.Collections.Generic;

public class ConnectionsCreatorEditorBookmark : ScenarioEditorBookmarkBase
{
    #region Fields

    [SerializeField]
    private MapConnectionVisualization editorConnectionVisualizationPrefab;

    [Title("Toggles")]
    [SerializeField]
    private Toggle addModeToggle;
    [SerializeField]
    private Toggle removeModeToggle;
    [SerializeField]
    private Toggle editModeToggle;

    [Title("Extend elements")]
    [SerializeField]
    private ConnectionRemoveExtendElement connectionRemoveExtendPrefab;
    [SerializeField]
    private ConnectionExtendElementBase connectionEditExtendPrefab;

    [Title("Panels")]
    [SerializeField]
    private ConnectionEditPanel connectionEditPanel;

    #endregion

    #region Propeties

    public MapConnectionVisualization EditorConnectionVisualizationPrefab { get => editorConnectionVisualizationPrefab; }

    private MapConnectionVisualization CurrentConnectionVisualization { get; set; }

    public Toggle AddModeToggle { get => addModeToggle; }

    public Toggle RemoveModeToggle { get => removeModeToggle; }

    public Toggle EditModeToggle { get => editModeToggle;}

    private MapNode CachedFirstNode
    {
        get;
        set;
    }

    private ConnectionRemoveExtendElement ConnectionRemoveExtendPrefab { 
        get => connectionRemoveExtendPrefab; 
        set => connectionRemoveExtendPrefab = value; 
    }

    private ConnectionExtendElementBase ConnectionEditExtendPrefab
    {
        get => connectionEditExtendPrefab;
        set => connectionEditExtendPrefab = value;
    }

    private List<ConnectionRemoveExtendElement> SpawnedConnectionRemoveCollection
    {
        get;
        set;
    } = new List<ConnectionRemoveExtendElement>();

    private List<ConnectionExtendElementBase> SpawnedConnectionEditCollection
    {
        get;
        set;
    } = new List<ConnectionExtendElementBase>();

    private ConnectionEditPanel ConnectionEditPanel { get => connectionEditPanel; }

    #endregion

    #region Methods

    public override void InitializeBookmark()
    {
        base.InitializeBookmark();

        RemoveModeToggle.onValueChanged.AddListener(OnRemoveToggleChanged);
        EditModeToggle.onValueChanged.AddListener(OnEditToggleChanged);
        ConnectionEditPanel.Hide();
    }

    public override void ShrinkBookmark()
    {
        base.ShrinkBookmark();

        RemoveModeToggle.onValueChanged.RemoveAllListeners();
        EditModeToggle.onValueChanged.RemoveAllListeners();
        OnRemoveToggleChanged(false);
        OnEditToggleChanged(false);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        InputManager.Instance.OnMouseLeftUp += TrySetSecondNode;
        InputManager.Instance.OnMouseLeftHold += UpdateConnectionSecondPosition;
    }

    public override void DettachEvents()
    {
        base.DettachEvents();

        if(InputManager.Instance != null)
        {
            InputManager.Instance.OnMouseLeftUp -= TrySetSecondNode;
            InputManager.Instance.OnMouseLeftHold -= UpdateConnectionSecondPosition;
        }
    }

    public override void OnMapNodeClickHandle(MapNode selectedNode)
    {
        base.OnMapNodeClickHandle(selectedNode);

        if(AddModeToggle.isOn == true)
        {
            CameraManager.Instance.SetIsCameraMove(false);
            CachedFirstNode = selectedNode;
            InitializeNewConnection();
        }
    }

    private void OnRemoveToggleChanged(bool isOn)
    {
        if(isOn == true)
        {
            // Spawnowanie do kazdego polaczenia ikony do jego znieszczenia.
            List<MapConnection> connections = MapManager.Instance.MapConnectionsCollection;
            foreach (MapConnection connection in connections)
            {
                ConnectionRemoveExtendElement extendElement = Instantiate(ConnectionRemoveExtendPrefab);
                extendElement.transform.ResetParent(connection.ConnectionVisualization.transform);
                extendElement.Initialize(connection);

                SpawnedConnectionRemoveCollection.Add(extendElement);
            }
        }
        else
        {
            SpawnedConnectionRemoveCollection.ClearDestroy();
        }
    }

    private void OnEditToggleChanged(bool isOn)
    {
        if (isOn == true)
        {
            List<MapConnection> connections = MapManager.Instance.MapConnectionsCollection;
            foreach (MapConnection connection in connections)
            {
                ConnectionExtendElementBase extendElement = Instantiate(ConnectionEditExtendPrefab);
                extendElement.transform.ResetParent(connection.ConnectionVisualization.transform);
                extendElement.Initialize(connection, OnConnectionEditExtendClickedHandler);

                SpawnedConnectionEditCollection.Add(extendElement);
            }
        }
        else
        {
            SpawnedConnectionEditCollection.ClearDestroy();
        }
    }

    private void OnConnectionEditExtendClickedHandler(MapConnection targetConnection)
    {
        ConnectionEditPanel.RefresPanel(targetConnection);
        ConnectionEditPanel.Show();
    }

    private void InitializeNewConnection()
    {
        CurrentConnectionVisualization = Instantiate(EditorConnectionVisualizationPrefab);
        CurrentConnectionVisualization.SetFirstPosition(CachedFirstNode.MapPostion);
    }

    private void UpdateConnectionSecondPosition(Vector2 position)
    {
        if(CurrentConnectionVisualization != null)
        {
            CurrentConnectionVisualization.SetSecondPosition(Camera.main.ScreenToWorldPoint(position));
        }
    }

    private void TrySetSecondNode(Vector2 position)
    {
        if(CachedFirstNode == null || CurrentConnectionVisualization == null)
        {
            SetDefaultData();
            return;
        }

        ISelectable selectedNode = SelectingManager.Instance.TryGetSelectableOfTypeAtPosition<MapNodeVisualization>(position);
        if(selectedNode != null)
        {
            MapNode secondNode = (MapNode)selectedNode.GetSelectedObject();
            if(MapManager.Instance.CheckNodesHasConnection(CachedFirstNode, secondNode) == false && secondNode.IDEqual(CachedFirstNode.ID) == false)
            {
                MapManager.Instance.CreateNewMapConnection(CachedFirstNode, (MapNode)selectedNode.GetSelectedObject());
            }
        }

        SetDefaultData();
    }

    private void SetDefaultData()
    {
        CachedFirstNode = null;
        if(CurrentConnectionVisualization != null)
        {
            Destroy(CurrentConnectionVisualization.gameObject);
            CurrentConnectionVisualization = null;
        }

        CameraManager.Instance.SetIsCameraMove(true);
    }

    #endregion

    #region Enums



    #endregion
}
