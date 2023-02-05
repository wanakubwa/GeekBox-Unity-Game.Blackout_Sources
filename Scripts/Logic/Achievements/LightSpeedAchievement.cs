using System;
using System.Collections.Generic;

namespace GeekBox.Achievements
{
    [Serializable]
    public class LightSpeedAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private TechnologyTreeManager TechnologyTree { get; set; }

        private int SpeedUpgradesCount { get; set; }
        private int CurrentPlayerSpeedUpgrades { get; set; }

        #endregion

        #region Methods

        public override void Init()
        {
            base.Init();

            TechnologyTree = TechnologyTreeManager.Instance;

            SpeedUpgradesCount = GetSpeedUpgradesCount(TechnologyTree.GetAllUpgrades());
            CurrentPlayerSpeedUpgrades = GetCurrentPlayerSpeedUpgrades();
            CheckUnlockAchivement();
        }

        public override void AttachEvents()
        {
            base.AttachEvents();

            TechnologyTree.OnBuyUpgrade += OnPlayerBuyUpgrade;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            if(TechnologyTree != null)
            {
                TechnologyTree.OnBuyUpgrade -= OnPlayerBuyUpgrade;
            }
        }

        private int GetCurrentPlayerSpeedUpgrades()
        {
            List<UpgradeNode> upgrades = new List<UpgradeNode>();
            List<int> playerUpgradesIds = PlayerManager.Instance.Wallet.UnlockedUpgradesIds;
            foreach (int id in playerUpgradesIds)
            {
                UpgradeNode node = TechnologyTree.GetUpgradeById(id);
                if(node != null)
                {
                    upgrades.Add(node);
                }
            }

            return GetSpeedUpgradesCount(upgrades);
        }

        private int GetSpeedUpgradesCount(List<UpgradeNode> upgrades)
        {
            int result = 0;
            foreach (var upgrade in upgrades)
            {
                upgrade.Effects.ForEach((x) => {

                    if(x is ChangeSpeedPercentUpgradeEffect)
                    {
                        result++;
                    }

                });
            }

            return result;
        }

        private void OnPlayerBuyUpgrade(UpgradeNode upgrade)
        {
            foreach (var effect in upgrade.Effects)
            {
                if(effect is ChangeSpeedPercentUpgradeEffect)
                {
                    CurrentPlayerSpeedUpgrades++;
                    CheckUnlockAchivement();
                }
            }
        }

        private void CheckUnlockAchivement()
        {
            if(CurrentPlayerSpeedUpgrades == SpeedUpgradesCount)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_LIGHT_SPEED);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
