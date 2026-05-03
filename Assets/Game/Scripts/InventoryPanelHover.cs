using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanelHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        InventoryHover.SetPanelHover(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        InventoryHover.SetPanelHover(false);    
    }
}