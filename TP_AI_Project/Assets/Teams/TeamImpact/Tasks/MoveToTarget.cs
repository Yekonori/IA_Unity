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
		public SharedFloat zAngle;
		TeamImpactController controller;

		float maxSpeed;
		public float actualAngle = 0f;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
			maxSpeed = controller.shipMaxSpeed;
		}

		public override TaskStatus OnUpdate()
		{
			Vector2 dir = (target.Value - currentPosition.Value).normalized;
			float angleToRotate = Vector2.SignedAngle(Vector2.right, dir - Vector2.Perpendicular(dir) * Vector2.Dot(Vector2.Perpendicular(dir), velocity.Value));

			controller.targetOrient = angleToRotate;

            float power = 1f;
            Vector2 forward = new Vector2(Mathf.Cos(Mathf.PI * zAngle.Value / 180f), Mathf.Sin(Mathf.PI * zAngle.Value / 180f));
            actualAngle = Vector2.SignedAngle(forward, dir - Vector2.Perpendicular(dir) * Vector2.Dot(Vector2.Perpendicular(dir), velocity.Value));
            //if (Mathf.Abs(actualAngle) > 60f)
            //{
            //    power = 0f;
            //}
            //if (Mathf.Abs(actualAngle) > 30f)
            //{
            //    power = 0.5f;
            //}
           /* else*/ if (Mathf.Abs(actualAngle) < 1f && velocity.Value.magnitude == maxSpeed && Vector2.Dot(velocity.Value.normalized, dir.normalized) > 0.99f)
            {
                power = 0f;

            }
            controller.thrust = power;

            if ((currentPosition.Value - target.Value).magnitude <= 1)
            {
				return TaskStatus.Success;
            }
			
			return TaskStatus.Running;
		}
	}
}