using System;
using System.Collections.Generic;
using UnityEngine;

namespace RitualRhythm
{
    public class ResponsiveButtonDirectionalInput {
        private readonly string _up;
        private readonly string _down;
        private readonly string _left;
        private readonly string _right;

        private readonly IList<float> _horizontalInputs = new List<float>();
        private readonly IList<float> _verticalInputs = new List<float>();

        public ResponsiveButtonDirectionalInput(string up, string down, string left, string right) {
            _up = up;
            _down = down;
            _left = left;
            _right = right;
        }

        public float Horizontal {
            get {
                return _horizontalInputs.Count > 0
                        ? _horizontalInputs[_horizontalInputs.Count - 1]
                        : 0;
            }
        }

        public float Vertical {
            get {
                return _verticalInputs.Count > 0
                        ? _verticalInputs[_verticalInputs.Count - 1]
                        : 0;
            }
        }

        public void Update() {
            if (Input.GetButtonDown(_up) && !_verticalInputs.Contains(1f)) {
                _verticalInputs.Add(1f);
            }

            if (Input.GetButtonDown(_down) && !_verticalInputs.Contains(-1f)) {
                _verticalInputs.Add(-1f);
            }

            if (Input.GetButtonDown(_right) && !_horizontalInputs.Contains(1f)) {
                _horizontalInputs.Add(1f);
            }

            if (Input.GetButtonDown(_left) && !_horizontalInputs.Contains(-1f)) {
                _horizontalInputs.Add(-1f);
            }

            if (Input.GetButtonUp(_up)) {
                _verticalInputs.Remove(1f);
            }

            if (Input.GetButtonUp(_down)) {
                _verticalInputs.Remove(-1f);
            }

            if (Input.GetButtonUp(_right)) {
                _horizontalInputs.Remove(1f);
            }

            if (Input.GetButtonUp(_left)) {
                _horizontalInputs.Remove(-1f);
            }
        }
    }
}
