using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image image;
    [HideInInspector] public Transform parentAfterDrag;

    public ItemData itemData;

    //Begin Drag
    public void OnBeginDrag(PointerEventData eventData){
        Debug.Log("Begin Drag");
        Debug.Log("Dragging object: " + gameObject.name + " ItemData: " + itemData);

        //Layers Image above other slots by moving it's position in the hirarchy
        parentAfterDrag = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        //Stops any other mouse interaction while it's being dragged
        image.raycastTarget = false;

        eventData.pointerDrag = gameObject;
    }

    //While Dragging
    public void OnDrag(PointerEventData eventData){
        Debug.Log("Dragging");
        //Moves the Image with the mouse
        transform.position = eventData.position;
    }

    //End of drag
    public void OnEndDrag(PointerEventData eventData){
        Debug.Log("End Drag");
        //Puts it in the correct place in the hirearchy
        transform.SetParent(parentAfterDrag); 

        //Allows the mouse to interact with it again
        image.raycastTarget = true;

        //If dropped outside UI inventory, spawn world block and delete item
        if (!EventSystem.current.IsPointerOverGameObject())
        {
            SpawnBlock();
            Destroy(gameObject);
        }
    }

    //Spawns the world block version of this item
   private void SpawnBlock()
    {
        if (itemData == null)
        {
            Debug.LogError("DraggableItem has NO ItemData assigned! This item was not initialized properly.");
            return;
        }

        if (itemData.block == null)
        {
            Debug.LogError("ItemData exists but BLOCK prefab is missing!");
            return;
        }

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = 10f;

        Vector3 spawnPos = Camera.main.ScreenToWorldPoint(mousePos);
        spawnPos.z = 0f;

        Instantiate(itemData.block, spawnPos, Quaternion.identity);
    }
    //Called by InventorySlot when creating this item
    public void Setup(ItemData data)
    {
        itemData = data;

        if (itemData == null)
        {
            Debug.LogError("Setup failed: ItemData is NULL");
        }
        else
        {
            Debug.Log("ItemData assigned correctly: " + data.name);
        }
    }
}