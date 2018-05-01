using UnityEngine;

public class Navigation : MonoBehaviour {

    private GameObject player = null;
    public float speed = 0.05f;

    private Waypoint[] _waypoints;
    private Waypoint _currentWaypoint;


	void Start () {
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        _waypoints = FindAll();
        _currentWaypoint = Nearest();
        _currentWaypoint.Occupy();

        player.transform.position = _currentWaypoint.waypointsSpecs.position;

        UpdateAll();
    }

	void Update () {
        if (_waypoints.Length > 0)
        {
            //if so, check all the waypoints to see if one of them has been hit
            for (int i = 0; i < _waypoints.Length; i++)
            {
                //if a waypoint has been hit, it's an active waypoint, and the person is pressing the trigger, activate it
                if (_waypoints[i].states.triggered)
                {
                    //exit the current waypoint
                    _currentWaypoint.Depart();
                    //set the current waypoint to be the new waypoint
                    _currentWaypoint = _waypoints[i];
                    //update all the waypoints to reflect their new active/inactive status
                    UpdateAll();
                }
            }

            //if the current waypoint isn't occupied (ie, it has been changed) and we aren't already on it, move towards it
            if (_currentWaypoint.states.occupied == false && player.transform.position != _currentWaypoint.waypointsSpecs.position)
            {
                MoveTo(_currentWaypoint);
            }
        }
    }

    public Waypoint[] FindAll()
    {
        GameObject[] waypoint_object = GameObject.FindGameObjectsWithTag("Waypoint");
        Waypoint[] waypoints = new Waypoint[waypoint_object.Length];

        for (int i = 0; i < waypoint_object.Length; i++)
        {
            waypoints[i] = waypoint_object[i].GetComponent<Waypoint>();
        }

        return waypoints;
    }

    public Waypoint Nearest()
    {
        int nearest_waypoint_index = 0;
        float distance_to_nearest = float.PositiveInfinity;

        for (int i = 0; i < _waypoints.Length; i++)
        {
            float distance_to_waypoint = Vector3.Distance(player.transform.position, _waypoints[i].waypointsSpecs.position);

            if (distance_to_waypoint < distance_to_nearest)
            {
                nearest_waypoint_index = i;
                distance_to_nearest = distance_to_waypoint;
            }
        }

        return _waypoints[nearest_waypoint_index];
    }

    public void UpdateAll()
    {
        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i].UpdateActivation();
        }
    }

    // Movement methods.
    public void MoveTo(Waypoint waypoint)
    {
        float distance = Vector3.Distance(player.transform.position, waypoint.waypointsSpecs.position);

        if (distance > 0.05f)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, waypoint.waypointsSpecs.position, speed);
        }
        else
        {
            player.transform.position = waypoint.waypointsSpecs.position;
            _currentWaypoint.Occupy();
            UpdateAll();
        }
    }
}
