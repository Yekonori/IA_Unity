using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DoNotModify;
//
using BehaviorDesigner.Runtime;
using System.Linq;

namespace TeamImpact {

	public class TeamImpactController : BaseSpaceShipController
	{
		BehaviorTree behaviorTree;

		List<WayPoint> waypoints;
		List<WayPoint> myWaypoints;
		List<WayPoint> ennemyWaypoints;
		List<WayPoint> waypointsNotOwn;
		[SerializeField] WayPoint closestWP = null;


		// Variables to modify
		public float thrust = 0.5f;
		float targetOrient = 0f;


		private void Start()
        {
			behaviorTree = GetComponent<BehaviorTree>();
        }

        public override void Initialize(SpaceShip spaceship, GameData data)
		{
			
		}

		public override InputData UpdateInput(SpaceShip spaceship, GameData data)
		{
			waypoints = data.WayPoints;
			myWaypoints = waypoints.FindAll((wp) => wp.Owner == spaceship.Owner);
			ennemyWaypoints = waypoints.FindAll((wp) => wp.Owner != spaceship.Owner && wp.Owner != -1); //waypoints[0].Owner == -1 si pas contest
			waypointsNotOwn = waypoints.Except(myWaypoints).ToList();

			float minDistance = 1000f;
			// try to go to closest wp not own
			foreach (WayPoint wp in waypointsNotOwn)
            {
				float distance = (spaceship.transform.position - wp.transform.position).magnitude;
				if (distance < minDistance)
                {
					minDistance = distance;
					closestWP = wp;
				}
            }
			Vector2 dir = Vector2.zero;
			if (closestWP != null)
            {
				dir = (closestWP.transform.position - spaceship.transform.position).normalized;
            }

			targetOrient = Vector2.SignedAngle(Vector2.right, dir -  Vector2.Perpendicular(dir) * Vector2.Dot(Vector2.Perpendicular(dir), spaceship.Velocity));
			return new InputData(thrust, targetOrient, false, false, false);
		}

	}


}
