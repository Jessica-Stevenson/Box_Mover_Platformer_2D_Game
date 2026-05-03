using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryHover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject inventoryPanel;

    private static bool tabHover;
    private static bool panelHover;

    void Start()
    {
        inventoryPanel.SetActive(false);
    }

    void Update()
    {
        if (!tabHover && !panelHover && !IsHoveringAnyUI())
        {
            inventoryPanel.SetActive(false);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tabHover = true;
        inventoryPanel.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tabHover = false;
    }

    public static void SetPanelHover(bool value)
    {
        panelHover = value;
    }

    private bool IsHoveringAnyUI()
    {
        return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
    }
}