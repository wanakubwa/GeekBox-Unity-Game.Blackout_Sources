using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class BlackHoleAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TotalStarsCount {get; set;}
        private int TotalUpgradesCount { get; set; }
        private PlayerData.PlayerWallet Wallet { get; set; }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();

            Wallet = PlayerManager.Instance.Wallet;
            TotalStarsCount = ScenariosContentSettings.Instance.ScenariosCollection.Count * ScenarioDataManager.MAX_STARS_REWARD;
            TotalUpgradesCount = TechnologyTreeManager.Instance.GetAllUpgrades().Count;
        }

        public override void AttachEvents()
        {
            base.AttachEvents();

            Wallet.OnFinishedLvlsUpdate += OnFinishLvl;
            Wallet.OnUnlockedUpgrade += OnBuyUpgrade;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            if (Wallet != null)
            {
                Wallet.OnFinishedLvlsUpdate -= OnFinishLvl;
                Wallet.OnUnlockedUpgrade -= OnBuyUpgrade;
            }
        }

        private void CheckAchievement()
        {
            int currentStars = Wallet.GetPlayerTotalStars();
            int currentUpgrades = Wallet.UnlockedUpgradesIds.Count;
            if (currentStars >= TotalStarsCount && currentUpgrades >= TotalUpgradesCount)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_BLACK_HOLE);
            }
        }

        // Handlers.
        private void OnFinishLvl()
        {
            CheckAchievement();
        }

        private void OnBuyUpgrade(int id)
        {
            CheckAchievement();
        }

        #endregion

        #region Enums



        #endregion
    }
}
