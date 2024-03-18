using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Item", menuName = "Scriptable Object/ItemData")]
public class ItemData : ScriptableObject
{
    [Header("# Main Info")]
    public int itemID; // 아이템 번호
    public string itemName; // 아이템 이름
    //public Sprite itemIcon; // 아이템 아이콘
    public int itemScore; // 아이템 점수
}
