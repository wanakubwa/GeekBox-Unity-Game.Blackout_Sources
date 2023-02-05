using AISystem;
using MEC;
using SaveLoadSystem;
using System.Collections.Generic;

public class AIManager : SingletonScenarioSaveableManager<AIManager, AIManagerMemento>, IContentLoadable
{
    #region Fields



    #endregion

    #region Propeties

    public AISettings Settings { 
        get; 
        private set; 
    } = new AISettings();

    private List<AIParentActorData> ParentsActorsCollection { 
        get; 
        set; 
    } = new List<AIParentActorData>();

    private CoroutineHandle CustomUpdateHandler
    {
        get;
        set;
    }

    #endregion

    #region Methods

    public void RegisterParentActor(IAIParentActor parentActor)
    {
        AIParentSettings actorSettings = Settings.GetParentAISettingsByParentId(parentActor.ID);
        AIParentActorData actorData = new AIParentActorData(parentActor.AIActor, actorSettings);
        ParentsActorsCollection.Add(actorData);
    }

    public void UnregisterParentActor(IAIParentActor parentActor)
    {
        ParentsActorsCollection.RemoveElementByID(parentActor.ID);
    }

    public override void CreateNewScenario()
    {
        Settings = new AISettings();
    }

    public override void LoadManager(AIManagerMemento memento)
    {
        Settings = new AISettings(memento.SettingsSave);
    }

    public void LoadContent()
    {
        
    }

    public void UnloadContent()
    {
        Timing.KillCoroutines(CustomUpdateHandler);
        ParentsActorsCollection.Clear();
        Settings = new AISettings();
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        ScenariosManager.Instance.OnGameScenarioLoaded += OnGameScenarioLoadedHandler;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        if (ScenariosManager.Instance != null)
        {
            ScenariosManager.Instance.OnGameScenarioLoaded -= OnGameScenarioLoadedHandler;
        }
    }

    private IEnumerator<float> _SlowUpdate()
    {
        yield return Timing.WaitForOneFrame;

        TimeManager timeManager = TimeManager.Instance;
        while (true)
        {
            if(timeManager.IsTimeCounting == true)
            {
                foreach (AIParentActorData aiActor in ParentsActorsCollection)
                {
                    aiActor.TryRefreshModul(Timing.DeltaTime + Timing.Instance.TimeBetweenSlowUpdateCalls);
                    yield return Timing.WaitForOneFrame;
                }
            }

            yield return Timing.WaitForOneFrame;
        }
    }

    // todo dodac atrybut.
    private void OnGameScenarioLoadedHandler()
    {
        CustomUpdateHandler = Timing.RunCoroutine(_SlowUpdate(), Segment.SlowUpdate);
    }

    #endregion

    #region Enums


    #endregion
}
