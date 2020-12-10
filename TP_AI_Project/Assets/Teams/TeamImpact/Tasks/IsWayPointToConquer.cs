using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class IsWayPointToConquer : Conditional
	{
		TeamImpactController controller;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			if (controller.hasWaypointToConquest.Value)
            {
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}