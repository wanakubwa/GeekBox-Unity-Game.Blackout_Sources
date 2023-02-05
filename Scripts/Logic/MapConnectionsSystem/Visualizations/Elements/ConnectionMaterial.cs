using Sirenix.OdinInspector;
using UnityEngine;

public class ConnectionMaterial : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Material defaultMaterialReference;
    [SerializeField]
    private LineRenderer specialLineRenderer;

    [Title("Colors settings")]
    [SerializeField]
    private float defaultColorAlfa;

    #endregion

    #region Propeties

    public Material DefaultMaterialReference { 
        get => defaultMaterialReference; 
    }

    private Material CurrentMaterial
    {
        get;
        set;
    }

    public float DefaultColorAlfa { 
        get => defaultColorAlfa; 
    }

    #endregion

    #region Methods

    public void Initialize()
    {
        LineRenderer currentRenderer = GetComponent<LineRenderer>();

        CurrentMaterial = new Material(DefaultMaterialReference);
        currentRenderer.material = CurrentMaterial;
    }

    public void SetDefaultColor()
    {
        Color defaultColor = ParentsContentSettings.Instance.GetNeutralParentColor().ParentColor;
        SetColor(defaultColor, DefaultColorAlfa);
    }

    public void SetColor(Color color, float alphaValue = 1f)
    {
        CurrentMaterial.SetColor("_LineColor", color);
        CurrentMaterial.SetFloat("_Alpha", alphaValue);
    }

    #endregion

    #region Enums



    #endregion
}
