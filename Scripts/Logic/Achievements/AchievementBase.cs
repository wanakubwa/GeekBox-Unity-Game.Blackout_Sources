using System;

namespace GeekBox.Achievements
{
    [Serializable]
    public abstract class AchievementBase
    {
        #region Fields



        #endregion

        #region Propeties

        private bool IsUnlocked { get; set; } = false;

        #endregion

        #region Methods

        // Template.
        /// <summary>
        /// Uzywane do inicjalizacji na poczatku istnienia managera.
        /// </summary>
        public virtual void Init()
        {

        }

        /// <summary>
        /// Trigger w momencie, gdy manager obslugujacy otrzyma wywolanie subskrypcji eventow.
        /// </summary>
        public virtual void AttachEvents()
        {
            ScenariosManager.Instance.OnGameScenarioLoaded += AttachGameplayEvents;
        }

        public virtual void DetachEvents()
        {
            if(ScenariosManager.Instance != null)
            {
                ScenariosManager.Instance.OnGameScenarioLoaded -= AttachGameplayEvents;
            }
        }

        /// <summary>
        /// Trigger w momencie, gdy zaladowana zostanie scnena z gra. Tutaj mozna subskrybowac eventy zwiazane z aktualnym stanem gry na scenie.
        /// </summary>
        protected virtual void AttachGameplayEvents()
        {

        }

        protected virtual void DetachGameplayEvents()
        {

        }

        /// <summary>
        /// Udblokowanie osiagniecia o zadanej nazwie.
        /// </summary>
        /// <param name="name">Nazwa klucza osiagniecia</param>
        protected void UnlockAchivement(string name)
        {
            if(IsUnlocked == false)
            {
                GeekBox.Ads.EasyMobileManager.Instance.UnlockAchievementByName(name);
            }
        }

        #endregion

        #region Enums



        #endregion
    }
}
