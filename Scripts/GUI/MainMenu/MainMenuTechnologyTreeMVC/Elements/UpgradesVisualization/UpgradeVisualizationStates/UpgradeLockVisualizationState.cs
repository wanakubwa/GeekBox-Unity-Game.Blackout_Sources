using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradeLockVisualizationState : UpgradeVisualizationState
    {
        #region Fields



        #endregion

        #region Propeties

        public override Color StateIconColor
        {
            get => Constants.RED_COLOR;
        }

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
                Constants.LOC_UPGRADE_LOCKED.Localize().SetColor(StateIconColor)
                );
        }

        public override bool TrySwicthState()
        {
            PlayerManager playerManager = PlayerManager.Instance;
            if (playerManager.Wallet.HasPlayerUpgrade(Context.CurrentUpgradeNode.ID) == true)
            {
                Context.SetBehaviorState(new UpgradeOwnVisualizationState());
                return true;
            }
            else if (Context.CurrentUpgradeNode.RequiredUpgradesIds.Count < 1 
                || playerManager.Wallet.HasPlayerAnyUpgrade(Context.CurrentUpgradeNode.RequiredUpgradesIds) == true)
            {
                Context.SetBehaviorState(new UpgradeUnlockVisualizationState());
                return true;
            }

            return false;
        }

        #endregion

        #region Enums



        #endregion
    }
}

