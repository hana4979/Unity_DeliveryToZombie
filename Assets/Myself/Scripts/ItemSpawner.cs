using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // 생성할 아이템 프리팹

    public ItemData[] itemDatas; // 아이템 데이터를 담은 배열
    public GameObject[] spawnPoints; // 아이템 전달할 위치

    public ItemData itemData; // 생성된 아이템
    public GameObject spawnData; // 생성된 스포너
    public GameObject item;

    public bool isSpawn = false;

    void FixedUpdate()
    {
        // 게임오버 상태거나 이미 스폰이 됐을 때는 생성하지 않음
        if (GameManager.instance != null && GameManager.instance.isGameover || isSpawn)
        {
            return;
        }

        CreateSpawn();

    }

    private void CreateSpawn()
    {
        // 사용할 아이템 데이터 랜덤으로 결정
        int randomItem = Random.Range(0, 3); // 아이템 랜덤으로 결정
        itemData = itemDatas[randomItem];

        // 아이템 전달 위치를 랜덤으로 결정
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        spawnData = spawnPoints[randomSpawn];
        spawnData.SetActive(true); // 스포너 활성화
        Transform spawnPoint = spawnData.transform;

        // 아이템 생성
        item = Instantiate(itemPrefabs[randomItem], spawnPoint.position, spawnPoint.rotation);

        // 스폰 상태 true
        isSpawn = true;
    }
}
