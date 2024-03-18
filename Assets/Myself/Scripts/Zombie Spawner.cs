using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 좀비 게임 오브젝트를 주기적으로 생성
public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab; // 생성할 좀비 원본 프리팹

    public ZombieData[] zombieDatas; // 사용할 좀비 셋업 데이터
    public Transform[] spawnPoints; // 좀비 AI를 소환할 위치

    private float currentTime; // 현재 시간
    private float spawnTime = 10f; // 스폰 시간
    //private List<Zombie> zombies = new List<Zombie>(); // 생성된 좀비를 담는 리스트
    //private int wave; // 현재 웨이브

    void Update()
    {
        // 게임오버 상태일 때는 생성하지 않음
        if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        currentTime += Time.deltaTime; // 현재 시간 카운트
        
        // 좀비를 모두 물리친 경우
        // 스폰 시간이 지난 경우 다음 스폰 실행
        if(spawnTime <= currentTime)
        {
            SpawnWave();
            currentTime = 0f; // 카운트 초기화
        }
    }

    private void SpawnWave()
    {
        // 웨이브 1 증가
        //wave++;

        // 3마리 좀비 생성
        for(int i = 0; i < 3; i++)
        {
            // 좀비 생성 처리 실행
            CreateZombie();
        }
    }

    // 좀비를 생성하고 좀비에 추적할 대상 할당
    private void CreateZombie()
    {
        // 사용할 좀비 데이터 랜덤으로 결정
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];

        // 생성할 위치를 랜덤으로 결정
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // 좀비 프리팹으로부터 좀비 생성
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        // 생성한 좀비의 능력치 설정
        zombie.Setup(zombieData);

        // 생성된 좀비를 리스트에 추가
        //zombies.Add(zombie);

        // 좀비의 onDeath 이벤트에 익명 메서드 등록
        // 사망한 좀비를 리스트에서 제거
        //zombie.onDeath += () => zombies.Remove(zombie);
        // 사망한 좀비를 10초 뒤에 파괴
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        // 좀비 사망 시 점수 상승
        zombie.onDeath += () => GameManager.instance.AddScore(20);
    }
}
