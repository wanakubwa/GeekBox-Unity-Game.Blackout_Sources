using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class GiantAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TargetUpgradesCount { get; set; }
        private PlayerData.PlayerWallet Wallet { get; set; }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();

            Wallet = PlayerManager.Instance.Wallet;
            TargetUpgradesCount = Enum.GetNames(typeof(NodeModeType)).Length;
        }

        public override void AttachEvents()
        {
            base.AttachEvents();
            Wallet.OnUnlockMode += OnUnlockSpecial;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            if (Wallet != null)
            {
                Wallet.OnUnlockMode -= OnUnlockSpecial;
            }
        }

        private void CheckAchievement()
        {
            if (Wallet.UnlockedModes.Count >= TargetUpgradesCount)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_TO_GIANT);
            }
        }

        // Handlers.

        private void OnUnlockSpecial()
        {
            CheckAchievement();
        }

        #endregion

        #region Enums



        #endregion
    }
}
