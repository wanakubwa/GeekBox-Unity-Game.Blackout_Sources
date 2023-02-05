using UnityEngine;

public class SendChargeTest : MonoBehaviour
{
    #region Fields

    // DEBUGING ONLY AND TESTS. !!!!!!!!!!
    [SerializeField]
    private MapNode firstNode;
    [SerializeField]
    private MapNode secondNode;

    #endregion

    #region Propeties



    #endregion

    #region Methods

    private void Start()
    {
        OnMapNodeDeselected(null);
    }

    private void OnEnable()
    {
        SelectingManager.Instance.OnMapNodeSelected += OnMapNodeSelected;
        SelectingManager.Instance.OnMapNodeDeselected += OnMapNodeDeselected;
    }

    private void OnDisable()
    {
        if(SelectingManager.Instance != null)
        {
            SelectingManager.Instance.OnMapNodeSelected -= OnMapNodeSelected;
            SelectingManager.Instance.OnMapNodeDeselected -= OnMapNodeDeselected;
        }
    }

    private void OnMapNodeSelected(MapNode node)
    {
        if((firstNode == null || firstNode.ID == Constants.DEFAULT_ID) && node.CachedParentReference.IDEqual(Constants.NODE_PLAYER_PARENT_ID) == true)
        {
            firstNode = node;
            return;
        }
        
        if(firstNode != null && firstNode.IDEqual(node.ID) == false)
        {
            secondNode = node;
            MapManager.Instance.SendChargeBetweenNodes(firstNode, secondNode);
            firstNode = null;
            secondNode = null;
        }
    }

    private void OnMapNodeDeselected(MapNode node)
    {
        firstNode = null;
        secondNode = null;
    }

        #endregion

        #region Enums



        #endregion
    }
