using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun; // 사용할 총

    private PlayerInput playerInput;

    private void Start()
    {
        // 사용할 컴포넌트 가져오기
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // 입력을 감지하고 총을 발사하거나 재장전
        if (playerInput.fire)
        {
            // 발사 입력 감지 시 총 발사
            gun.Fire();
        }
        else if (playerInput.reload)
        {
            // 재장전 입력 감지 시 재장전
            gun.Reload();
        }

        // 남은 탄알 UI 갱신
        UpdateUI();
    }

    // 탄알 UI 갱신
    private void UpdateUI()
    {
        /*
        if (gun != null && UIManager.instance != null){
            // UI 매니저의 탄알 텍스트에 탄창과 탄알과 남은 전체 탄알 표시
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
        */
    }

}
