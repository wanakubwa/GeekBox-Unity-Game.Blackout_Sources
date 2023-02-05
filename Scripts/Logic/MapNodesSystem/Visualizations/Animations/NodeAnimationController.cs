using UnityEngine;
using System.Collections;

public class NodeAnimationController : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Animator currentAnimator;

    [SerializeField]
    private NodeRetakeAnimationController retakeComponent;

    [Header("Properties Settings")]
    [SerializeField]
    private string selectedTriggerName;

    #endregion

    #region Propeties

    private Animator CurrentAnimator { 
        get => currentAnimator; 
    }

    private string SelectedTriggerName { 
        get => selectedTriggerName; 
    }
    public NodeRetakeAnimationController RetakeComponent { get => retakeComponent; }

    #endregion

    #region Methods

    public void PlaySelectedAnimation()
    {
        CurrentAnimator.SetTrigger(SelectedTriggerName);
    }

    public void PlayRetakeAnimation(Color targetColor)
    {
        RetakeComponent.ShowRetake(targetColor);
    }

    #endregion

    #region Enums



    #endregion
}
