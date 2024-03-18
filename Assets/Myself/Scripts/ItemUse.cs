using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUse : MonoBehaviour
{
    public ItemData data; // ��ũ��Ƽ�� ������Ʈ ItemData
    private PlayerInput playerInput;
    private ItemSpawner itemSpawner;
    private ItemUI itemUI;

    private int selectItem = 0; // ������ ������ ��ȣ
    private bool onSpawner = false; // �����ʿ� ��ġ�� �ִ��� ����

    private AudioSource playerAudioPlayer; // �÷��̾� �Ҹ� �����
    public AudioClip DeliveryClip; // ��� �Ϸ� �Ҹ�

    private void Start()
    {
        // �ʿ��� ������Ʈ ��������
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
            // useItem ��ư�� ������ ��
            if (playerInput.useItem)
            {
                bool isItemSpawned = itemSpawner.isSpawn;
                var item = itemSpawner.itemData;
                bool isSameItem = selectItem == item.itemID;
                // �����ʿ��� ������ �������� ���̵� ������ ���̵�� ���ٸ�
                if (isItemSpawned && isSameItem)
                {
                    GameManager.instance.AddScore(100); // 100�� ����
                    playerAudioPlayer.PlayOneShot(DeliveryClip); // ��� �Ϸ� �� ȿ���� ���
                    Destroy(itemSpawner.item); // ������ ������ ������ �ı�
                    itemSpawner.spawnData.SetActive(false); // ������ ������ ��Ȱ��ȭ
                    itemSpawner.isSpawn = false; // ���� ���� false
                }
            }
        }
    }
    
    private void Use()
    {
        // �Էµ� ������ ��ư�� ���� ���õ� ������ ����
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
