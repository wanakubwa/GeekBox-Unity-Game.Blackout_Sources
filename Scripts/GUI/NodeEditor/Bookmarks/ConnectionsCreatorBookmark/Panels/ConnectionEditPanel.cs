using UnityEngine;
using UnityEngine.UI;

public class ConnectionEditPanel : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Toggle isSpecialToggle;

    #endregion

    #region Propeties

    private Toggle IsSpecialToggle { 
        get => isSpecialToggle; 
    }

    private MapConnection CachedConnection
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public void RefresPanel(MapConnection targetConnection)
    {
        CachedConnection = targetConnection;
        SetIsSpecialToggle(targetConnection.IsSpecialConnection);
    }

    public void Hide()
    {
        CachedConnection = null;
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Save()
    {
        CachedConnection.SetIsSpecialConnection(IsSpecialToggle.isOn);
    }

    private void SetIsSpecialToggle(bool state)
    {
        IsSpecialToggle.SetIsOnWithoutNotify(state);
    }

    #endregion

    #region Enums



    #endregion
}
