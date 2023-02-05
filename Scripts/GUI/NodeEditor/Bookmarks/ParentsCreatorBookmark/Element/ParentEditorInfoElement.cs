using UnityEngine;
using System.Collections;
using TMPro;

public class ParentEditorInfoElement : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TextMeshProUGUI parentName;

    #endregion

    #region Propeties

    public TextMeshProUGUI ParentName { 
        get => parentName; 
        private set => parentName = value; 
    }

    public NodeParent CachedParent { get; private set; }

    #endregion

    #region Methods

    public void SetInfo(NodeParent currentParent)
    {
        CachedParent = currentParent;
        SetParentName(CachedParent.ID.ToString());
    }

    public void SetParentName(string name)
    {
        ParentName.SetText(name);
    }

    #endregion

    #region Enums



    #endregion
}
