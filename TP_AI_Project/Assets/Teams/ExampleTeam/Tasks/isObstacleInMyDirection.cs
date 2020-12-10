using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class isObstacleInMyDirection : Conditional
	{
		
		public float maxDistanceToCheck = 10f;
		public SharedVector3 currentPosition;
		public SharedVector2 velocity;

		public override TaskStatus OnUpdate()
		{
			Vector2 startRay = new Vector2(currentPosition.Value.x, currentPosition.Value.y);

			if (velocity.Value.magnitude == 0) return TaskStatus.Failure;

			RaycastHit2D hit = Physics2D.Raycast(startRay, velocity.Value, maxDistanceToCheck, 12); //layer 12 = asteroids
			Debug.DrawRay(startRay, velocity.Value.normalized * maxDistanceToCheck, Color.red);

			if (hit.collider == null)
            {
				return TaskStatus.Failure;
            }
			return TaskStatus.Success;
		}
	}
}
