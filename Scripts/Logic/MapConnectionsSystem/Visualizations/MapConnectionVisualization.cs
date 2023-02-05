using UnityEngine;
using System.Collections;
using Sirenix.OdinInspector;
using System;

public class MapConnectionVisualization : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private LineRenderer connectionLine;
    [SerializeField]
    private LineRenderer specialConnectionRenderer;
    [SerializeField]
    private ConnectionMaterial currentMaterial;

    [Title("Charge Settings")]
    [SerializeField]
    private ConnectionChargeVisualization chargeVisualizationPrefab;

    #endregion

    #region Propeties

    public LineRenderer ConnectionLine { get => connectionLine; }
    public ConnectionMaterial CurrentMaterial { get => currentMaterial; }
    public ConnectionChargeVisualization ChargeVisualizationPrefab { get => chargeVisualizationPrefab; }
    public LineRenderer SpecialConnectionRenderer { get => specialConnectionRenderer; }

    #endregion

    #region Methods

    public void Initialize(MapConnection connection)
    {
        CurrentMaterial.Initialize();
        RefreshVisualization(connection.FirstNodeReference, connection.SecondNodeReference);
        RefreshSpecialConnection(connection.IsSpecialConnection);
    }

    public void SetPositions(Vector3 first, Vector3 second)
    {
        SetFirstPosition(first);
        SetSecondPosition(second);
    }

    public void SetFirstPosition(Vector3 position)
    {
        ConnectionLine.SetPosition(0, position);
        SpecialConnectionRenderer.SetPosition(0, position);
    }

    public void SetSecondPosition(Vector2 position)
    {
        ConnectionLine.SetPosition(1, position);
        SpecialConnectionRenderer.SetPosition(1, position);
    }

    public void RefreshVisualization(MapNode firstNode, MapNode secondNode)
    {
        if (firstNode.ParentId == secondNode.ParentId && firstNode.ParentId != Constants.NODE_NEUTRAL_PARENT_ID)
        {
            CurrentMaterial.SetColor(firstNode.CachedParentReference.Settings.MainColor);
        }
        else
        {
            CurrentMaterial.SetDefaultColor();
        }
    }

    public ConnectionChargeVisualization GetChargeVisualization()
    {
        ConnectionChargeVisualization chargeVisualization = Instantiate(ChargeVisualizationPrefab);
        chargeVisualization.transform.ResetParent(transform);
        return chargeVisualization;
    }

    private void RefreshSpecialConnection(bool isSpecial)
    {
        SpecialConnectionRenderer.gameObject.SetActive(isSpecial);
        SpecialConnectionRenderer.SetPosition(0, ConnectionLine.GetPosition(0));
        SpecialConnectionRenderer.SetPosition(1, ConnectionLine.GetPosition(1));
    }

    #endregion

    #region Enums



    #endregion
}
