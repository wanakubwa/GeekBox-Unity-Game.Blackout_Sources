using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class AstronomerAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TargetValue {get; set;}
        private PlayerData.PlayerWallet Wallet { get; set; }

        #endregion

        #region Methods

        public AstronomerAchievement(int targetValue)
        {
            TargetValue = targetValue;
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

        private void CheckAchievement(int currentStars)
        {
            if(currentStars >= TargetValue)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_ASTRONOMER);
            }
        }

        // Handlers.
        private void OnFinishLvl()
        {
            CheckAchievement(Wallet.GetPlayerTotalStars());
        }

        #endregion

        #region Enums



        #endregion
    }
}
