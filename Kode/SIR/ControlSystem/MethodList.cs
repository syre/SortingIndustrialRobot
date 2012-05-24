using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ControlSystem
{
    public class MethodList : List<string>
    {
        public MethodList()
        {
            Add("closeGripper()");
            Add("insertBox(positionid,length,width,depth,weight)");
            Add("isOnline()");
            Add("moveBase(speed)");
            Add("moveByAbsoluteCoordinates(x,y,z,pitch,roll)");
            Add("moveByCoordinates(x,y,z)");
            Add("moveByRelativeCoordinates(x,y,z,pitch,roll)");
            Add("moveConveyorBelt(speed)");
            Add("moveElbow(speed)");
            Add("moveGripper(speed)");
            Add("moveShoulder(speed)");
            Add("moveToCubePosition(cubeid)");
            Add("moveWristRoll(speed)");
            Add("openGripper()");
            Add("seekHome()");
            Add("stopAllMovement()");
            Add("removeBox(boxid)");
        }
    }
}
