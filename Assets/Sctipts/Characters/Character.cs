using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class Character : MonoBehaviour
{
    [SerializeField] private string characterName;
    [SerializeField] private LayerMask characterLayerMask;
    public LayerMask CharacterLayerMask => characterLayerMask;

    public HealthController healthController;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
    }
}
