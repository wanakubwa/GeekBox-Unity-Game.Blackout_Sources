using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using System;
using DG.Tweening;
using GeekBox.Scripts.Generic;

public class MainMenuCarouselController : SingletonBase<MainMenuCarouselController>
{
    #region Fields

    [SerializeField]
    private CarouselScreen centerScreeen;
    [SerializeField]
    private CarouselScreen rightScreeen;
    [SerializeField]
    private CarouselScreen leftScreeen;

    [Space]
    [SerializeField]
    private RectTransform centerPoint;
    [SerializeField]
    private RectTransform content;
    [SerializeField]
    private float spaceBetweenScreens = 1600f;

    [Header("Animation Settings")]
    [SerializeField]
    private float transitionDurationS = 0.2f;

    #endregion

    #region Propeties

    private CarouselScreen RightScreeen { 
        get => rightScreeen;
    }

    private CarouselScreen CenterScreeen {
        get => centerScreeen; 
    }

    private CarouselScreen LeftScreeen {
        get => leftScreeen;
    }

    private RectTransform CenterPoint { 
        get => centerPoint;  
    }

    private RectTransform Content { 
        get => content;  
    }

    private float SpaceBetweenScreens { 
        get => spaceBetweenScreens; 
    }

    private ScreenType SelectedScreen
    {
        get; set;
    } = ScreenType.MAIN_SCREEN;

    private float TransitionDurationS { 
        get => transitionDurationS; 
    }

    #endregion

    #region Methods

    public void MakeTransitionToScreen(ScreenType target)
    {
        if(SelectedScreen == target)
        {
            return;
        }

        CarouselScreen targetScreen = GetScreenByType(target);
        CarouselScreen currentScreen = GetScreenByType(SelectedScreen);
        SelectedScreen = target;

        // (target_screen - Srodek) = wartosc o ktora nalezy przesunac content. Pozostaje kwestia znaku.
        // W ktora strone przesunac rozwiazuje znak tego rownania. + = targte jest po lewej; - = target jest po prawej.
        // Obliczenie dystansu pomiedzy punktem srodka i wybranym ekranem. Dystans moze zostac scachowany bo zawsze wyniesie tyle samo w teorii.
        // W praktyce moze sie (kiedys) zdarzyc ze beda 4 dodatkowe ekrany wiec dystans bedzie inny w zaleznosci od targetu.
        float distance = Math.Abs(targetScreen.transform.position.x - CenterPoint.position.x);
        float shiftValue = targetScreen.transform.position.x > 0 ? distance * -1 : distance;
        Vector3 contentPosition = new Vector3(Content.position.x + shiftValue, Content.position.y, Content.position.z);

        targetScreen.Show();
        SetContentPosition(contentPosition, currentScreen);
    }

    private void SetContentPosition(Vector3 newPosition, CarouselScreen screenToHide)
    {
        Content.DOMoveX(newPosition.x, TransitionDurationS).OnComplete(() => {
            if(screenToHide != null)
            {
                screenToHide.Hide();
            }
        });
    }

    private CarouselScreen GetScreenByType(ScreenType target)
    {
        switch (target)
        {
            case ScreenType.MAIN_SCREEN:
                return CenterScreeen;
            case ScreenType.LVL_SELECT_SCREEN:
                return RightScreeen;
            case ScreenType.UPGRADE_SCREEN:
                return LeftScreeen;
        }

        return null;
    }

    private void Start()
    {
        MEC.Timing.RunCoroutine(WaitOneFrameAndInitialize());
    }

    private IEnumerator<float> WaitOneFrameAndInitialize()
    {
        yield return MEC.Timing.WaitForOneFrame;

        Vector3 screenShift = new Vector3(CenterPoint.position.x + CenterScreeen.RectTransform.rect.width + SpaceBetweenScreens, 0f, 0f);
        CenterScreeen.Initialize(CenterPoint.position, true);
        RightScreeen.Initialize(CenterPoint.position + screenShift, false);
        LeftScreeen.Initialize(CenterPoint.position - screenShift, false);
    }

#if UNITY_EDITOR

    [Button]
    public void RefreshScreensPosition()
    {
        Vector3 screenShift = new Vector3(CenterPoint.position.x + CenterScreeen.RectTransform.rect.width + SpaceBetweenScreens, 0f, 0f);
        CenterScreeen.Initialize(CenterPoint.position, true);
        RightScreeen.Initialize(CenterPoint.position + screenShift, true);
        LeftScreeen.Initialize(CenterPoint.position - screenShift, true);
    }

#endif

#endregion

    #region Enums

    public enum ScreenType
    {
        MAIN_SCREEN,
        LVL_SELECT_SCREEN,
        UPGRADE_SCREEN
    }

    #endregion
}
