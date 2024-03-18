using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("# Main Info")]
    public int itemID; // ������ ��ȣ
    public string itemName; // ������ �̸�
    //public Sprite itemIcon; // ������ ������
    public int itemScore; // ������ ����
}
