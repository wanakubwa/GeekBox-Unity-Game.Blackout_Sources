using UnityEngine;
using System;
using System.Collections.Generic;

[Serializable]
[CreateAssetMenu(fileName = "NodeContentSettings.asset", menuName = "Settings/NodeContentSettings")]
public class NodeContentSettings : ScriptableObject
{
    #region Fields

    private static NodeContentSettings instance;

    [SerializeField]
    private NodeModeType defaultType = NodeModeType.DEFAULT;
    [SerializeField]
    private List<NodeProfileBase> nodeModeProfiles = new List<NodeProfileBase>();

    #endregion

    #region Propeties

    public static NodeContentSettings Instance
    {
        get
        {
            if (instance == null)
            {
                instance = Resources.Load<NodeContentSettings>("Settings/NodeContentSettings");
            }

            return instance;
        }
        set
        {
            instance = value;
        }
    }

    public List<NodeProfileBase> NodeModeProfiles { get => nodeModeProfiles; }
    public NodeModeType DefaultType { get => defaultType; }

    #endregion

    #region Methods

    public NodeProfileBase GetNodeProfileByModeType(NodeModeType modeType)
    {
        for(int i =0; i < NodeModeProfiles.Count; i++)
        {
            if(NodeModeProfiles[i].ModeType == modeType)
            {
                return NodeModeProfiles[i];
            }
        }

        return null;
    }

    public NodeProfileBase GetDefaultProfile()
    {
        return GetNodeProfileByModeType(DefaultType);
    }

    public string[] GetAllProfilesType()
    {
        string[] modesTypeName = new string[NodeModeProfiles.Count];
        for(int i =0; i < NodeModeProfiles.Count; i++)
        {
            modesTypeName[i] = NodeModeProfiles[i].ModeType.ToString();
        }

        return modesTypeName;
    }

    public int GetNodeModeIndex(NodeModeType modeType)
    {
        string[] typesName = GetAllProfilesType();
        return typesName.IndexOf(modeType.ToString());
    }

    #endregion

    #region Enums



    #endregion
}
