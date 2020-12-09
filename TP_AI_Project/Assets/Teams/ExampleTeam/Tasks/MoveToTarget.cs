using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class MoveToTarget : Action
	{
		
		public SharedVector3 target;
		public SharedVector3 currentPosition;
		public SharedVector2 velocity;
		TeamImpactController controller;


		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			Vector2 dir = (target.Value - currentPosition.Value).normalized;
			float angleToRotate = Vector2.SignedAngle(Vector2.right, dir - Vector2.Perpendicular(dir) * Vector2.Dot(Vector2.Perpendicular(dir), velocity.Value));

			controller.targetOrient = angleToRotate;

			if ((currentPosition.Value - target.Value).magnitude <= 1)
            {
				return TaskStatus.Success;
            }
			
			return TaskStatus.Running;
		}
	}
}