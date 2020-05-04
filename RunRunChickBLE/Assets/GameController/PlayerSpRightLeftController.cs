using Dreamteck.Splines;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpRightLeftController : MonoBehaviour
{
    public SplineFollower _this;

    // Awake is called before the first frame update
    void Awake()
    {
        if (_this == null) { _this = this.GetComponent<SplineFollower>(); }
    }

    // Start is called before the first frame update
    void Start()
    {
        _this.motion.applyPositionX = true;
        _this.motion.applyPositionZ = true;
        _this.motion.applyPositionY = false;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetAxisRaw("Horizontal") == 1)&&(_this.motion.offset.x<1.5f))
        {
            _this.motion.offset = new Vector2(_this.motion.offset.x + Time.deltaTime*5, 0);
        }
        if ((Input.GetAxisRaw("Horizontal") == -1)&& (_this.motion.offset.x > -1.5f))
        {
            _this.motion.offset = new Vector2(_this.motion.offset.x - Time.deltaTime*5, 0);
        }
        else
        {

        }

    }
}
