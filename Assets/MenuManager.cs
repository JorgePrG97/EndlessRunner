using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class MenuManager : MonoBehaviour {

    public GameObject titleText;

    private void Awake()
    {
        titleText.gameObject.GetComponent<RectTransform>().DOScale(1.5f, 1.5f).SetLoops(1, LoopType.Yoyo);
    }
    public void StartPlay()
    {
        SceneManager.LoadScene(1);
    }
}
