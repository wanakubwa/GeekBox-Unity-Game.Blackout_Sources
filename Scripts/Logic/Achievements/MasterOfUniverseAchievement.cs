using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class MasterOfUniverseAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TotaleLvlsCount {get; set;}
        private PlayerData.PlayerWallet Wallet { get; set; }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();

            Wallet = PlayerManager.Instance.Wallet;
            TotaleLvlsCount = ScenariosManager.Instance.GetTotalScenariosCount();
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
            if(Wallet.FinishedLvls.Count >= TotaleLvlsCount)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_MASTER);
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
