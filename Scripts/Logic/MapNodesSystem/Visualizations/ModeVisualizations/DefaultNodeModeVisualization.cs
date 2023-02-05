using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class DefaultNodeModeVisualization : NodeModeVisualizationBase
{
    #region Fields

    [SerializeField]
    private Material backgroundMaterial;

    #endregion

    #region Propeties

    public Material BackgroundMaterial { 
        get => backgroundMaterial; 
        private set => backgroundMaterial = value; 
    }

    #endregion

    #region Methods

    public override void RefreshModeVisualization(MapNodeVisualization root)
    {
        base.RefreshModeVisualization(root);

        SetMaterialMaterialProperties(GetComponent<SpriteRenderer>().material, root.CurrentSettings);
    }

    public override NodeModeVisualizationBase SpawnVisualization(MapNodeVisualization root)
    {
        NodeModeVisualizationBase currentVisualization = base.SpawnVisualization(root);
        Material mainMaterial = new Material(BackgroundMaterial);
        SetMaterialMaterialProperties(mainMaterial, root.CurrentSettings);
        currentVisualization.GetComponent<SpriteRenderer>().material = mainMaterial;

        return currentVisualization;
    }

    private void SetMaterialMaterialProperties(Material material, ParentSettings settings)
    {
        material.SetColor("_BackgroundColor", settings.MainColor);
    }

    #endregion

    #region Enums



    #endregion
}
