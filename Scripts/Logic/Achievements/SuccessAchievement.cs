
namespace GeekBox.Achievements
{
    public class SuccessAchievement : AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private int TargetValue { get; set; }
        private PlayerData.PlayerWallet Wallet { get; set; }

        #endregion

        #region Methods

        public SuccessAchievement(int targetValue)
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

            Wallet.OnKPointsChanged += OnKPointsChangedHandler;
        }

        public override void DetachEvents()
        {
            base.DetachEvents();

            if(Wallet != null)
            {
                Wallet.OnKPointsChanged += OnKPointsChangedHandler;
            }
        }

        // Handlers.
        private void OnKPointsChangedHandler(int value)
        {
            if (Wallet.KPointsAmmountAllTheTime >= TargetValue)
            {
                UnlockAchivement(EasyMobile.EM_GameServicesConstants.Achievement_ACHIV_SUCCES);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
