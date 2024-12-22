using UnityEngine;

public class ArrowSpawner : MonoBehaviour
{
    [SerializeField] private Pool<Arrow> _pool;
    [SerializeField] private ArrowData _arrowData;

    public Arrow Spawn(Transform transform, Quaternion quaternion)
    {
        Arrow arrow = _pool.Get(transform, quaternion);
        arrow.gameObject.SetActive(true);
        arrow.Init(_arrowData.ArrowFlightSpeed, _arrowData.AttackRadius, _pool);

        return arrow;
    }
}