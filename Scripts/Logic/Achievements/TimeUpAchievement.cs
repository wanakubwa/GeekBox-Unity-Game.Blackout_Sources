namespace GeekBox.Achievements
{
    public class TimeUpAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private float TargetTimeMS { get; set; }

        #endregion

        #region Methods

        public TimeUpAchievement(float targetTimeMS)
        {
            TargetTimeMS = targetTimeMS;
        }

        protected override void AttachGameplayEvents()
        {
            base.AttachGameplayEvents();

            TimeManager.Instance.OnGameTimeUpdate += OnGameTimeUpdateHandler;
        }

        protected override void DetachGameplayEvents()
        {
            base.DetachGameplayEvents();

            TimeManager.Instance.OnGameTimeUpdate -= OnGameTimeUpdateHandler;
        }

        // Handlers.
        private void OnGameTimeUpdateHandler(float timeMs)
        {
            if(timeMs >= TargetTimeMS)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_TIMES_UP);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
