import clr
clr.AddReference("DSL_component")
import DSL_component

def Initialize(mode, shrtType):
	return initializationWrapped(mode,type)

def GetParameterFolder(folder):
	return getParameterFolderWrapped(folder)

def SetParameterFolder(folder):
	return setParameterFolderWrapped(folder)

def Control(axisGroup, IsOn):
	return controlWrapped(axisGroup,IsOn)

def Home(axisGroup):
	return homeWrapped(axisGroup)

def DummyHello(name):
	HelloName(name)

