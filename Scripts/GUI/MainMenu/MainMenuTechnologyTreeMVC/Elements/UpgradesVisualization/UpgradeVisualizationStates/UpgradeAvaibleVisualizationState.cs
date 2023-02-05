using MEC;
using System.Collections.Generic;
using UnityEngine;

namespace UpgradesGUI
{
    public class UpgradeAvaibleVisualizationState : UpgradeVisualizationState
    {
        #region Fields



        #endregion

        #region Propeties

        private CoroutineHandle BuyDelayCoroutineHandle {
            get;
            set;
        }

        #endregion

        #region Methods

        public override void OnHold(UpgradeInfoBox infoBox)
        {
            base.OnHold(infoBox);

            infoBox.SetBuyStatus(true);
            Timing.KillCoroutines(BuyDelayCoroutineHandle);
            BuyDelayCoroutineHandle = Timing.RunCoroutine(_StartBuyDelayCounterCoroutine(infoBox, Constants.BUY_UPGRADE_WAIT_TIME_S));
        }

        public override void OnRelease(UpgradeInfoBox infoBox)
        {
            base.OnRelease(infoBox);

            Timing.KillCoroutines(BuyDelayCoroutineHandle);
            infoBox.SetBuyStatus(false);
        }

        public override void SetupIcon()
        {
            Context.SetIcon(Context.UpgradeIcon);
            Context.SetBackgroundSprite(Context.BlackBacgroundSprite);
            Context.SetIconColor(StateIconColor);
        }

        public override void ShowInfo(UpgradeInfoBox infoBox)
        {
            infoBox.Show();
            infoBox.SetInfo(Context.UpgradeIcon, 
                Color.black, 
                Context.CurrentUpgradeNode.NameKey.Localize(), 
                Context.CurrentUpgradeNode.GetEffectsInfo(),
                Constants.LOC_UPGRADE_HOLD_TO_BUY.Localize()
                );
        }

        public override bool TrySwicthState()
        {
            if (PlayerManager.Instance.Wallet.HasPlayerUpgrade(Context.CurrentUpgradeNode.UpgradeId) == true)
            {
                Context.SetBehaviorState(new UpgradeOwnVisualizationState());
                return true;
            }
            else if (PlayerManager.Instance.Wallet.UpgradePointsAmmount == Constants.DEFAULT_VALUE)
            {
                Context.SetBehaviorState(new UpgradeUnlockVisualizationState());
                return true;
            }

            return false;
        }

        private void BuyCurrentUpgrade()
        {
            Context.HandleBuyUpgrade();
        }

        private IEnumerator<float> _StartBuyDelayCounterCoroutine(UpgradeInfoBox infoBox, float delayToBuyS)
        {
            float currentDelayTimer = Constants.DEFAULT_VALUE;
            infoBox.SetProgress(currentDelayTimer);
            while (currentDelayTimer < delayToBuyS)
            {
                infoBox.SetProgress(currentDelayTimer / delayToBuyS);
                currentDelayTimer += MEC.Timing.DeltaTime;

                yield return MEC.Timing.WaitForOneFrame;
            }

            infoBox.SetBuyStatus(false);
            BuyCurrentUpgrade();
        }

        #endregion

        #region Enums



        #endregion
    }
}
