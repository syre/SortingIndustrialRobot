import clr
clr.AddReference("DSL")
import DSL
clr.AddReference("System.Data")
import System.Data.SqlClient

# coordinates movement function
def moveByCoordinates(x,y,z):
	if (_robot.movebyCoordinates(x,y,z) == True):
                print "moved by coordinates succesful"
        else:
                print "moved by coordinates unsuccessfully"

def moveByAbsoluteCoordinates(x,y,z,pitch,roll):
        if (_robot.moveByAbsoluteCoordinates(x,y,z,pitch,roll) == True):
                print "moved by absolute coordinates succesful"
        else:
                print "moved by absolute coordinates successful"

def moveByRelativeCoordinates(x,y,z,pitch,roll):
        if (_robot.moveByRelativeCoordinates(x,y,z,pitch,roll) == True):
                print "moved by relative coordinates successful"
        else:
                print "moved by relative coordinates unsuccesful"

# gripper functions
def openGripper():
	if(_robot.openGripper() == True):
                print "gripper opened successfully"
        else:
                print "gripper failed to open"

def closeGripper():
        if (robot.closeGripper() == True):
                print "gripper closed succesfully"
        else:
                print "gripper failed to close"

# standard robot functions

def Initialization():
	if (_robot.Initialization() == True):
                print "robot initialized successfully"
        else:
                print "robot failed to initialize"

def seekHome(): 
	if (_robot.homeRobot() == True):
                print "robot homed successfully"
        else:
                print "robot homed unsuccesfully"

def stopAllMovement():
        if (_robot.stopAllMovement() == True):
                print "movement stopped successfully"
        else:
                print "movement stopped unsuccessfully"

# axis movement functions
def moveBase(speed):
        if (_robot.moveBase(speed) == True):
                print "moving base with "+str(speed)+"% speed"
        else:
                print "failed to move base"

def moveShoulder(speed):
	if (_robot.moveShoulder(speed) == True):
                print "moving shoulder with "+str(speed)+"% speed"
        else:
                print "failed to move shoulder"

def moveElbow(speed):
	if (_robot.moveElbow(speed) == True):
                print "moving elbow with "+str(speed)+"% speed"
        else:
                print "failed to move elbow"

def moveWristRoll(speed):
	if (_robot.moveWristRoll(speed) == True):
                print "moving wrist with "+str(speed)+"% speed"
        else:
                print "failed to move wrist"

def moveGripper(speed):
	if (_robot.moveGripper(speed) == True):
                print "moving gripper with "+str(speed)+"% speed"
        else:
                print "failed to move gripper"

def moveConveyorBelt(speed):
	if (_robot.moveConveyerBelt(speed) == True):
                print "moving conveyor belt with "+str(speed)+"% speed"
        else:
                print "failed to move conveyor belt"

def isOnline():
	if (_robot.isOnline() == True):
                print "robot is online"
        else:
                print "robot is offline"

def insertBox(positionid,length,width,depth,weight):
        command = _sqlhandler.makeCommand("INSERT INTO BoxInfo VALUES ("+str(positionid)+","+str(length)+","+str(width)+","+str(depth)+","+str(weight)+")")
        _sqlhandler.runQuery(command,"write")
        print "box inserted succesfully"

def removeBox(boxid):
        command = _sqlhandler.makeCommand("DELETE FROM BoxInfo WHERE BoxID = "+str(boxid))
        _sqlhandler.runQuery(command,"write")
        print "box removed succesfully"
