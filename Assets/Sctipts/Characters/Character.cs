using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class Character : MonoBehaviour
{
    [SerializeField] public string characterName;
    [SerializeField] private LayerMask characterLayerMask;
    [SerializeField] private int _humanPoints;
    public LayerMask CharacterLayerMask => characterLayerMask;

    public HealthController healthController;

    private void Start()
    {
        healthController = GetComponent<HealthController>();
    }

    public void ChangePoints(int ammount)
    {
        this._humanPoints = ammount;
    }

}
