using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    /*
    public string moveAxisName = "Vertical"; // 앞뒤 움직임을 위한 입력축 이름
    public string rotateAxisName = "Horizontal"; // 좌우 회전을 위한 입력축 이름
    */

    public string fireButtonName = "Fire1"; // 발사를 위한 입력 버튼 이름
    public string reloadButtonName = "Reload"; // 재장전을 위한 입력 버튼 이름
    public string item1ButtonName = "Item1"; // 아이템1 선택을 위한 입력 버튼 이름
    public string item2ButtonName = "Item2"; // 아이템2 선택을 위한 입력 버튼 이름
    public string item3ButtonName = "Item3"; // 아이템3 선택을 위한 입력 버튼 이름
    public string useItemButtonName = "UseItem"; // 아이템 사용을 위한 입력 버튼 이름


    // 값 할당은 내부에서만 가능
    /*
    public float move { get; private set; } // 감지된 움직임 입력값
    public float rotate { get; private set; } // 감지된 회전 입력값
    */

    public bool fire { get; private set; } // 감지된 발사 입력값
    public bool reload { get; private set; } // 감지된 재장전 입력값
    public bool item1 { get; private set; } // 감지된 아이템1 입력값
    public bool item2 { get; private set; } // 감지된 아이템2 입력값
    public bool item3 { get; private set; } // 감지된 아이템3 입력값
    public bool useItem { get; private set; } // 감지된 아이템 사용 입력값

    void Update()
    {
        // ..게임오버 상태에서는 사용자 입력을 감지하지 않음
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
        move = Input.GetAxis(moveAxisName); // move에 관한 입력 감지
        rotate = Input.GetAxis(rotateAxisName); // rotate에 관한 입력 감지
        */

        fire = Input.GetButton(fireButtonName); // fire에 관한 입력 감지
        reload = Input.GetButtonDown(reloadButtonName); // reload에 관한 입력 감지
        item1 = Input.GetButtonDown(item1ButtonName); // item1에 관한 입력 감지
        item2 = Input.GetButtonDown(item2ButtonName); // item2에 관한 입력 감지
        item3 = Input.GetButtonDown(item3ButtonName); // item3에 관한 입력 감지
        useItem = Input.GetButtonDown(useItemButtonName); // useItem에 관한 입력 감지
    }
}
