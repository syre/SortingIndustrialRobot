/** \file PositionModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using ControlSystem;

namespace RoboGO.ViewModels
{
    /// <summary>
    /// Class to keep track of simulator position.
    /// </summary>
    public class PositionModel
    {
        private VecPoint _positionVec;

        /// <summary>
        /// Get and set Position as a VectPoint
        /// </summary>
        public VecPoint PositionVec
        {
            get { return _positionVec; }
            set 
            { 
                _positionVec = value;
            }
        }
    }
}
