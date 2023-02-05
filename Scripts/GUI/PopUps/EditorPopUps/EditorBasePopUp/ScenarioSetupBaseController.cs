using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ScenarioSetupBaseController<T> : PopUpController
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public void RefreshPopUp(T target)
    {
        GetModel<ScenarioSetupBaseModel<T>>().SetTarget(target);
    }

    public void SaveChanges()
    {
        GetModel<ScenarioSetupBaseModel<T>>().SaveTarget();
    }

    public void SaveAndQuit()
    {
        GetModel<ScenarioSetupBaseModel<T>>().SaveTarget();
        ClosePopUp();
    }

    #endregion

    #region Enums



    #endregion
}
