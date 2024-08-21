using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DetectionZone : MonoBehaviour
{
    public UnityEvent<Collider2D> onDetected;
    public UnityEvent<Collider2D> onLost;
    public UnityEvent noCollidersDetected;
    [SerializeField] private List<Collider2D> _detectedColliders = new List<Collider2D>();
    Collider2D _collider;

    void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        _detectedColliders.Add(other);

        onDetected?.Invoke(other);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _detectedColliders.Remove(other);

        onLost?.Invoke(other);

        if(_detectedColliders.Count <= 0)
        {
            noCollidersDetected?.Invoke();
        }
    }

    public bool IsDetected(Collider2D other)
    {
        return _detectedColliders.Contains(other);
    }
}
