using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class DestroyMineByShockwave : Action
	{
		TeamImpactController controller;
		public SharedBool isMineNearby;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			if (isMineNearby.Value)
				controller.fireShockWAve = true;

			return TaskStatus.Success;
		}
	}
}
