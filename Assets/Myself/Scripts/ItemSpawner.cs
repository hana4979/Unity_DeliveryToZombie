using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs; // ������ ������ ������

    public ItemData[] itemDatas; // ������ �����͸� ���� �迭
    public GameObject[] spawnPoints; // ������ ������ ��ġ

    public ItemData itemData; // ������ ������
    public GameObject spawnData; // ������ ������
    public GameObject item;

    public bool isSpawn = false;

    void FixedUpdate()
    {
        // ���ӿ��� ���°ų� �̹� ������ ���� ���� �������� ����
        if (GameManager.instance != null && GameManager.instance.isGameover || isSpawn)
        {
            return;
        }

        CreateSpawn();

    }

    private void CreateSpawn()
    {
        // ����� ������ ������ �������� ����
        int randomItem = Random.Range(0, 3); // ������ �������� ����
        itemData = itemDatas[randomItem];

        // ������ ���� ��ġ�� �������� ����
        int randomSpawn = Random.Range(0, spawnPoints.Length);
        spawnData = spawnPoints[randomSpawn];
        spawnData.SetActive(true); // ������ Ȱ��ȭ
        Transform spawnPoint = spawnData.transform;

        // ������ ����
        item = Instantiate(itemPrefabs[randomItem], spawnPoint.position, spawnPoint.rotation);

        // ���� ���� true
        isSpawn = true;
    }
}
