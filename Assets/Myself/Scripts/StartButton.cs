using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // �� ������ ���� �ڵ�

public class StartButton : MonoBehaviour
{
    public GameObject gamestartUI; // ���ӽ���

    public void GameStart()
    {
        SceneManager.LoadScene("Stage01");
    }
}
