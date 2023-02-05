using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class ScenarioSetupBaseModel<T> : PopUpModel
{
    #region Fields



    #endregion

    #region Propeties

    public T Target
    {
        get;
        private set;
    }

    #endregion

    #region Methods

    public abstract void SaveTarget();

    public void SetTarget(T newTarget)
    {
        Target = newTarget;
    }

    #endregion

    #region Enums



    #endregion
}
