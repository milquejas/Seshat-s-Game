using System;
using System.Collections.Generic;
using UnityEngine;

/*
 * Setup item so it takes info from SO when enabled so it can be pooled
 * Spawning back to inventory if dropped outside the game. 
 * Animation when placed into inventory
 * Needs reference of coordinates of item slot in inventory? Or just fly into the middle of the inventory?
 * Draggable
*/

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class DraggableWeightedItem : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool InRange { get; set; }
    private LayerMask Interactable;
    private LayerMask layermask;
    public ItemSO Item;

    [field: NonSerialized] public Rigidbody2D RBody;

    [field: NonSerialized] public SpriteRenderer itemImage;
    public ScaleMinigameInventoryItem originInventoryItem;
    [field: NonSerialized] public ScaleCupScript ItemIsInThisCup;

    private List<GameObject> listOfColliderChildren = new List<GameObject>();
    [field: NonSerialized] public Transform originalParent;

    public Vector2 originPoolPosition;

    private void Awake()
    {
        if (listOfColliderChildren.Count == 0)
        {
            foreach (Transform child in transform)
            {
                listOfColliderChildren.Add(child.gameObject);
            }
        }
        originalParent = transform.parent;
    }

    public void RemoveFromCup()
    {
        if (ItemIsInThisCup is not null)
        {
            ItemIsInThisCup.RemoveFromCup(this);
        }
    }


    public void ChangeToNoCollisionLayer(bool toggle)
    {
        string layerName;

        if (toggle)
            layerName = "NoCollisions";

        else
            layerName = "Interactable";

        if (GetComponentInChildren<Collider2D>() != null)
        {
            GetComponentInChildren<Collider2D>().gameObject.layer = LayerMask.NameToLayer(layerName);
        }
            
    }

    public Transform Interact()
    {
        return transform;
    }

    private void OnEnable()
    {
        itemImage = GetComponent<SpriteRenderer>();
        itemImage.sprite = Item.ItemImage;
        RBody = GetComponent<Rigidbody2D>();
        RBody.mass = Item.ItemWeight / 100f;
        itemImage.color = Color.white;

        //EnableItemCollider(true);
    }

    public void InitializeWeightedItem(ItemSO item)
    {
        Item = item;
        RBody.mass = Item.ItemWeight / 100f;
        itemImage.sprite = Item.ItemImage;
    }

    // also gravity disabled while collider disabled
    public void EnableItemCollider(bool toggle)
    {
        RBody.gravityScale = Convert.ToInt32(toggle);

        switch (Item.ItemName)
        {
            case ItemType.Apple:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Cantaloupe:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Citrus:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Grapes:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Herbs:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Olives:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Onion:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Orange:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Pomegranate:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Potato:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Watermelon:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            default:
                Debug.Log("case for item type collider not set?");
                break;
        }
    }
}
