/** \file IWrapper.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DSL
{
    public interface IWrapper
    {
        /// <summary>
        /// Initializes the robot.
        /// 
        /// Note: Should wait for it to be done before calling other functions.
        /// </summary>
        /// <param name="_shrtMode">Mode.(Use one of constants[Normally use online mode])</param>
        /// <param name="_shrtType">Type of connection.(Use one of constants[Normally use default])</param>
        /// <param name="_funcptrSuccess">Function to be called on success.</param>
        /// <param name="_funcptrError">Function to be called on error.</param>
        /// <returns>Returns true on successful call.(But errors can still happen)</returns>
        bool initializationWrapped(Wrapper.enumSystemModes _sysmodeMode, Wrapper.enumSystemTypes _systypeType, DLL.DgateCallBack _funcptrSuccess, DLL.DgateCallBack _funcptrError);

        /// <summary>
        /// Turns control on and off for certain axis group.
        /// </summary>
        /// <param name="bAxis">Axis group to affect.(Use enum)</param>
        /// <param name="_bControlOnOrOff">To have it turned off or on.</param>
        /// <returns>Returns true on successful call.</returns>
        bool controlWrapped(Wrapper.enumAxisSettings _axisSettingsGroup, bool _bControlOnOrOff);

        /// <summary>
        /// Tells about the robot being online.
        /// </summary>
        /// <returns>Returns true if it is, false otherwise.</returns>
        bool isOnlineOkWrapped();

        /// <summary>
        /// Homes a axis group.
        /// Should be called before calling most movement functions.
        /// </summary>
        /// <param name="_axisSettingsGroup">The axis group.(Use enum)</param>
        /// <param name="_funcptrHomingEventHandler">Function to be called for homing events.
        /// 
        /// Values being passed in event:
        ///     0xff: Homing started
        ///     1 - 8: Axis n being homed.
        ///     0x40: Homing ended.</param>
        /// <returns>Returns true on successful call.</returns>
        bool homeWrapped(Wrapper.enumAxisSettings _axisSettingsGroup, DLL.DgateCallBackByteRefArg _funcptrHomingEventHandler);

        /// <summary>
        /// Must be called to use manual movement. 
        /// Seems to stop previous movement of any object(Axis) that was moving before.
        /// </summary>
        /// <param name="_enummanMoveType">What to move by.(Axis(0), Coordinates(1))</param>
        /// <returns>Returns true on successful call.</returns>
        bool enterManualWrapped(Wrapper.enumManualType _enummanMoveType);

        /// <summary>
        /// Stops manual mode.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        bool closeManualWrapped();

        /// <summary>
        /// Moves the robot.
        /// homeWrapped must have been called if moving by coordinates.
        /// enterManual seems have to be called before each call to this function.
        /// Use stopWrapped to stop motion afterwards.(Moving some other part of the system also stops the previous movement, since the system can only handle one object(Axis) moving at a time.)
        /// </summary>
        bool moveManualWrapped(Wrapper.enumManualModeWhat _enumWhatToMove, int _lSpeed);

        /// <summary>
        /// Stops movement of axis.
        /// </summary>
        /// <param name="_bWhatToStop">Axis to stop.</param>
        /// <returns>Returns true on successful call.</returns>
        bool stopWrapped(Wrapper.enumAxisSettings _bWhatToStop); /// \todo Refactor.
           
        /// <summary>
        /// Flytter robotten til et punkt.
        /// </summary>
        /// <param name="_sNameOfVector">Navnet på vektoren i robotten.</param>
        /// <param name="_iIndex">Index for punkt.</param>
        /// <returns>Returns true on successfull call.</returns>
        bool moveLinearWrapped(string _sNameOfVector, int _iIndex);

        /// <summary>
        /// Opens the gripper.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        bool openGripperWrapped();

        /// <summary>
        /// Closes the gripper.
        /// </summary>
        /// <returns>Returns true on successful call.</returns>
        bool closeGripperWrapped();

        /// <summary>
        /// Gives information about how much open the gripper is.(Between the 'fingers')
        /// 
        /// Note: Probably most useful to use the _shrtWidth arg.
        /// </summary>
        /// <param name="_shrtPerc">Data in percentage.</param>
        /// <param name="_shrtWidth">Data in width.(mm)</param>
        /// <returns>Returns true on successful call.</returns>
        bool getJawWrapped(ref short _shrtPerc, ref short _shrtWidth);

        /// <summary>
        /// Adds functions to be called when motion starts and motion ends. 
        /// 
        /// Note: Ignoring return value.
        /// </summary>
        /// <param name="_funcptrCallbackEnd">Function to be called when motion has ended.</param>
        /// <param name="_funcptrCallbackStart">Function to be called when motion has started.</param>
        void watchMotionWrapped(DLL.DgateCallBackCharArg _funcptrCallbackEnd, DLL.DgateCallBackCharArg _funcptrCallbackStart);

        /// <summary>
        /// Adds a function to be called when digital input changes.
        /// </summary>
        /// <param name="_funcptrCallbackEvent">The function to be called.</param>
        /// <returns>Returns true if successful call.</returns>
        bool watchDigitalInputWrapped(DLL.DgateCallBackLongArg _funcptrCallbackEvent);

        /// <summary>
        /// Stops watching of digital inputs.
        /// 
        /// Note: Probably means no more events.
        /// </summary>
        /// <returns>Returns true if successful call.</returns>
        bool closeWatchDigitalInputWrapped();

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
        bool defineVectorWrapped(Wrapper.enumAxisSettings _enumGroup, string _sVectorName, short _shrtLength);

        /// <summary>
        /// Add the vector points to the vector with the same name. 
        /// 
        /// Note: Should call 'defineVectorWrapped' first.
        /// </summary>
        /// <param name="vecTheSirVector">The vector with the points.</param>
        /// <returns>Returns true on succeessfull call.</returns>
        bool teachWrapped(SIRVector vecTheSirVector);

        /// <summary>
        /// Returns the position of the robot.
        /// 
        /// </summary>
        /// <returns>Returns current position.</returns>
        VecPoint getCurrentPosition();

        byte axisSettingsToByte(Wrapper.enumAxisSettings axisSettingsArg);
        byte manualMovementToByte(Wrapper.enumManualModeWhat enumArg);
    }
}
