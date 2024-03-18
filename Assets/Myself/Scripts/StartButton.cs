using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 씬 관리자 관련 코드

public class StartButton : MonoBehaviour
{
    public GameObject gamestartUI; // 게임시작

    public void GameStart()
    {
        SceneManager.LoadScene("Stage01");
    }
}
