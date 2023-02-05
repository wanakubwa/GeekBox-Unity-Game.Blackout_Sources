using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

[Serializable]
public class NodeEditPanel : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TMP_Dropdown parentsDropdown;
    [SerializeField]
    private TextMeshProUGUI nodeId;
    [SerializeField]
    private TMP_Dropdown modesDropdown;
    [SerializeField]
    private TMP_InputField mapPositionX;
    [SerializeField]
    private TMP_InputField mapPositionY;

    #endregion

    #region Propeties

    public TMP_Dropdown ParentsDropdown { 
        get => parentsDropdown; 
        private set => parentsDropdown = value; 
    }

    public TextMeshProUGUI NodeId { 
        get => nodeId; 
    }

    public TMP_Dropdown ModesDropdown
    {
        get => modesDropdown;
    }

    private MapNode CachedNode
    {
        get;
        set;
    }

    public TMP_InputField MapPositionX { 
        get => mapPositionX; 
        private set => mapPositionX = value; 
    }

    public TMP_InputField MapPositionY { 
        get => mapPositionY; 
        private set => mapPositionY = value; 
    }

    #endregion

    #region Methods

    public void RefreshData(MapNode node)
    {
        CachedNode = node;
        RefreshParentsDropdown();
        RefreshModesDropdown();
        SetNodeId(CachedNode.NodeId);
        SetMapPositionText(CachedNode.MapPostion);
    }

    public void SetDefaultData()
    {
        CachedNode = null;
        ParentsDropdown.ClearOptions();
        SetNodeId(Constants.DEFAULT_ID);
    }

    public void SaveChanges()
    {
        if(CachedNode != null)
        {
            SaveParent();
            SaveNodeMode();
            SaveNodeMapPosition();
            SaveNodeCharge();
        }
    }

    public void OnNodeParentsDropdownValueChanged(int index)
    {
        NodeParent selectedParent = GetDropdownParent(ParentsDropdown.value);
        if(selectedParent == null)
        {
            Debug.Log("Blad wyboru tego rodzica rodzica!".SetColor(Color.red));
            return;
        }

        // Noda nie nalezala poprzednio do rodzica ale teraz nalezy.
        if (CachedNode.ParentId == Constants.NODE_NEUTRAL_PARENT_ID)
        {
            ModesDropdown.SetValueWithoutNotify(NodeContentSettings.Instance.GetNodeModeIndex(NodeModeType.NORMAL));
        }
        else
        {
            // Noda byla od rodzica ale staje sie neutralna.
            if(selectedParent.IDEqual(Constants.NODE_NEUTRAL_PARENT_ID) == true)
            {
                ModesDropdown.SetValueWithoutNotify(NodeContentSettings.Instance.GetNodeModeIndex(NodeModeType.DEFAULT));
            }
        }
    }

    private void RefreshModesDropdown()
    {
        string[] typesName = NodeContentSettings.Instance.GetAllProfilesType();
        ModesDropdown.ClearOptions();
        ModesDropdown.AddOptions(typesName.ToList());
        ModesDropdown.SetValueWithoutNotify(typesName.IndexOf(CachedNode.Values.ProfileModeType.ToString()));
    }

    private void RefreshParentsDropdown()
    {
        int[] parentsIds = ParentsManager.Instance.GetParentsIds();
        ParentsDropdown.ClearOptions();
        ParentsDropdown.AddOptions(parentsIds.ToStringArray().ToList());
        ParentsDropdown.SetValueWithoutNotify(parentsIds.IndexOf(CachedNode.ParentId));

        ParentsDropdown.onValueChanged.RemoveAllListeners();
        ParentsDropdown.onValueChanged.AddListener(OnNodeParentsDropdownValueChanged);
    }

    private void SetNodeId(int id)
    {
        NodeId.text = id.ToString();
    }

    private void SetMapPositionText(Vector3 position)
    {
        MapPositionX.text = position.x.ToString();
        MapPositionY.text = position.y.ToString();
    }

    private void SaveParent()
    {
        NodeParent currentParent = GetDropdownParent(ParentsDropdown.value);
        if (currentParent != null)
        {
            MapManager.Instance.ChangeNodeParent(CachedNode, currentParent);
        }
    }

    private NodeParent GetDropdownParent(int index)
    {
        int parentId = int.Parse(ParentsDropdown.options[index].text);
        return ParentsManager.Instance.ParentsCollection.GetElementByID(parentId);
    }

    private void SaveNodeMode()
    {
        NodeModeType modeType = (NodeModeType)Enum.Parse(typeof(NodeModeType), ModesDropdown.options[ModesDropdown.value].text);
        CachedNode.SetNodeModeProfile(NodeContentSettings.Instance.GetNodeProfileByModeType(modeType));
    }

    private void SaveNodeMapPosition()
    {
        Vector2 selectedPosition = new Vector2((int)MapPositionX.text.ParseToFloat(), (int)MapPositionY.text.ParseToFloat());
        CachedNode.SetMapPosition(selectedPosition);
    }

    private void SaveNodeCharge()
    {
        int charge = CachedNode.ParentId == Constants.NODE_NEUTRAL_PARENT_ID ? Constants.NODE_CREATE_DEFAULT_CHARGE : Constants.NODE_WITH_PARENT_DEFAULT_CHARGE;
        CachedNode.Values.SetChargeValue(charge);
    }

    #endregion

    #region Enums



    #endregion
}
