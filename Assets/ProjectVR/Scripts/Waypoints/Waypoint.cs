using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(MeshRenderer))]
public class Waypoint : MonoBehaviour {
    
    [System.Serializable]
    public class States
    {
        public bool occupied = false;
        public bool active = false;
        public bool focused = false;
        public bool triggered = false;
    }

    [System.Serializable]
    public class Colors
    {
        public Color active_color = new Color(0.0f, 1.0f, 0.0f, 0.5f);
        public Color hilight_color = new Color(0.8f, 0.8f, 1.0f, 0.125f);
        public Color disabled_color = new Color(0.125f, 0.125f, .125f, 0.0f);
    }

    [System.Serializable]
    public class Animations
    {
        public float animation_scale = 2.5f;
        public float animation_speed = 3.0f;
    }

    private Vector3 _origional_scale = Vector3.one;

    private float _hilight = 0.0f;
    private float _hilight_fade_speed = 0.25f;

    private Material _material;

    [System.Serializable]
    public class WaypointsSpecs
    {
        public Rigidbody rigid_body;
        public Vector3 position = Vector3.zero;

        public Waypoint[] neighborhood;
    }

    public States states;
    public Colors colors;
    public Animations animations;
    public WaypointsSpecs waypointsSpecs;


    void Awake()
    {
        waypointsSpecs.rigid_body = gameObject.GetComponent<Rigidbody>();
        _material = gameObject.GetComponent<MeshRenderer>().material;
        _origional_scale = transform.localScale;

        if (waypointsSpecs.position == Vector3.zero)
        {
            waypointsSpecs.position = gameObject.transform.position;
        }

        UpdateActivation();
    }


    void LateUpdate()
    {
        if (states.active && !states.occupied)
        {
            Animate();
        }
        else
        {
            if (states.active)
            {
                Deactivate();
            }
        }
    }


    public void UpdateActivation()
    {
        Deactivate();

        for (int i = 0; i < waypointsSpecs.neighborhood.Length; i++)
        {
            if (waypointsSpecs.neighborhood[i].states.occupied == true)
            {
                Activate();
            }
        }
    }


    public void Occupy()
    {
        states.occupied = true;
    }


    public void Depart()
    {
        states.occupied = false;
    }


    public void Activate()
    {
        _material.color = colors.active_color;
        transform.localScale = _origional_scale;

        states.active = true;

        GetComponent<BoxCollider>().enabled = true;
        GetComponent<MeshRenderer>().enabled = true;
    }


    public void Deactivate()
    {
        GetComponent<MeshRenderer>().enabled = false;

        _material.color = colors.disabled_color;
        transform.localScale = _origional_scale * 0.5f;

        states.active = false;
        states.triggered = false;

        GetComponent<BoxCollider>().enabled = false;
    }


    public void Trigger()
    {
        if (states.focused && states.active && !states.occupied)
        {
            states.triggered = true;
            states.occupied = false;
            _hilight = 1.0f;
        }
    }


    public void Enter()
    {
        if (!states.focused && states.active)
        {
            states.focused = true;
            _hilight = .5f;
        }
    }


    public void Exit()
    {
        states.focused = false;
        _hilight = 1.0f;
    }


    private void Animate()
    {
        float pulse_animation = Mathf.Abs(Mathf.Cos(Time.time * animations.animation_speed));

        _material.color = Color.Lerp(colors.active_color, colors.hilight_color, _hilight);

        _hilight = Mathf.Max(_hilight - _hilight_fade_speed, 0.0f);

        Vector3 hilight_scale = Vector3.one * (_hilight + (states.focused ? 0.5f : 0.0f));

        transform.localScale = Vector3.Lerp(_origional_scale + hilight_scale, _origional_scale * animations.animation_scale + hilight_scale, pulse_animation);
    }
}
