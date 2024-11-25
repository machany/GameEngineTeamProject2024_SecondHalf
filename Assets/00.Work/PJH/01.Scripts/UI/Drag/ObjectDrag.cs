using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class ObjectDrag : MonoBehaviour
{
    private Vector3 _defaultPos;

    private void OnMouseDown()
    {
        _defaultPos = transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 objectPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        objectPos.z = 0;

        transform.position = objectPos;
    }

    private void OnMouseUp()
    {
        transform.position = _defaultPos;
    }
}