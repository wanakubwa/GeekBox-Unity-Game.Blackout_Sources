using System;
using UnityEngine;

[Serializable]
public class NodeModeVisualizationBase : MonoBehaviour
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public virtual void RefreshModeVisualization(MapNodeVisualization root)
    {

    }

    public virtual NodeModeVisualizationBase SpawnVisualization(MapNodeVisualization root)
    {
        NodeModeVisualizationBase spawnedModeVisualization = Instantiate(this);
        spawnedModeVisualization.transform.ResetParent(root.transform);
        return spawnedModeVisualization;
    }

    public virtual void Apply(MapNodeVisualization currentVisualization)
    {

    }

    public virtual void RemoveVisualization(MapNodeVisualization currentVisualization)
    {
        Destroy(gameObject);
    }

    #endregion

    #region Enums



    #endregion
}
