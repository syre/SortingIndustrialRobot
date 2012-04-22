using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSL
{
    public class PositionModel
    {
        
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Z { get; private set; }
        public int Pitch { get; private set; }
        public int Roll { get; private set; }
        private VecPoint _positionVec;

        public VecPoint PositionVec
        {
            get { return _positionVec; }
            set 
            { _positionVec = value;
              setCordinates();  
            }
        }

        private void setCordinates()
        {
            X = PositionVec.iX;
            Y = PositionVec.iY;
            Z = PositionVec.iZ;
            Pitch = PositionVec.iPitch;
            Roll = PositionVec.iRoll;
        }
    }
}
