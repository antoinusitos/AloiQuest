using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraManager : BaseManager
{
    private CursorLockMode _currentCursorLockMode;

    public void SetCursorLocked(CursorLockMode newMode)
    {
        _currentCursorLockMode = newMode;
        Cursor.lockState = _currentCursorLockMode;
    }

    public CursorLockMode GetCurrentCursorMode()
    {
        return _currentCursorLockMode;
    }


    public Image fadeImage = null;
    private float fadeTime = 0;
    public delegate void function();
    function callBack;

    public void FadeOut(float timeToFade, function toCall)
    {
        fadeTime = timeToFade;
        callBack = toCall;

        if (fadeImage == null) Debug.LogError("NO IMAGE TO FADE !");

        StartCoroutine("Fade");
    }

    private IEnumerator Fade()
    {
        float timer = 0;
        while(timer < fadeTime)
        {
            Color c = fadeImage.color;
            c.a = timer / fadeTime;
            fadeImage.color = c;
            timer += Time.deltaTime;
            yield return null;
        }
        Color col = fadeImage.color;
        col.a = 1.0f;
        fadeImage.color = col;
        callBack();
    }


    // -----------------------------------------------------------------------------------------

    public override void InitManagerForEditor()
    {

    }



    //
    // Singleton Stuff
    // 

    private static CameraManager _instance;

    public static CameraManager GetInstance()
    {
        return _instance;
    }

    private void Awake()
    {
        _instance = this;
    }
}
