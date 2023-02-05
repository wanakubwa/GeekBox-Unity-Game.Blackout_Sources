using MEC;
using System.Collections.Generic;

public abstract class ScenarioSetupBaseView<T> : PopUpView
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public override void CustomStart()
    {
        base.CustomStart();

        T currentTarget = GetModel<ScenarioSetupBaseModel<T>>().Target;
        if (currentTarget != null)
        {
            Timing.RunCoroutine(_WaitOneFrameAndRefreshView(currentTarget));
        }
    }

    private IEnumerator<float> _WaitOneFrameAndRefreshView(T target)
    {
        yield return Timing.WaitForOneFrame;

        RefreshView(target);
    }

    public abstract void RefreshView(T target);

    #endregion

    #region Enums



    #endregion
}
