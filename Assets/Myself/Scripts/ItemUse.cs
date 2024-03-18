using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public ItemData data; // 스크립티블 오브젝트 ItemData
    private PlayerInput playerInput;
    private ItemSpawner itemSpawner;
    private ItemUI itemUI;

    private int selectItem = 0; // 선택한 아이템 번호
    private bool onSpawner = false; // 스포너에 위치해 있는지 여부

    private AudioSource playerAudioPlayer; // 플레이어 소리 재생기
    public AudioClip DeliveryClip; // 배달 완료 소리

    private void Start()
    {
        // 필요한 컴포넌트 가져오기
        playerInput = GetComponent<PlayerInput>();
        itemSpawner = GameManager.instance.itemSpawner;
        itemUI = GetComponent<ItemUI>();

        playerAudioPlayer = GetComponent<AudioSource>();
    }

    private void Update()
    {
        Use();

        if (onSpawner)
        {
            // useItem 버튼을 눌렀을 때
            if (playerInput.useItem)
            {
                bool isItemSpawned = itemSpawner.isSpawn;
                var item = itemSpawner.itemData;
                bool isSameItem = selectItem == item.itemID;
                // 스포너에서 생성된 아이템의 아이디가 선택한 아이디와 같다면
                if (isItemSpawned && isSameItem)
                {
                    GameManager.instance.AddScore(100); // 100점 증가
                    playerAudioPlayer.PlayOneShot(DeliveryClip); // 배달 완료 시 효과음 재생
                    Destroy(itemSpawner.item); // 생성된 아이템 데이터 파괴
                    itemSpawner.spawnData.SetActive(false); // 생성된 스포너 비활성화
                    itemSpawner.isSpawn = false; // 스폰 상태 false
                }
            }
        }
    }
    
    private void Use()
    {
        // 입력된 아이템 버튼에 맞춰 선택된 아이템 설정
        if (playerInput.item1) selectItem = 0;
        if (playerInput.item2) selectItem = 1;
        if (playerInput.item3) selectItem = 2;

        GameManager.instance.SelectItem(selectItem);
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.gameObject.tag == "DeliverySpawner")
        {
            onSpawner = true;
        }
    }
}
