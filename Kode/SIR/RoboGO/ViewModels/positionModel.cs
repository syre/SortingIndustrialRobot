/** \file PositionModel.cs */
/** \author Robotic Global Organization(RoboGO) */

using DSL;

namespace RoboGO.ViewModels
{
    public class PositionModel
    {
        
        private VecPoint _positionVec;

        //Get and set Position as a VectPoint
        public VecPoint PositionVec
        {
            get { return _positionVec; }
            set 
            { _positionVec = value;
            }
        }

    }
}
