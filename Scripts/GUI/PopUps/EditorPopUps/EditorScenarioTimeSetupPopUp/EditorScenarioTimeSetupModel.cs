using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class EditorScenarioTimeSetupModel : ScenarioSetupBaseModel<ScenarioDataManager>
{
    #region Fields



    #endregion

    #region Propeties



    #endregion

    #region Methods

    public override void SaveTarget()
    {
        EditorScenarioTimeSetupView currentView = GetView<EditorScenarioTimeSetupView>();

        Target.OneStarRewardData.SetTimeThreshold(currentView.OneStarRewardThresholdMs.text.ParseToFloat());
        Target.TwoStarRewardData.SetTimeThreshold(currentView.TwoStarRewardThresholdMs.text.ParseToFloat());
        Target.ThreeStarRewardData.SetTimeThreshold(currentView.ThreeStarRewardThresholdMs.text.ParseToFloat());
        Target.SetDevTime(currentView.DevTimeMs.text.ParseToFloat());
    }

    #endregion

    #region Enums



    #endregion
}
