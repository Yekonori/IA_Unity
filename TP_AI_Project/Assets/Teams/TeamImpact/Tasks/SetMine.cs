using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class SetMine : Action
	{
		TeamImpactController controller;

		public bool needToDropMine = true;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			controller.dropMine = needToDropMine;

			return TaskStatus.Success;
		}
	}
}