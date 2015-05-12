using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using XnLib;

public class TweenControl<T>
{
    public delegate T LerpFunc<T>(T start, T end, float amount);
    public delegate void TweenEndFunc();

    LerpFunc<T> m_lerpFunction;
    TweenEndFunc m_tweenEndFunction;

    public T CurrentValue { get; private set; }
    T m_start;
    T m_target;
    float m_duration;
    float m_timer;
    public bool IsActive { get; private set; }

    public T StartValue
    {
        get { return m_start; }
    }

    public T EndValue
    {
        get { return m_target; }
    }

    public float Duration
    {
        get { return m_duration; }
    }

    public TweenControl(LerpFunc<T> lerpFunction)
    {
        m_tweenEndFunction = null;
        m_lerpFunction = lerpFunction;
        CurrentValue = default(T);
        m_start = default(T);
        m_target = default(T);
        m_duration = 0.0f;
        m_timer = 0.0f;
        IsActive = false;
    }

    public TweenControl(LerpFunc<T> lerpFunction, T initialValue)
    {
        m_tweenEndFunction = null;
        m_lerpFunction = lerpFunction;
        CurrentValue = initialValue;
        m_start = initialValue;
        m_target = initialValue;
        m_duration = 0.0f;
        m_timer = 0.0f;
        IsActive = false;
    }

    public void Start(T start, T end, float duration, TweenEndFunc onTweenEndFunc = null)
    {
        m_start = start;
        m_target = end;
        m_duration = duration;
        m_timer = 0.0f;
        CurrentValue = m_start;
        m_tweenEndFunction = onTweenEndFunc;

        if (duration == 0.0f)
        {
            IsActive = false;
        }
        else
        {
            IsActive = true;
        }
    }

    public void Reset()
    {
        m_timer = 0.0f;
        CurrentValue = m_start;
        IsActive = false;
    }

    public void Update(float delta)
    {
        if (!IsActive)
        {
            return;
        }

        m_timer += delta;
        if (m_timer >= m_duration)
        {
            m_timer = m_duration;
            IsActive = false;
            if (m_tweenEndFunction != null)
            {
                m_tweenEndFunction();
            }
        }

        float percentage = Math.Min(m_timer / m_duration, 1.0f);
        CurrentValue = m_lerpFunction(m_start, m_target, percentage);
    }
}
