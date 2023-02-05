using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeekBox.Achievements
{
    public class BigBangAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties



        #endregion

        #region Methods

        protected override void AttachGameplayEvents()
        {
            base.AttachGameplayEvents();

            InGameEvents.Instance.OnKamikazeEffectUsed += OnKamikazeEffectUdes;
        }

        protected override void DetachGameplayEvents()
        {
            base.DetachGameplayEvents();

            if(InGameEvents.Instance != null)
            {
                InGameEvents.Instance.OnKamikazeEffectUsed -= OnKamikazeEffectUdes;
            }
        }

        // Handlers.
        private void OnKamikazeEffectUdes(int senderId)
        {
            if(senderId == Constants.NODE_PLAYER_PARENT_ID)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_BIG_BANG);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
