﻿using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private Transform _slotsParentObject;
    [SerializeField] private List<Slot> _slots = new List<Slot>();

    private void Start()
    {
        SetDefaultAmount();
        GetAllSlots();
    }

    private void GetAllSlots()
    {
        for (int i = 0; i < _slotsParentObject.childCount; i++)
        {
            if (_slotsParentObject.GetChild(i).GetComponent<Slot>() != null)
            {
                _slots.Add(_slotsParentObject.GetChild(i).GetComponent<Slot>());
            }
        }
    }

    public void AddItem(ItemParameters itemParameters, int amount)
    {
        foreach (Slot slot in _slots)
        {
            if (slot.ItemParameters == itemParameters)
            {
                if (slot.Amount + amount <= itemParameters._maximumAmount)
                {
                    slot.Amount += amount;
                    slot.TextAmount.text = slot.Amount.ToString();
                    Debug.Log("Item added in stack ");
                }

                break;
            }
        }

        foreach (Slot slot in _slots)
        {
            if (slot.IsEmpty)
            {
                slot.ItemParameters = itemParameters;
                slot.Amount = amount;
                slot.IsEmpty = false;
                slot.SetIcon(itemParameters.Icon);
                if (slot.ItemParameters._maximumAmount != 1)
                {
                    slot.TextAmount.text = amount.ToString();
                }

                Debug.Log("Item added in empty slot");
                break;
            }
        }
    }

    private void SetDefaultAmount()
    {
        foreach (Slot slot in _slots)
        {
            slot.TextAmount.text = "";
        }
    }
}