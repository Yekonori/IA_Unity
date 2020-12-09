using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;

namespace TeamImpact
{
	public class IsEnemyNearestThanWaypoint : Conditional
	{
		TeamImpactController controller;

		public override void OnStart()
		{
			controller = GetComponent<TeamImpactController>();
		}

		public override TaskStatus OnUpdate()
		{
			SharedVector3 currentPosition = (SharedVector3)controller.BehaviorTree.GetVariable("currentPosition");
			SharedVector3 enemyPosition = (SharedVector3)controller.BehaviorTree.GetVariable("ennemyPosition");

			float distToEnemy = (currentPosition.Value - enemyPosition.Value).magnitude;
			float distToClosestWP = (currentPosition.Value - controller.ClosestWP.transform.position).magnitude;

            if (distToEnemy > distToClosestWP)
            {
				controller.isChassingEnemy = false;
                return TaskStatus.Failure;
            }

			controller.isChassingEnemy = true;
			return TaskStatus.Success;
		}
	}
}