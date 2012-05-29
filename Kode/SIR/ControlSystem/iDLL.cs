/** \file iDLL.cs */
/** \author Robotic Global Organization(RoboGO) */

using System.Runtime.InteropServices;

namespace ControlSystem
{
    /// <summary>
    /// Interface for the functions in the USBC.dll files, in C# format.
    /// </summary>
    public interface IDLL
    {
        #region Interfaced DLL's

        // -Robot functions

        /// Initializes the robot.
        /// 
        /// Note: Should wait for it to be done before calling other functions.
        /// </summary>
        /// <param name="_shrtMode">Mode.(Use one of constants[Normally use online mode])</param>
        /// <param name="_shrtType">Type of connection.(Use one of constants[Normally use default])</param>
        /// <param name="_funcprtCallBack">Function to be called on success.</param>
        /// <param name="_funcptrCallBackError">Function to be called on error.</param>
        /// <returns>Returns 1 on successful call.(But errors can still happen)</returns>
        int Initialization(short _shrtMode, short _shrtType, DLL.DgateCallBack _funcprtCallBack, DLL.DgateCallBack _funcptrCallBackError);


        /// <summary>
        /// Turns control on and off for certain axis group.
        /// </summary>
        /// <param name="bAxis">Axis group to affect.(Use enum)</param>
        /// <param name="_bIsOn">To have it turned off or on.</param>
        /// <returns>Returns 1 on successful call.</returns>
        int Control(byte _bAxis, bool _bIsOn);


        /// <summary>
        /// Homes a axis group.
        /// Should be called before calling most movement functions.
        /// </summary>
        /// <param name="_axis">The axis group.(Use enum)</param>
        /// <param name="_funcptrCallBack">Function to be called for homing events.
        /// 
        /// Values being passed in event:
        ///     0xff: Homing started
        ///     1 - 8: Axis n being homed.
        ///     0x40: Homing ended.</param>
        /// <returns>Returns 1 on successful call.</returns>
        int Home(byte _axis, DLL.DgateCallBackByteRefArg _funcptrCallBack);

        /// <summary>
        /// Opens the gripper.
        /// </summary>
        /// <returns>Returns 1 on successful call.</returns>
        int OpenGripper();


        /// <summary>
        /// Closes the gripper.
        /// </summary>
        /// <returns>Returns 1 on successful call.</returns>
        int CloseGripper();


        /// <summary>
        /// Gives information about how much open the gripper is.(Between the 'fingers')
        /// 
        /// Note: Probably most useful to use the _shrtWidth arg.
        /// </summary>
        /// <param name="_perc">Data in percentage.</param>
        /// <param name="_metric">Data in width.(mm)</param>
        /// <returns>Returns 1 on successful call.</returns>
        int GetJaw(ref short _perc, ref short _metric);


        /// <summary>
        /// Must be called to use manual movement. 
        /// Seems to stop previous movement of any object(Axis) that was moving before.
        /// </summary>
        /// <param name="_shrArg">What to move by.(Axis(0), Coordinates(1))</param>
        /// <returns>Returns 1 on successful call.</returns>
        int EnterManual(short _shrtArg);


        /// <summary>
        /// Stops manual mode.
        /// </summary>
        /// <returns>Returns 1 on successful call.</returns>
        int CloseManual();


        /// <summary>
        /// Moves the robot.
        /// homeWrapped must have been called if moving by coordinates.
        /// enterManual seems have to be called before each call to this function.
        /// Use stopWrapped to stop motion afterwards.(Moving some other part of the system also stops the previous movement, since the system can only handle one object(Axis) moving at a time.)
        /// <param name="__bAxis">Which Axis to move (0-7)</param>
        /// <param name="_lSpeed">The move speed of Axis 0-100%</param>
        /// <returns>Returns true on successful call.</returns>
        /// </summary>
        int MoveManual(byte _bAxis, int _lSpeed);


        /// <summary>
        /// Stops movement of axis.
        /// </summary>
        /// <param name="_axis">Axis to stop.</param>
        /// <returns>Returns 1 on successful call.</returns>
        int Stop(byte _axis);


