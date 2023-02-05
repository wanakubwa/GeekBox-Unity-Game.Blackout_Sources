using GeekBox.UI;
using UnityEngine;

[RequireComponent(typeof(UILineRenderer))]
public class UpgradeUIEditorConnection : MonoBehaviour, ICleanable
{
    #region Fields

    #endregion

    #region Propeties

    private UILineRenderer Renderer { get; set; }

    private UpgradeUIEditorElement FirstUpgradeElement
    {
        get;
        set;
    }

    private UpgradeUIEditorElement SecondUpgradeElement
    {
        get;
        set;
    }

    #endregion

    #region Methods

    private void Awake()
    {
        Renderer = GetComponent<UILineRenderer>();
    }

    public void Initialize(UpgradeUIEditorElement firstElement, UpgradeUIEditorElement secondElement)
    {
        FirstUpgradeElement = firstElement;
        SecondUpgradeElement = secondElement;

        RefreshPosition();
    }

    public void RefreshPosition()
    {
        Renderer.SetPoint(0, FirstUpgradeElement.CachedUpgrade.EditorPosition);
        Renderer.SetPoint(1, SecondUpgradeElement.CachedUpgrade.EditorPosition);
    }

    public void CleanData()
    {
        FirstUpgradeElement.RemoveConnection(this);
        SecondUpgradeElement.RemoveConnection(this);
        Destroy(gameObject);
    }

    #endregion

    #region Enums



    #endregion
}
