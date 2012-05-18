/** \file PositionViewModel.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSL
{
    public class PositionViewModel
    {
        private PositionModel _positionModel;

        public void update(VecPoint _vec)
        {
            _positionModel.PositionVec = _vec;
        }

        public PositionViewModel()
        {
            _positionModel = new PositionModel();
        }

        public PositionViewModel(PositionModel _pm)
        {
            _positionModel = _pm;
        }

        public string XYZ
        {
            get { return getXYZ(); }
        }

        public string XYZPR
        {
            get { return getXYZPR(); }

        }

        private string getX()
        {
            return _positionModel.X.ToString();
        }

        private string getY()
        {
            return _positionModel.Y.ToString();
        }

        private string getZ()
        {
            return _positionModel.Z.ToString();
        }

        private string getPitch()
        {
            return _positionModel.Pitch.ToString();
        }

        private string getRoll()
        {
            return _positionModel.Roll.ToString();
        }

        public string getXYZ()
        {
            return getX() + ";" + getY() + ";" + getZ();
        }

        public string getXYZPR()
        {
            return getX() + ";" + getY() + ";" + getZ() + ";" + getPitch() + ";" + getRoll();
        }
    }
}
