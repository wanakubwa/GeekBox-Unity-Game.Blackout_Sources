using UnityEngine;
using System;

public class ConnectionRemoveExtendElement : MonoBehaviour
{
    #region Fields


    #endregion

    #region Propeties

    private MapConnection CurrentConnection
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public void Initialize(MapConnection connection)
    {
        CurrentConnection = connection;

        Vector3 firstPosition = connection.ConnectionVisualization.ConnectionLine.GetPosition(0);
        Vector3 secondPosition = connection.ConnectionVisualization.ConnectionLine.GetPosition(1);
        Vector3 direction = (firstPosition - secondPosition).normalized;

        Vector3 conectionCenterPosition = new Vector3(firstPosition.x - Mathf.Sign(direction.x) * (Math.Abs(firstPosition.x - secondPosition.x) * 0.5f), firstPosition.y - Mathf.Sign(direction.y) * (Math.Abs(firstPosition.y - secondPosition.y) * 0.5f), 5f);
        transform.position = conectionCenterPosition;
    }

    public void OnRemoveButtonClicked()
    {
        MapManager.Instance.DeleteMapConnection(CurrentConnection);
    }

    #endregion

    #region Enums



    #endregion
}
