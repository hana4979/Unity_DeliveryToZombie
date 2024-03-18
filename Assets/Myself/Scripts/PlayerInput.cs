using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    /*
    public string moveAxisName = "Vertical"; // �յ� �������� ���� �Է��� �̸�
    public string rotateAxisName = "Horizontal"; // �¿� ȸ���� ���� �Է��� �̸�
    */

    public string fireButtonName = "Fire1"; // �߻縦 ���� �Է� ��ư �̸�
    public string reloadButtonName = "Reload"; // �������� ���� �Է� ��ư �̸�
    public string item1ButtonName = "Item1"; // ������1 ������ ���� �Է� ��ư �̸�
    public string item2ButtonName = "Item2"; // ������2 ������ ���� �Է� ��ư �̸�
    public string item3ButtonName = "Item3"; // ������3 ������ ���� �Է� ��ư �̸�
    public string useItemButtonName = "UseItem"; // ������ ����� ���� �Է� ��ư �̸�


    // �� �Ҵ��� ���ο����� ����
    /*
    public float move { get; private set; } // ������ ������ �Է°�
    public float rotate { get; private set; } // ������ ȸ�� �Է°�
    */

    public bool fire { get; private set; } // ������ �߻� �Է°�
    public bool reload { get; private set; } // ������ ������ �Է°�
    public bool item1 { get; private set; } // ������ ������1 �Է°�
    public bool item2 { get; private set; } // ������ ������2 �Է°�
    public bool item3 { get; private set; } // ������ ������3 �Է°�
    public bool useItem { get; private set; } // ������ ������ ��� �Է°�

    void Update()
    {
        // ..���ӿ��� ���¿����� ����� �Է��� �������� ����
        if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            /*
            move = 0;
            rotate = 0;
            */
            fire = false;
            reload = false;
            item1 = false;
            item2 = false;
            item3 = false;
            useItem = false;
            return;
        }

        /*
        move = Input.GetAxis(moveAxisName); // move�� ���� �Է� ����
        rotate = Input.GetAxis(rotateAxisName); // rotate�� ���� �Է� ����
        */

        fire = Input.GetButton(fireButtonName); // fire�� ���� �Է� ����
        reload = Input.GetButtonDown(reloadButtonName); // reload�� ���� �Է� ����
        item1 = Input.GetButtonDown(item1ButtonName); // item1�� ���� �Է� ����
        item2 = Input.GetButtonDown(item2ButtonName); // item2�� ���� �Է� ����
        item3 = Input.GetButtonDown(item3ButtonName); // item3�� ���� �Է� ����
        useItem = Input.GetButtonDown(useItemButtonName); // useItem�� ���� �Է� ����
    }
}
