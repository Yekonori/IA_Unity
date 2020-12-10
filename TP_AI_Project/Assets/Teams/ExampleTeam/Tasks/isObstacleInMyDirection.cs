using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class isObstacleInMyDirection : Conditional
	{
		
		public float maxDistanceToCheck = 10f;
		public SharedVector3 currentPosition;
		public SharedVector3 closestWP;

		
		public override TaskStatus OnUpdate()
		{
			Vector2 startRay = new Vector2(currentPosition.Value.x, currentPosition.Value.y);
			Vector2 dirRay = new Vector2(closestWP.Value.x, closestWP.Value.y);
			//if (velocity.Value.magnitude == 0) return TaskStatus.Failure;

			LayerMask layerMask = LayerMask.GetMask("Asteroid");
			RaycastHit2D hit = Physics2D.Raycast(startRay, dirRay, maxDistanceToCheck, layerMask); //layer 12 = asteroids
			Debug.DrawRay(startRay, dirRay.normalized * maxDistanceToCheck, Color.red);

			if (hit.collider == null)
            {
				return TaskStatus.Failure;
            }
			Debug.Log(hit.collider.name);
			return TaskStatus.Success;
		}
	}
}
