using TutorialSystem;
using UnityEngine;

public class TutorialTriggerComponent : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private TutorialSystem.TutorialType targetTutorial;

    #endregion

    #region Propeties

    public TutorialType TargetTutorial { 
        get => targetTutorial;
    }

    #endregion

    #region Methods

    private void Start()
    {
        TutorialManager.Instance.TryShowTutorial(TargetTutorial);
    }

    #endregion

    #region Enums



    #endregion
}
