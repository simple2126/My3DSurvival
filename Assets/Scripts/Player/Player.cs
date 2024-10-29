using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerController controller;
    public PlayerCondition condition;

    public Equipment equip;
    public ItemData itemData;
    public Action addItem;
    public Transform dropPosition;

    private void Awake()
    {
        controller = GetComponent<PlayerController>();
        condition = GetComponent<PlayerCondition>();
        equip = GetComponent<Equipment>();
    }

    private void Start()
    {
        CharacterManager.Instance.Player = this;
    }
}