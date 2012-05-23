/** \file PositionViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */

using ControlSystem;

namespace RoboGO.ViewModels
{
    public class PositionViewModel
    {
        private PositionModel _positionModel;

        /// <summary>
        /// PositionViewModel classconstructor
        /// </summary>
        public PositionViewModel()
        {
            _positionModel = new PositionModel();
        }

        /// <summary>
        /// PositionViewModel explixitconstructor
        /// </summary>
        public PositionViewModel(PositionModel _pm)
        {
            _positionModel = _pm;
        }

        /// <summary>
        /// Update function, getting the current position from the current IRobotinstance. Set the Position as vectpoint in PositionModel
        /// </summary>
        public void update()
        {
            _positionModel.PositionVec = Factory.currentIRobotInstance.getCurrentPosition();
        }

        public void update(VecPoint _vect)
        {
            _positionModel.PositionVec = _vect;
        }

        /// <summary>
        /// Get the current position
        /// </summary>
        /// <returns>String of position Vectpoint</returns>
        public string getXYZPR()
        {
            return _positionModel.PositionVec.ToString();
        }
    }
}