        /// <summary>
        /// Adds functions to be called when motion starts and motion ends. 
        /// 
        /// Note: Ignoring return value.
        /// </summary>
        /// <param name="_funcptrCallbackEnd">Function to be called when motion has ended.</param>
        /// <param name="_funcptrCallbackStart">Function to be called when motion has started.</param>
        DLL.DgateCallBackByteRefArg WatchMotion(DLL.DgateCallBackByteRefArg _funcptrCallbackEnd, DLL.DgateCallBackByteRefArg _funcptrCallbackStart);


        /// <summary>
        /// Adds a function to be called when digital input changes.
        /// </summary>
        /// <param name="_funcptrCallbackEvent">The function to be called.</param>
        /// <returns>Returns 1 if successful call.</returns>
        int WatchDigitalInput(DLL.DgateCallBackLongArg _funcptrCallbackEvent);


        /// <summary>
        /// Stops watching of digital inputs.
        /// 
        /// Note: Probably means no more events.
        /// </summary>
        /// <returns>Returns 1 if successful call.</returns>
        int CloseWatchDigitalInput();


        /// <summary>
        /// Tells about the robot being online.
        /// </summary>
        /// <returns>Returns 1 if it is, 0 otherwise.</returns>
        int IsOnLineOk();


        /// <summary>
        /// Move the Robot to a specific point
        /// </summary>
        /// <param name="_sNameOfVectorThatGotPosition">Navnet på vektoren i robotten.</param>
        /// <param name="_shrtPointInVector">Index for punkt.</param>
        /// <returns>Returns true on successfull call.</returns>
        int MoveLinear([MarshalAs(UnmanagedType.LPStr)] string _sNameOfVectorThatGotPosition, short _shrtPointInVector, [MarshalAs(UnmanagedType.LPStr)] string _sSecondaryPos, short _shrtPointToMoveTo);


        /// <summary>
        /// Defines a new vector in robot memory.
        /// 
        /// Note: Good idea to have in program one of the SIRVector classes to contains vector information.
        /// </summary>
        /// <param name="_enumGroup">Group can use:
        ///     Robot(Normally used)
        ///     Peripherals
        ///     All</param>
        /// <param name="_sVectorName">Name of vector.</param>
        /// <param name="_shrtLength">Length of vector.(Number of points.)</param>
        /// <returns>Returns true on successfull call.</returns>
        int DefineVector(byte bGroup, [MarshalAs(UnmanagedType.LPStr)] string _sVectorName, short _shrtSizeOfVector);


        /// <summary>
        /// Add the vector points to the vector with the same name. 
        /// 
        /// Note: Should call 'defineVectorWrapped' first.
        /// </summary>
        /// <param name="_sVectorName">The vector with the points.</param>
        /// <returns>Returns true on succeessfull call.</returns>
        int Teach([MarshalAs(UnmanagedType.LPStr)] string _sVectorName, short _shrtPoint, int[] _iaPointInfo, short _shrtSizeOfArray, int _iPointType); // long types used in C++ functions.


        /// <summary>
        /// Get the current position by reference
        /// 
        /// </summary>
        /// <returns>Returns 1 if called</returns>
        int GetCurrentPosition(ref int[] _ibufEnc, ref int[] _ibufJoint, ref int[] _ibufXYZ);

        /// <summary>
        /// Set time for movements
        /// 
        /// </summary>
        /// <param name="_bGroup">Which joint to set time (& for all)</param>
        /// <param name="_mTime">Time in milisecond</param>
        /// <returns>Returns 1 if called</returns>
        int Time(byte _bGroup, long _mTime);

        /// <summary>
        /// Set speed for movements
        /// 
        /// </summary>
        /// <param name="_bGroup">Which joint to set time (& for all)</param>
        /// <param name="_mSpeed">speed from 0-100%</param>
        /// <returns>Returns 1 if called</returns>
        int Speed(byte _bGroup, long _mSpeed);
        #endregion
    }
}
