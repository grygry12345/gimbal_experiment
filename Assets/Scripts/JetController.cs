using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System;
using UnityEngine;
using UnityEngine.UI;

public class JetController : MonoBehaviour
{
    public Transform Jet;

    private Vector3 currentPosition;


    #region Initialization
    private void Start()
    {
        currentPosition = new Vector3(0, 0, 0);

        InputFieldYaw.text = "0.00";
        InputFieldPitch.text = "0.00";
        InputFieldRoll.text = "0.00";

        OrientationStartYaw.text = "0.00";
        OrientationStartPitch.text = "0.00";
        OrientationStartRoll.text = "0.00";
        OrientationEndYaw.text = "0.00";
        OrientationEndPitch.text = "0.00";
        OrientationEndRoll.text = "0.00";

        turnYaw = new Vector3(0, 1f, 0);
        turnPitch = new Vector3(1f, 0, 0);
        turnRoll = new Vector3(0, 0, 1f);
        

    }

    #endregion

    #region Slider Control

    public Slider SliderYaw;
    public Slider SliderPitch;
    public Slider SliderRoll;


    public void SliderYawControl()
    {
        currentPosition.y = SliderYaw.value;
        InputFieldYaw.text = currentPosition.y.ToString("F");
        Jet.eulerAngles = currentPosition;
    }

    public void SliderPitchControl()
    {
        currentPosition.x = -1 * SliderPitch.value;
        InputFieldPitch.text = (-1 * currentPosition.x).ToString("F");
        Jet.eulerAngles = currentPosition;
    }

    public void SliderRollControl()
    {
        currentPosition.z = SliderRoll.value;
        InputFieldRoll.text = currentPosition.z.ToString("F");
        Jet.eulerAngles = currentPosition;
    }
    #endregion

    #region Input field Control

    public InputField InputFieldPitch;
    public InputField InputFieldYaw;
    public InputField InputFieldRoll;

    public void InputFieldYawControl()
    {
        SliderYaw.value = float.Parse(InputFieldYaw.text);
    }

    public void InputFieldPitchControl()
    {
        SliderPitch.value = float.Parse(InputFieldPitch.text);
    }


    public void InputFieldRollControl()
    {
        SliderRoll.value = float.Parse(InputFieldRoll.text);
    }

    #endregion

    #region Orientation Input

    public InputField OrientationStartYaw;
    public InputField OrientationStartPitch;
    public InputField OrientationStartRoll;

    public InputField OrientationEndYaw;
    public InputField OrientationEndPitch;
    public InputField OrientationEndRoll;

    public void SetCurrentStart()
    {
        OrientationStartYaw.text = InputFieldYaw.text;
        OrientationStartPitch.text = InputFieldPitch.text;
        OrientationStartRoll.text = InputFieldRoll.text;
    }

    public void SetCurrentEnd()
    {
        OrientationEndYaw.text = InputFieldYaw.text;
        OrientationEndPitch.text = InputFieldPitch.text;
        OrientationEndRoll.text = InputFieldRoll.text;
    }

    #endregion

    #region Animation

    public float AnimationTime;

    private Vector3 turnYaw;
    private Vector3 turnPitch;
    private Vector3 turnRoll;

    private float orientaitonStartYaw;
    private float orientaitonStartPitch;
    private float orientaitonStartRoll;
    private float orientaitonEndYaw;
    private float orientaitonEndPitch;
    private float orientaitonEndRoll;

    private float diffAngleYaw;
    private float diffAnglePitch;
    private float diffAngleRoll;

    private float angleSpeedYaw;
    private float angleSpeedPitch;
    private float angleSpeedRoll;

    private bool isAnimationButtonClicked;
    private bool stoppedYaw;
    private bool stoppedPitch;
    private bool stoppedRoll;

    public void AnimationStart()
    {
        orientaitonStartYaw = float.Parse(OrientationStartYaw.text);
        orientaitonStartPitch = float.Parse(OrientationStartPitch.text);
        orientaitonStartRoll = float.Parse(OrientationStartRoll.text);
        
        orientaitonEndYaw = float.Parse(OrientationEndYaw.text);
        orientaitonEndPitch = float.Parse(OrientationEndPitch.text);
        orientaitonEndRoll = float.Parse(OrientationEndRoll.text);

        stoppedYaw = false;
        stoppedPitch = false;
        stoppedRoll = false;

        diffAngleYaw = orientaitonEndYaw - orientaitonStartYaw;
        diffAnglePitch = orientaitonEndPitch - orientaitonStartPitch;
        diffAngleRoll = orientaitonEndRoll - orientaitonStartRoll;

        angleSpeedYaw = diffAngleYaw / AnimationTime; 
        angleSpeedPitch = diffAnglePitch / AnimationTime; 
        angleSpeedRoll = diffAngleRoll / AnimationTime;

        Jet.eulerAngles = new Vector3(orientaitonStartPitch,orientaitonStartYaw,orientaitonStartRoll);

        isAnimationButtonClicked = true;
    }

    private void Update()
    {
        if (isAnimationButtonClicked)
        {
            if (Math.Round(Jet.eulerAngles.y - orientaitonEndYaw,2) != 0 && !stoppedYaw)
            {
                Jet.eulerAngles += turnYaw * angleSpeedYaw;
            }
            else
            {
                stoppedYaw = true;
            }
            if (Math.Round(Jet.eulerAngles.x - orientaitonEndPitch,2) != 0 && !stoppedPitch)
            {
                Jet.eulerAngles += turnPitch * angleSpeedPitch;
            }
            else
            {
                stoppedPitch = true;
            }
            if (Math.Round(Jet.eulerAngles.z - orientaitonEndRoll,2) != 0 && !stoppedRoll)
            {
                Jet.eulerAngles += turnRoll * angleSpeedRoll;
            }
            else
            {
                stoppedRoll = true;
            }
        }
        if (stoppedYaw && stoppedPitch && stoppedRoll)
        {
            isAnimationButtonClicked = false;
        }
    }

    #endregion
}
