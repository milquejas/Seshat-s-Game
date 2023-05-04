using UnityEngine;

/*
 * When stuff stays in a cup for 2sec, sticky them and add them to the weight pool? 
 * Every time something is added, update weights and animation? 
 * Every time something is removed, update weights and animation? 
 * If scale is even && one side has only weights -> enable accept trade button? 
 * Accepting trade gives you what trade ui says. 
 * Inventory update sends event to quest manager. 
 * QuestManager compares items you got to quest target. 
 * Start from beginning/reset inventory if wrong. 
 * 
 * Tip button to show what is inside a cup and their weights?
 * 
 * Limit which side of the scale can take items.
 * Tutorial limits actions player can take. 
*/ 

public class ScaleBehaviour : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        
    }
}
public class Quest
{
    public string description;
    public ItemType itemType;
    public float targetWeight;
}
