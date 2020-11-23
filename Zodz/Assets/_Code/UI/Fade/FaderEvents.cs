using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FaderEvents : MonoBehaviour
{
    public UnityEvent OnFadeIn;
    public UnityEvent OnFadeOut;

    public void FadeInCallback(){
        OnFadeIn?.Invoke();
    }

    public void FadeOutCallback(){
        OnFadeOut?.Invoke();
    }
}
