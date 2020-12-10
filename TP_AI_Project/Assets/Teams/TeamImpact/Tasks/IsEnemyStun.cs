using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class IsEnemyStun : Conditional
	{
		TeamImpactController controller;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			SharedBool isEnemyStun = (SharedBool)controller.BehaviorTree.GetVariable("isEnnemyStun");

			if (isEnemyStun.Value)
            {
				return TaskStatus.Success;
			}

			return TaskStatus.Failure;
		}
	}
}
