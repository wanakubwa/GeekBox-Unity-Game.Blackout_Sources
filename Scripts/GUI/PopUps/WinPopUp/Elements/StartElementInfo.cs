using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class StartElementInfo : MonoBehaviour
{
    #region Fields

    [SerializeField]
    private Image starImageRoot;
    [SerializeField]
    private Sprite unlockedIco;
    [SerializeField]
    private Sprite lockedIco;
    [SerializeField]
    private TextMeshProUGUI timeText;

    #endregion

    #region Propeties

    private Image StarImageRoot { get => starImageRoot; }
    private Sprite UnlockedIco { get => unlockedIco; }
    private Sprite LockedIco { get => lockedIco; }
    private TextMeshProUGUI TimeText { get => timeText; }

    #endregion

    #region Methods

    public void SetInfo(float timeThresholdMs, bool isUnlocked)
    {
        SetTimeText(timeThresholdMs);
        if(isUnlocked == true)
        {
            SetUnlockedstatus();
        }
        else
        {
            SetLockedStatus();
        }
    }

    private void SetTimeText(float timeMs)
    {
        TimeText.text = timeMs.ToTimeFormatt("mm:ss");
    }

    private void SetLockedStatus()
    {
        StarImageRoot.sprite = lockedIco;
    }

    private void SetUnlockedstatus()
    {
        StarImageRoot.sprite = unlockedIco;
    }

    #endregion

    #region Enums



    #endregion
}
