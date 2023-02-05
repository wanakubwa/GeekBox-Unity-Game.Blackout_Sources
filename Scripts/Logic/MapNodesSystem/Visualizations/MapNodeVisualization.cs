using UnityEngine;
using System;
using TMPro;
using Sirenix.OdinInspector;

public class MapNodeVisualization : MonoBehaviour, ISelectable, IChargeCollidable
{
    #region Fields

    [SerializeField]
    private SpriteRenderer familyColor;
    [SerializeField]
    private TextMeshPro nodeValueText;
    [SerializeField]
    private GameObject selectionVisualization;
    [SerializeField]
    private ShieldsVisualization shields;

    [SerializeField]
    private NodeAnimationController animation;

    #endregion

    #region Propeties

    public SpriteRenderer FamilyColor { 
        get => familyColor; 
    }

    public MapNode NodeReference
    {
        get;
        private set;
    }

    public NodeModeVisualizationBase CurrentModeVisualization
    {
        get;
        set;
    }

    public ParentSettings CurrentSettings
    {
        get;
        set;
    }

    public TextMeshPro NodeValueText { get => nodeValueText; }
    public GameObject SelectionVisualization { get => selectionVisualization; }
    public ShieldsVisualization Shields { get => shields; }
    private NodeAnimationController Animation { get => animation; }

    #endregion

    #region Methods

    public void SetMapPosition(Vector2 mapPosition)
    {
        transform.position = new Vector3(mapPosition.x, mapPosition.y, Constants.DEFAULT_NODE_Z_POSITION);
    }

    public void SetModeVisualization(NodeModeVisualizationBase modeVisualization)
    {
        if(CurrentModeVisualization != null)
        {
            CurrentModeVisualization.RemoveVisualization(this);
        }

        CurrentModeVisualization = modeVisualization.SpawnVisualization(this);
        CurrentModeVisualization.Apply(this);
    }

    public void SetSelectionVisualization(bool isSelected)
    {
        if(isSelected == true && SelectionVisualization.activeInHierarchy == false)
        {
            Animation.PlaySelectedAnimation();
        }

        SelectionVisualization.SetActive(isSelected);
    }

    public Type GetSelectedType()
    {
        return typeof(MapNodeVisualization);
    }

    public object GetSelectedObject()
    {
        return NodeReference;
    }

    public void SetNodeReference(MapNode node)
    {
        NodeReference = node;
    }

    public void ResetPosition(Transform parent, Vector3 wordPosition)
    {
        transform.ResetParent(parent);
        transform.position = new Vector3(wordPosition.x, wordPosition.y, Constants.DEFAULT_NODE_Z_POSITION);
    }

    public void RefreshVisualization(ParentSettings settings)
    {
        CurrentSettings = settings;
        FamilyColor.color = settings.MainColor;
        NodeValueText.color = settings.FontColor;
        Shields.SetVisualizationColor(settings.ShieldColor);
        Animation.PlayRetakeAnimation(settings.MainColor);

        RefreshModeVisualization();
    }

    public void RefreshChargeValue(int value)
    {
        NodeValueText.text = value.ToStringKiloFormat();
    }

    public void RefreshShieldsValue(int value)
    {
        Shields.SetShieldsAmmount(value);
    }

    public void Collide(int charge, int senderParentId)
    {
        NodeReference.CheckChargeCollide(charge, senderParentId);
    }

    private void RefreshModeVisualization()
    {
        if(CurrentModeVisualization != null)
        {
            CurrentModeVisualization.RefreshModeVisualization(this);
        }
    }

    public int GetID()
    {
        return NodeReference.ID;
    }

    private void Awake()
    {
        SelectionVisualization.SetActive(false);
    }

    #endregion

    #region Enums



    #endregion
}
