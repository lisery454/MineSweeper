using UnityEngine;

namespace MineSweeper {
    public interface IInputState {
        IInputState OnLeftMouseDown();
        IInputState OnRightMouseDown();
        IInputState OnLeftMouseUp();
        IInputState OnRightMouseUp();
    }

    public class NormalInputState : IInputState {
        public IInputState OnLeftMouseDown() {
            return new LeftMouseInputState();
        }

        public IInputState OnRightMouseDown() {
            return new RightMouseInputState();
        }

        public IInputState OnLeftMouseUp() {
            Debug.LogError("NormalInputState 不能 LeftMouseUp");
            return this;
        }

        public IInputState OnRightMouseUp() {
            Debug.LogError("NormalInputState 不能 RightMouseUp");
            return this;
        }
    }

    public class LeftMouseInputState : IInputState {
        public IInputState OnLeftMouseDown() {
            Debug.LogError("LeftMouseInputState 不能 LeftMouseDown");
            return this;
        }

        public IInputState OnRightMouseDown() {
            return new LeftRightMouseInputState();
        }

        public IInputState OnLeftMouseUp() {
            return new NormalInputState();
        }

        public IInputState OnRightMouseUp() {
            Debug.LogError("LeftMouseInputState 不能 RightMouseUp");
            return this;
        }
    }

    public class RightMouseInputState : IInputState {
        public IInputState OnLeftMouseDown() {
            return new LeftRightMouseInputState();
        }

        public IInputState OnRightMouseDown() {
            Debug.LogError("RightMouseInputState 不能 RightMouseDown");
            return this;
        }

        public IInputState OnLeftMouseUp() {
            Debug.LogError("RightMouseInputState 不能 LeftMouseUp");
            return this;
        }

        public IInputState OnRightMouseUp() {
            return new NormalInputState();
        }
    }

    public class LeftRightMouseInputState : IInputState {
        public IInputState OnLeftMouseDown() {
            Debug.LogError("LeftRightMouseInputState 不能 LeftMouseDown");
            return this;
        }

        public IInputState OnRightMouseDown() {
            Debug.LogError("LeftRightMouseInputState 不能 RightMouseDown");
            return this;
        }

        public IInputState OnLeftMouseUp() {
            return new RightMouseInputState();
        }

        public IInputState OnRightMouseUp() {
            return new LeftMouseInputState();
        }
    }
}