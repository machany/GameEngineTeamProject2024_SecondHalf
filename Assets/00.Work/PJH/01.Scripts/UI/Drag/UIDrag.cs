using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class UIDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private Vector3 _defaultPos;
    
    public void OnBeginDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = false;
        
        _defaultPos = transform.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }
    
    public void OnEndDrag(PointerEventData eventData)
    {
        GetComponent<Image>().raycastTarget = true;
        
        transform.position = _defaultPos;
    }
}
