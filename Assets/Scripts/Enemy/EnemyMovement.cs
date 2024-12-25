using UnityEngine;

namespace Enemies
{
    public class EnemyMovement : MonoBehaviour
    {
        public void Move(Transform target, float moveSpeed)
        {
            //transform.position = Vector3.MoveTowards(transform.position, target.position, moveSpeed * Time.deltaTime);
        }
    }
}
