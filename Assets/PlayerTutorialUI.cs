using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PlayerTutorialUI : MonoBehaviourPun
{
    public TutorialManager myTutorialManager;

    void Start()
    {
        /*if (!photonView.IsMine)
        {
            gameObject.SetActive(false);
            return;
        }*/

        if (myTutorialManager == null)
        {
            myTutorialManager = GetComponentInChildren<TutorialManager>();
            if (myTutorialManager == null)
            {
                Debug.LogError("myTutorialManager가 연결되지 않았습니다!");
                return;
            }
        }

        GameManager.Instance.tutorialManagerFromPlayer = myTutorialManager;
        GameManager.Instance.TryStartTutorial();
    }
}
