using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class BeginningMilkyWayAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TargetFinishLvlCount { get; set; }
        private PlayerData.PlayerWallet Wallet { get; set; }

        #endregion

        #region Methods

        public BeginningMilkyWayAchievement(int finishLvlCount)
        {
            TargetFinishLvlCount = finishLvlCount;
        }

        public override void Init()
        {
            base.Init();

            Wallet = PlayerManager.Instance.Wallet;
        }

        public override void AttachEvents()
        {
            base.AttachEvents();

            Wallet.OnFinishedLvlsUpdate += OnFinishLvl;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            if (Wallet != null)
            {
                Wallet.OnFinishedLvlsUpdate -= OnFinishLvl;
            }
        }

        private void CheckAchievement()
        {
            if(Wallet.FinishedLvls.Count >= TargetFinishLvlCount)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_BEGINNING);
            }
        }

        // Handlers.
        private void OnFinishLvl()
        {
            CheckAchievement();
        }

        #endregion

        #region Enums



        #endregion
    }
}
