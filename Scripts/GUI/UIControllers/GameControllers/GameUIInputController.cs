using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine.Experimental.PlayerLoop;

public class GameUIInputController : MonoBehaviour
{
    #region Fields

    private const int FIRST_POSITION_INDEX = 0;
    private const int SECOND_POSITION_INDEX = 1;

    [Title("Input Visualization settings")]
    [SerializeField]
    private LineRenderer selectionVisualizationLinePrefab;

    #endregion

    #region Propeties

    public LineRenderer SelectionVisualizationLinePrefab { 
        get => selectionVisualizationLinePrefab; 
    }

    private MapNode FirstSelectedNode
    {
        get;
        set;
    }

    private MapNode SecondSelectedNode
    {
        get;
        set;
    }

    private LineRenderer SpawnedSelectionLineVisualization
    {
        get;
        set;
    }

    // Dependencies;
    private SelectingManager SelectingManagerInstance
    {
        get;
        set;
    }

    #endregion

    #region Methods

    private void Awake()
    {
        // Setup dependency;
        SelectingManagerInstance = SelectingManager.Instance;
    }

    private void Start()
    {
        SpawnedSelectionLineVisualization = Instantiate(SelectionVisualizationLinePrefab);
        SpawnedSelectionLineVisualization.transform.ResetParent(transform);

        OnMapNodeDeselected(null);
    }

    private void OnEnable()
    {
        if(SelectingManagerInstance != null)
        {
            SelectingManagerInstance.OnMapNodeSelected += OnMapNodeSelected;
            SelectingManagerInstance.OnMapNodeDeselected += OnMapNodeDeselected;
        }

        InputManager inputManager = InputManager.Instance;
        if (inputManager != null)
        {
            inputManager.OnMouseLeftHold += OnMouseLeftButtonHoldHandler;
            inputManager.OnMouseLeftUp += OnMouseLeftButtonUpHandler;
        }
    }

    private void OnDisable()
    {
        if (SelectingManagerInstance != null)
        {
            SelectingManagerInstance.OnMapNodeSelected -= OnMapNodeSelected;
            SelectingManagerInstance.OnMapNodeDeselected -= OnMapNodeDeselected;
        }

        if(InputManager.Instance != null)
        {
            InputManager.Instance.OnMouseLeftHold -= OnMouseLeftButtonHoldHandler;
            InputManager.Instance.OnMouseLeftUp -= OnMouseLeftButtonUpHandler;
        }
    }

    private void OnMapNodeSelected(MapNode node)
    {
        if ((FirstSelectedNode == null || FirstSelectedNode.ID == Constants.DEFAULT_ID) && node.CachedParentReference.IDEqual(Constants.NODE_PLAYER_PARENT_ID) == true)
        {
            FirstSelectedNode = node;
            SpawnedSelectionLineVisualization.gameObject.SetActive(true);
            SetVisualizationFirstPosition(FirstSelectedNode.MapPostion);
            AttachToNode(node);

            CameraManager.Instance.SetIsCameraMove(false);
            return;
        }
    }

    private void OnMouseLeftButtonHoldHandler(Vector2 screenPosition)
    {
        if(FirstSelectedNode != null)
        {
            UpdateVisualizationTargetPosition(Camera.main.ScreenToWorldPoint(screenPosition));
        }
    }

    private void OnMouseLeftButtonUpHandler(Vector2 screenPosition)
    {
        if(FirstSelectedNode == null)
        {
            return;
        }

        ISelectable node = SelectingManagerInstance.TryGetSelectableOfTypeAtPosition<MapNodeVisualization>(screenPosition);
        if (node != null && node.GetSelectedObject() is MapNode secondNode)
        {
            SecondSelectedNode = secondNode;
            TrySendChangeBetweenNodes();
            SelectingManagerInstance.NotifyMapNodeSelected(secondNode);
        }

        OnMapNodeDeselected(null);
    }

    private void TrySendChangeBetweenNodes()
    {
        if (FirstSelectedNode.IDEqual(SecondSelectedNode.ID) == false)
        {
            MapManager.Instance.SendChargeBetweenNodesWithFactor(FirstSelectedNode, SecondSelectedNode);
        }
    }

    private void OnMapNodeDeselected(MapNode node)
    {
        FirstSelectedNode = null;
        SecondSelectedNode = null;
        DetachFromNode(node);
        CameraManager.Instance.SetIsCameraMove(true);

        ResetSelectingLineVisualization();
    }

    private void SetVisualizationFirstPosition(Vector2 worldPosition)
    {
        SpawnedSelectionLineVisualization.SetPosition(FIRST_POSITION_INDEX, worldPosition);
        SpawnedSelectionLineVisualization.SetPosition(SECOND_POSITION_INDEX, worldPosition);
    }

    private void UpdateVisualizationTargetPosition(Vector2 worldPosition)
    {
        SpawnedSelectionLineVisualization.SetPosition(SECOND_POSITION_INDEX, worldPosition);
    }

    private void ResetSelectingLineVisualization()
    {
        SpawnedSelectionLineVisualization.SetPosition(FIRST_POSITION_INDEX, Vector3.zero);
        SpawnedSelectionLineVisualization.SetPosition(SECOND_POSITION_INDEX, Vector3.zero);
        SpawnedSelectionLineVisualization.gameObject.SetActive(false);
    }

    private void AttachToNode(MapNode node)
    {
        node.OnRetake += HandleNodeRetaken;
    }

    private void DetachFromNode(MapNode node)
    {
        if(node != null)
        {
            node.OnRetake -= HandleNodeRetaken;
        }
    }

    private void HandleNodeRetaken(MapNode node)
    {
        if(node.ParentId != Constants.NODE_PLAYER_PARENT_ID)
        {
            OnMapNodeDeselected(node);
        }
    }

    #endregion

    #region Enums



    #endregion
}
