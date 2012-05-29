import clr
clr.AddReference("DSL")
import DSL

def DummyHello(name):
	_robot.HelloName(name)

def GoTo(x,y,z):
	return 0

def Goto(Position):
	return 0

def WaitFor(arg):
	return 0

# coordinates movement function
def moveByCoordinates(x,y,z):
	return _robot.moveByCoordinates(x,y,z)

def moveByAbsoluteCoordinates(x,y,z,pitch,roll):
	return _robot.moveByAbsoluteCoordinates(x,y,z,pitch,roll)

def moveByRelativeCoordinates(x,y,z,pitch,roll):
	return _robot.moveByRelativeCoordinates(x,y,z,pitch,roll)

# gripper functions
def OpenGripper():
	return _robot.openGripper()

def CloseGripper():
	return _robot.closeGripper()

# standard robot functions

def Initialization():
	return	_robot.Initialization()

def seekHome():
	return _robot.homeRobot()

def stopAllMovement():
	return _robot.stopAllMovement()

# axis movement functions
def moveBase(speed):
	return _robot.moveBase(speed)

def moveShoulder(speed):
	return _robot.moveShoulder(speed)

def moveElbow(speed):
	return _robot.moveElbow(speed)

def moveWristRoll(speed):
	return _robot.moveWristRoll(speed)

def moveGripper(speed):
	return _robot.moveGripper(speed)

def moveConveyorBelt(speed):
	return _robot.moveConveyerBelt(speed)

def isOnline():
	return _robot.isOnline()
