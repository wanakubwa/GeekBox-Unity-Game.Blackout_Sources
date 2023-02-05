using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class FallingStarAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TargetStrike { get; set; }

        private int LastLvlId { get; set; } = Constants.DEFAULT_ID;
        private int LooseStrikeCount { get; set; }
        private GameEventsManager GameEventsCache { get; set; }
        private ScenariosManager ScenariosManagerCache { get; set; }

        #endregion

        #region Methods

        public FallingStarAchievement(int targetStrike)
        {
            TargetStrike = targetStrike;
        }

        public override void Init()
        {
            base.Init();

            GameEventsCache = GameEventsManager.Instance;
            ScenariosManagerCache = ScenariosManager.Instance;
        }

        public override void AttachEvents()
        {
            base.AttachEvents();

            GameEventsCache.OnPlayerLooseScenario += OnLooseLvl;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            if (GameEventsCache != null)
            {
                GameEventsCache.OnPlayerLooseScenario -= OnLooseLvl;
            }
        }

        private void CheckAchievement()
        {
            if(LooseStrikeCount >= TargetStrike)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_FALLING_STAR);
            }
        }

        // Handlers.
        private void OnLooseLvl()
        {
            if(ScenariosManagerCache.CurrentScenarioInfo.ID == LastLvlId)
            {
                LooseStrikeCount++;
                CheckAchievement();
            }
            else
            {
                LooseStrikeCount = Constants.DEFAULT_VALUE;
                LastLvlId = ScenariosManagerCache.CurrentScenarioInfo.ID;
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
