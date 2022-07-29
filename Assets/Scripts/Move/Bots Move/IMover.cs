using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Move
{
    public interface IMover
    {
        float AddSpeed{ get; }
        void MoveRight();
        void MoveLeft();
    }
    public interface IMoverUP : IMover
    {
        void MoveUp();
    }
}