using System;
using System.Collections.Generic;
using UnityEngine;
using static AntiqueScaleExtensions;

/*
 * IInteractable item that is moved through TouchAndMouseBehaiour script
 * Collider specific to different SO items 
 * Colliders can be toggled
 * Coupled to ScaleCupScript to handle antique scale
 * TODO: sorting layer depth raised each time you interact with an item to get it to front. 
*/

[RequireComponent(typeof(Rigidbody2D), typeof(SpriteRenderer))]
public class DraggableWeightedItem : MonoBehaviour, IInteractable
{
    [field: SerializeField] public bool InRange { get; set; }
    public ItemSO Item;

    [field: NonSerialized] public Rigidbody2D RBody;
    [field: NonSerialized] public SpriteRenderer itemImage;
    [field: NonSerialized] public ScaleCupScript ItemIsInThisCup;

    public ScaleMinigameInventoryItem originInventoryItem;
    [SerializeField] private List<GameObject> listOfColliderChildren = new List<GameObject>();
    [field: NonSerialized] public Transform originalParent;

    public Vector2 originPoolPosition;
    public static event Action<ItemSO> DraggableItemTouched;

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
        DraggableItemTouched?.Invoke(Item);
        return transform;
    }

    private void OnEnable()
    {
        itemImage = GetComponent<SpriteRenderer>();
        itemImage.sprite = Item.ItemImage;
        RBody = GetComponent<Rigidbody2D>();
        RBody.mass = ConvertRealWeightToUnityMass(Item.ItemWeight);
        itemImage.color = Color.white;

        //EnableItemCollider(true);
    }

    public void InitializeWeightedItem(ItemSO item)
    {
        Item = item;
        RBody.mass = ConvertRealWeightToUnityMass(Item.ItemWeight);
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

            case ItemType.Weight1to10:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Weight20to100:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Weight500to1000:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            case ItemType.Weight5000to10000:
                listOfColliderChildren[(int)Item.ItemName].SetActive(toggle);
                break;

            default:
                Debug.Log("case for item type collider not set?");
                break;
        }
    }
}
