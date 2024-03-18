using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI ���� �ڵ�

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // ü���� ǥ���� UI �����̴�

    public AudioClip deathClip; // ��� �Ҹ�
    public AudioClip hitClip; // �ǰ� �Ҹ�

    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����

    private PlayerCamera playerMovement; // �÷��̾� ������ ������Ʈ
    private PlayerShooter playerShooter; // �÷��̾� ���� ������Ʈ
    private ItemUse itemUse; // ������ ��� ������Ʈ

    private void Awake()
    {
        // ����� ������Ʈ ��������
        playerAudioPlayer = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerCamera>();
        playerShooter = GetComponent<PlayerShooter>();
        itemUse = GetComponent<ItemUse>();
    }

    protected override void OnEnable()
    {
        // LivingEntity�� OnEnable() ����(���� �ʱ�ȭ)
        base.OnEnable();

        // ü�� �����̴� Ȱ��ȭ
        healthSlider.gameObject.SetActive(true);
        // ü�� �����̴��� �ִ���� �⺻ ü�°����� ����
        healthSlider.maxValue = startingHealth;
        // ü�� �����̴��� ���� ���� ü�°����� ����
        healthSlider.value = health;

        // �÷��̾� ������ �޴� ������Ʈ Ȱ��ȭ
        playerShooter.enabled = true;
        playerMovement.enabled = true;
        itemUse.enabled = true;
    }
    
    // ü�� ȸ��
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity�� RestoreHealth() ����(ü�� ����)
        base.RestoreHealth(newHealth);
        // ���ŵ� ü������ ü�� �����̴� ����
        healthSlider.value = health;
    }

    // ����� ó��
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            // ������� ���� ��쿡�� ȿ���� ���
            playerAudioPlayer.PlayOneShot(hitClip);
        }

        // LivingEntity�� OnDamage() ����(����� ����)
        base.OnDamage(damage, hitPoint, hitDirection);
        // ���ŵ� ü���� ü�� �����̴��� �ݿ�
        healthSlider.value = health;
    }

    // ��� ó��
    public override void Die()
    {
        // LivingEntity�� Die() ����(��� ����)
        base.Die();

        // ü�� �����̴� ��Ȱ��ȭ
        healthSlider.gameObject.SetActive(false);

        // ����� ���
        playerAudioPlayer.PlayOneShot(deathClip);

        // �÷��̾� ������ �޴� ������Ʈ ��Ȱ��ȭ

        playerShooter.enabled = false;
        playerMovement.enabled = false;
        itemUse.enabled = false;
    }
}
