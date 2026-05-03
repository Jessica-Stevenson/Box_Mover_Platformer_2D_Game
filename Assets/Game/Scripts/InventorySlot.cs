using UnityEngine;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        if (transform.childCount > 0)
            return;

        GameObject dropped = eventData.pointerDrag;
        if (dropped == null) return;

        DraggableItem draggableItem = dropped.GetComponent<DraggableItem>();

        if (draggableItem == null)
        {
            draggableItem = dropped.GetComponentInParent<DraggableItem>();
        }

        if (draggableItem != null)
        {
            draggableItem.parentAfterDrag = transform;
            return;
        }


        DraggableBlock block = dropped.GetComponent<DraggableBlock>();

        if (block == null)
        {
            block = dropped.GetComponentInParent<DraggableBlock>();
        }

        if (block != null)
        {
            if (block.itemData == null)
            {
                Debug.LogError("Block is missing ItemData!");
                return;
            }

            ItemData data = block.itemData;

            Destroy(dropped);

            GameObject itemObj = Instantiate(data.item, transform);
            DraggableItem newItem = itemObj.GetComponent<DraggableItem>();

            if (newItem == null)
            {
                Debug.LogError("Spawned item prefab is missing DraggableItem component!");
                return;
            }

            newItem.Setup(data);
        }
    }
}