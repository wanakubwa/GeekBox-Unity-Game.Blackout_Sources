using System;

[Serializable]
public class CameraManagerMemento : MementoBase
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public override void CreateMemento(IManager sourceManager)
    {
        CameraManager manager = sourceManager as CameraManager;

    }

    #endregion

    #region Enums



    #endregion
}
