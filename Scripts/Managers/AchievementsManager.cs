using GeekBox.Achievements;
using Sirenix.Serialization;
using UnityEngine;

public class AchievementsManager : ManagerSingletonBase<AchievementsManager>
{

    #region Fields

    #endregion

    #region Propeties

    // Bardzo zly Hack - w nowym unity mamy serializeReference, ktory umozliwilby wystawienie tego do inspektora.
    private AchievementBase[] AchivementsCollection
    {
        get;
        set;
    } = new AchievementBase[]
    {
        new RedSupernovaAchievement(50),
        new LightSpeedAchievement(),
        new AstronomerAchievement(20),
        new BlackHoleAchievement(),
        new StarrySkyAchievement(20),
        new MasterOfUniverseAchievement(),
        new FallingStarAchievement(5),
        new BeginningMilkyWayAchievement(5),
        new GiantAchievement(),
        new SuperStarAchievement(100),
        new BigBangAchievement(),
        new SuccessAchievement(10_000),
        new TimeUpAchievement(900_000)
    };

    #endregion

    #region Methods

    protected override void Awake()
    {
        base.Awake();
        for (int i = 0; i < AchivementsCollection.Length; i++)
        {
            AchivementsCollection[i].Init();
        }
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        for(int i =0; i < AchivementsCollection.Length; i++)
        {
            AchivementsCollection[i].AttachEvents();
        }
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        for (int i = 0; i < AchivementsCollection.Length; i++)
        {
            AchivementsCollection[i].DetachEvents();
        }
    }

    #endregion

    #region Enums



    #endregion
}
