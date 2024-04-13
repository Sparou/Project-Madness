using Pathfinding;
using UnityEngine;

[RequireComponent(typeof(AIPath))]
[RequireComponent(typeof(Patrol))]
[RequireComponent(typeof(Enemy))]
public class AIPatrolling : MonoBehaviour
{
    #region Serilaized Variables
    [SerializeField] float patrollingSpeed;
    [SerializeField] float patrollingDelay;
    [SerializeField] Transform[] patrollingWaypoints;
    #endregion

    #region Private Variables
    private Enemy enemy;
    private AIPath aiPath;
    private Patrol patrol;
    #endregion

    void Start()
    {
        enemy = GetComponent<Enemy>();
        aiPath = GetComponent<AIPath>();
        patrol = GetComponent<Patrol>();

        patrol.delay = patrollingDelay;
        patrol.targets = patrollingWaypoints;
        patrol.enabled = false;
    }

    void FixedUpdate()
    {
        if (enemy.DistanceToTarget > enemy.AgressiveRadius)
        {
            aiPath.endReachedDistance = 0;
            aiPath.maxSpeed = patrollingSpeed;
            patrol.enabled = true;
        }
        else
        {
            patrol.enabled = false;
        }
    }
}
