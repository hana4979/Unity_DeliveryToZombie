using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 관련 코드

public class PlayerHealth : LivingEntity
{
    public Slider healthSlider; // 체력을 표시할 UI 슬라이더

    public AudioClip deathClip; // 사망 소리
    public AudioClip hitClip; // 피격 소리

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기

    private PlayerCamera playerMovement; // 플레이어 움직임 컴포넌트
    private PlayerShooter playerShooter; // 플레이어 슈터 컴포넌트
    private ItemUse itemUse; // 아이템 사용 컴포넌트

    private void Awake()
    {
        // 사용할 컴포넌트 가져오기
        playerAudioPlayer = GetComponent<AudioSource>();

        playerMovement = GetComponent<PlayerCamera>();
        playerShooter = GetComponent<PlayerShooter>();
        itemUse = GetComponent<ItemUse>();
    }

    protected override void OnEnable()
    {
        // LivingEntity의 OnEnable() 실행(상태 초기화)
        base.OnEnable();

        // 체력 슬라이더 활성화
        healthSlider.gameObject.SetActive(true);
        // 체력 슬라이더의 최댓밗을 기본 체력값으로 변경
        healthSlider.maxValue = startingHealth;
        // 체력 슬라이더의 값을 현재 체력값으로 변경
        healthSlider.value = health;

        // 플레이어 조작을 받는 컴포넌트 활성화
        playerShooter.enabled = true;
        playerMovement.enabled = true;
        itemUse.enabled = true;
    }
    
    // 체력 회복
    public override void RestoreHealth(float newHealth)
    {
        // LivingEntity의 RestoreHealth() 실행(체력 증가)
        base.RestoreHealth(newHealth);
        // 갱신된 체력으로 체력 슬라이더 갱신
        healthSlider.value = health;
    }

    // 대미지 처리
    public override void OnDamage(float damage, Vector3 hitPoint, Vector3 hitDirection)
    {
        if (!dead)
        {
            // 사망하지 않은 경우에만 효과음 재생
            playerAudioPlayer.PlayOneShot(hitClip);
        }

        // LivingEntity의 OnDamage() 실행(대미지 적용)
        base.OnDamage(damage, hitPoint, hitDirection);
        // 갱신된 체력을 체력 슬라이더에 반영
        healthSlider.value = health;
    }

    // 사망 처리
    public override void Die()
    {
        // LivingEntity의 Die() 실행(사망 적용)
        base.Die();

        // 체력 슬라이더 비활성화
        healthSlider.gameObject.SetActive(false);

        // 사망음 재생
        playerAudioPlayer.PlayOneShot(deathClip);

        // 플레이어 조작을 받는 컴포넌트 비활성화

        playerShooter.enabled = false;
        playerMovement.enabled = false;
        itemUse.enabled = false;
    }
}
