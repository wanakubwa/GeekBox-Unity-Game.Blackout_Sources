using UnityEngine;
using UnityEngine.InputSystem;
using System;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class InputManager : ManagerSingletonBase<InputManager>
{
    #region Fields

    private const float PINCH_THRESHOLD = 5f;

    #endregion

    #region Propeties

    public event Action<Vector2> OnMouseLeftClick = delegate { };
    public event Action<Vector2> OnMouseLeftUp = delegate { };
    public event Action<Vector2> OnMouseLeftHold = delegate { };
    public event Action<Vector2> OnMouseRightClick = delegate { };
    public event Action<Vector2> OnMouseRightUp = delegate { };

    public event Action<float> OnMouseScrollChanged = delegate { };

    // Mobile Gestures.

    /// <summary>
    /// Open pinch - Do zewnatrz.
    /// </summary>
    public event Action<float> OnPinchOpenGesture = delegate { };

    /// <summary>
    /// Close pinch - Do wewnatrz.
    /// </summary>
    public event Action<float> OnPinchCloseGesture = delegate { };

    /// <summary>
    /// One of actual touching finger go up but on screen are still other fingers.
    /// </summary>
    public event Action OnOneOfFingersUp = delegate { };

    public bool IsLeftMouseHold
    {
        get;
        set;
    } = false;

    public bool IsDoubleTouch
    {
        get;
        set;
    } = false;

    private float LastTouchDistance
    {
        get;
        set;
    } = 0f;

    #endregion

    #region Methods

    public void OnMouseLeftButtonDown()
    {
        IsLeftMouseHold = true;
        OnMouseLeftClick(Mouse.current.position.ReadValue());
    }

    public void OnMouseLeftButtonUp()
    {
        IsLeftMouseHold = false;
        OnMouseLeftUp(Mouse.current.position.ReadValue());
    }

    public void OnMouseRightButtonUp()
    {
        OnMouseRightUp(Mouse.current.position.ReadValue());
    }

    public void OnMouseScrollMove(InputValue input)
    {
        OnMouseScrollChanged(input.Get<Vector2>().y * 5f);
    }

    public override void AttachEvents()
    {
        base.AttachEvents();

        Touch.onFingerDown += OnFingerTouch;
        Touch.onFingerUp += OnFingerUp;
        //Touch.onFingerMove += OnFingerMove;
    }

    protected override void DetachEvents()
    {
        base.DetachEvents();

        Touch.onFingerDown -= OnFingerTouch;
        Touch.onFingerUp -= OnFingerUp;
        //Touch.onFingerMove -= OnFingerMove;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        EnhancedTouchSupport.Enable();
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        EnhancedTouchSupport.Disable();
    }

    private void OnFingerTouch(Finger finger)
    {
        if(Touch.activeFingers.Count == 1)
        {
            IsLeftMouseHold = true;
            OnMouseLeftClick(Touch.activeFingers[0].screenPosition);
        }
        else if(Touch.activeFingers.Count == 2)
        {
            HandleDoubleTouch();
        }
    }

    private void OnFingerUp(Finger finger)
    {
        if (Touch.activeFingers.Count == 1)
        {
            IsLeftMouseHold = false;
            OnMouseLeftUp(Touch.activeFingers[0].screenPosition);

            if (IsDoubleTouch == true)
            {
                IsDoubleTouch = false;
            }
        }
        else
        {
            Debug.Log("one finger up");
            OnOneOfFingersUp();
        }
    }

    private void HandleDoubleTouch()
    {
        LastTouchDistance = 0f;
        IsDoubleTouch = true;
    }

    private void Update()
    {

#if UNITY_EDITOR
        if (IsLeftMouseHold)
        {
            OnMouseLeftHold(Mouse.current.position.ReadValue());
        }
#elif UNITY_ANDROID

        // Dwa palce na ekranie telefonu.
        if (IsDoubleTouch && Touch.activeFingers.Count == 2)
        {
            CheckDoubleTouchGesture(Touch.activeFingers[0], Touch.activeFingers[1]);
        }
        else if (IsLeftMouseHold) // Hot-fix bledu na galaxy note.
        {
            TryNotifyOnFingerMove();
        }
#endif
    }

    private void TryNotifyOnFingerMove()
    {
        if (Touch.activeFingers.Count > 0)
        {
            OnMouseLeftHold(Touch.activeFingers[0].screenPosition);
        }
    }

    private void CheckDoubleTouchGesture(Finger firstFinger, Finger secondFinger)
    {
        Vector2 touch0, touch1;
        float distance;
        touch0 = firstFinger.screenPosition;
        touch1 = secondFinger.screenPosition;
        distance = Vector2.Distance(touch0, touch1);

        if(distance.Equals(LastTouchDistance) == true)
        {
            return;
        }

        if(LastTouchDistance == 0f)
        {
            LastTouchDistance = distance;
            return;
        }

        float distanceDelta = Mathf.Abs(LastTouchDistance - distance);
        if (LastTouchDistance != 0f && distanceDelta > PINCH_THRESHOLD)
        {
            if (distance > LastTouchDistance)
            {
                // Jaka jest roznica w dystansie, do ustalenia szybkosci gestu.
                float multiplier = LastTouchDistance / distance;
                OnPinchOpenGesture(multiplier);
            }
            else
            {
                // Jaka jest roznica w dystansie, do ustalenia szybkosci gestu.
                float multiplier = distance / LastTouchDistance;
                OnPinchCloseGesture(-multiplier);
            }

            LastTouchDistance = distance;
        }
    }

#endregion

#region Enums



#endregion
}
