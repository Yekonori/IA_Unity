using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class SetTarget : Action
	{
		TeamImpactController controller;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			controller.BehaviorTree.SetVariableValue("closestWP", controller.ClosestWP.transform.position);

			return TaskStatus.Success;
		}
	}
}