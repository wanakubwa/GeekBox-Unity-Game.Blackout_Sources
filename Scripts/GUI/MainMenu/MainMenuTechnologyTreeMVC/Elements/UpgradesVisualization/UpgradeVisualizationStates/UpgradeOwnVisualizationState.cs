using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradeOwnVisualizationState : UpgradeVisualizationState
    {
        #region Fields



        #endregion

        #region Propeties

        public override Color StateIconColor => Color.black;

        #endregion

        #region Methods

        public override void SetupIcon()
        {
            Context.SetIcon(Context.UpgradeIcon);
            Context.SetBackgroundSprite(Context.WhiteBackgroundSprite);
            Context.SetIconColor(StateIconColor);
        }

        public override void ShowInfo(UpgradeInfoBox infoBox)
        {
            infoBox.Show();
            infoBox.SetInfo(
                Context.UpgradeIcon,
                StateIconColor,
                Context.CurrentUpgradeNode.NameKey.Localize(),
                Context.CurrentUpgradeNode.GetEffectsInfo(),
                Constants.LOC_UPGRADE_OWNED.Localize()
                );
        }

        public override bool TrySwicthState()
        {
            return false;
        }

        #endregion

        #region Enums



        #endregion
    }
}
