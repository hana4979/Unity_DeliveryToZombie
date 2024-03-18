using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour
{
    public Gun gun; // ����� ��

    private PlayerInput playerInput;

    private void Start()
    {
        // ����� ������Ʈ ��������
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        // �Է��� �����ϰ� ���� �߻��ϰų� ������
        if (playerInput.fire)
        {
            // �߻� �Է� ���� �� �� �߻�
            gun.Fire();
        }
        else if (playerInput.reload)
        {
            // ������ �Է� ���� �� ������
            gun.Reload();
        }

        // ���� ź�� UI ����
        UpdateUI();
    }

    // ź�� UI ����
    private void UpdateUI()
    {
        /*
        if (gun != null && UIManager.instance != null){
            // UI �Ŵ����� ź�� �ؽ�Ʈ�� źâ�� ź�˰� ���� ��ü ź�� ǥ��
            UIManager.instance.UpdateAmmoText(gun.magAmmo, gun.ammoRemain);
        }
        */
    }

}
