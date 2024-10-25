using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;
using UnityEngine.XR;

public class EquipInteractor : Interactor
{
    [Header("Equipment Visuals")]
    public GameObject boomerangItem;
    public GameObject grappleItem;
    //Add Bomb glove/equip

    [SerializeField] private List<GameObject> acquiredItems = new List<GameObject>();

    [Header("Components")]
    [SerializeField] PlayerGrapple grappleStrategy;
    [SerializeField] BoomerangStrategy boomerangStrategy;

    private IEquipStrategy currentEquipStrategy;

    public override void Interact()
    {
        ChangeStrategy();

        if(currentEquipStrategy != null )
            currentEquipStrategy.UseEquipment();
    }

    public override void OnStart()
    {
        
    }

    private void ChangeStrategy()
    {
        if (currentEquipStrategy == null)
        {
            UnequipAllItems();
            boomerangItem.SetActive(true);
            currentEquipStrategy = boomerangStrategy;
        }

        if (input.equip1pressed && boomerangItem != null)
        {
            UnequipAllItems();
            boomerangItem.SetActive(true);
            currentEquipStrategy.OnStrategyChange();
            currentEquipStrategy = boomerangStrategy;
        }

        if (input.equip2pressed && grappleItem != null)
        {
            UnequipAllItems();
            grappleItem.SetActive(true);
            currentEquipStrategy.OnStrategyChange();
            currentEquipStrategy = grappleStrategy;
        }
    }

    private void UnequipAllItems()
    {
        if (acquiredItems.Count > 0)
        {
            foreach (var item in acquiredItems)
            {
                item.SetActive(false);
            }
        }
    }
}
