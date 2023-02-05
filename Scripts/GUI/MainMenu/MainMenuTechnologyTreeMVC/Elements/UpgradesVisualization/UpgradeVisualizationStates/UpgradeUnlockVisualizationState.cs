using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradeUnlockVisualizationState : UpgradeVisualizationState
    {
        #region Fields



        #endregion

        #region Propeties

        public override Color StateIconColor => Constants.RED_COLOR;

        #endregion

        #region Methods

        public override void SetupIcon()
        {
            Context.SetIcon(Context.UpgradeIcon);
            Context.SetBackgroundSprite(Context.BlackBacgroundSprite);
            Context.SetIconColor(StateIconColor);
        }

        public override void ShowInfo(UpgradeInfoBox infoBox)
        {
            infoBox.Show();
            infoBox.SetInfo(
                Context.UpgradeIcon,
                StateIconColor,
                Context.CurrentUpgradeNode.NameKey.Localize().SetColor(StateIconColor),
                Context.CurrentUpgradeNode.GetEffectsInfo(),
                Constants.LOC_UPGRADE_UNLOCKED.Localize().SetColor(StateIconColor)
                );
        }

        public override bool TrySwicthState()
        {
            // Jezeli gracz ma wiecej niz 0 punktow upgradow znaczy, ze stac go na zakup min 1 upgradu.
            if (PlayerManager.Instance.Wallet.UpgradePointsAmmount > Constants.DEFAULT_VALUE)
            {
                Context.SetBehaviorState(new UpgradeAvaibleVisualizationState());
                return true;
            }

            return false;
        }

        #endregion

        #region Enums



        #endregion
    }
}

