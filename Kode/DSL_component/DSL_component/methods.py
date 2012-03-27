import clr
clr.AddReference("DSL_component")
import DSL_component

def DummyHello(name):
	_robot.HelloName(name)

def SeekHome():
	return 0

def GoTo(x,y,z):
	return 0

def Goto(Position):
	return 0

def StartConveyorBelt():
	return 0

def WaitFor(arg):
	return 0

def OpenGripper():
	return 0

def CloseGripper():
	return 0


