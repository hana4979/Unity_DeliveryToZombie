using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� ���� ������Ʈ�� �ֱ������� ����
public class ZombieSpawner : MonoBehaviour
{
    public Zombie zombiePrefab; // ������ ���� ���� ������

    public ZombieData[] zombieDatas; // ����� ���� �¾� ������
    public Transform[] spawnPoints; // ���� AI�� ��ȯ�� ��ġ

    private float currentTime; // ���� �ð�
    private float spawnTime = 10f; // ���� �ð�
    //private List<Zombie> zombies = new List<Zombie>(); // ������ ���� ��� ����Ʈ
    //private int wave; // ���� ���̺�

    void Update()
    {
        // ���ӿ��� ������ ���� �������� ����
        if(GameManager.instance != null && GameManager.instance.isGameover)
        {
            return;
        }

        currentTime += Time.deltaTime; // ���� �ð� ī��Ʈ
        
        // ���� ��� ����ģ ���
        // ���� �ð��� ���� ��� ���� ���� ����
        if(spawnTime <= currentTime)
        {
            SpawnWave();
            currentTime = 0f; // ī��Ʈ �ʱ�ȭ
        }
    }

    private void SpawnWave()
    {
        // ���̺� 1 ����
        //wave++;

        // 3���� ���� ����
        for(int i = 0; i < 3; i++)
        {
            // ���� ���� ó�� ����
            CreateZombie();
        }
    }

    // ���� �����ϰ� ���� ������ ��� �Ҵ�
    private void CreateZombie()
    {
        // ����� ���� ������ �������� ����
        ZombieData zombieData = zombieDatas[Random.Range(0, zombieDatas.Length)];

        // ������ ��ġ�� �������� ����
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

        // ���� ���������κ��� ���� ����
        Zombie zombie = Instantiate(zombiePrefab, spawnPoint.position, spawnPoint.rotation);

        // ������ ������ �ɷ�ġ ����
        zombie.Setup(zombieData);

        // ������ ���� ����Ʈ�� �߰�
        //zombies.Add(zombie);

        // ������ onDeath �̺�Ʈ�� �͸� �޼��� ���
        // ����� ���� ����Ʈ���� ����
        //zombie.onDeath += () => zombies.Remove(zombie);
        // ����� ���� 10�� �ڿ� �ı�
        zombie.onDeath += () => Destroy(zombie.gameObject, 10f);
        // ���� ��� �� ���� ���
        zombie.onDeath += () => GameManager.instance.AddScore(20);
    }
}
