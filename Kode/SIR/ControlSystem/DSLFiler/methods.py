import clr
clr.AddReference("ControlSystem")
import ControlSystem
clr.AddReference("System.Data")
import System.Data.SqlClient

# coordinates movement function
def moveByCoordinates(x,y,z):
        if (_robot.movebyCoordinates(x,y,z) == True):
                print "moved by coordinates succesful"
                return True
        else:
                print "moved by coordinates unsuccessfully"
                return False

def moveByAbsoluteCoordinates(x,y,z,pitch,roll):
        if (_robot.moveByAbsoluteCoordinates(x,y,z,pitch,roll) == True):
                print "moved by absolute coordinates succesful"
                return True
        else:
                print "moved by absolute coordinates successful"
                return False

def moveByRelativeCoordinates(x,y,z,pitch,roll):
        if (_robot.moveByRelativeCoordinates(x,y,z,pitch,roll) == True):
                print "moved by relative coordinates successful"
                return True
        else:
                print "moved by relative coordinates unsuccesful"
                return False

# gripper functions
def openGripper():
        if(_robot.openGripper() == True):
                print "gripper opened successfully"
                return True
        else:
                print "gripper failed to open"
                return False

def closeGripper():
        if (_robot.closeGripper() == True):
                print "gripper closed succesfully"
                return True
        else:
                print "gripper failed to close"
                return False

# standard robot functions

def Initialization():
        if (_robot.Initialization() == True):
                print "robot initialized successfully"
                return True
        else:
                print "robot failed to initialize"
                return False

def seekHome(): 
        if (_robot.homeRobot() == True):
                print "robot homed successfully"
                return True
        else:
                print "robot homed unsuccesfully"
                return False

def stopAllMovement():
        if (_robot.stopAllMovement() == True):
                print "movement stopped successfully"
                return True
        else:
                print "movement stopped unsuccessfully"
                return False

# axis movement functions
def moveBase(speed):
        if (_robot.moveBase(speed) == True):
                print "moving base with "+str(speed)+"% speed"
                return True
        else:
                print "failed to move base"
                return False

def moveShoulder(speed):
        if (_robot.moveShoulder(speed) == True):
                print "moving shoulder with "+str(speed)+"% speed"
                return True
        else:
                print "failed to move shoulder"
                return False

def moveElbow(speed):
        if (_robot.moveElbow(speed) == True):
                print "moving elbow with "+str(speed)+"% speed"
                return True
        else:
                print "failed to move elbow"
                return False

def moveWristRoll(speed):
        if (_robot.moveWristRoll(speed) == True):
                print "moving wrist with "+str(speed)+"% speed"
                return True
        else:
                print "failed to move wrist"
                return False

def moveGripper(speed):
        if (_robot.moveGripper(speed) == True):
                print "moving gripper with "+str(speed)+"% speed"
                return True
        else:
                print "failed to move gripper"
                return False

def moveConveyorBelt(speed):
        if (_robot.moveConveyerBelt(speed) == True):
                print "moving conveyor belt with "+str(speed)+"% speed"
                return True
        else:
                print "failed to move conveyor belt"
                return False

def isOnline():
        if (_robot.isOnline() == True):
                print "robot is online"
                return True
        else:
                print "robot is offline"
                return False

def insertBox(position_id,length,width,depth,weight):
        try:
                command = _sqlhandler.makeCommand("INSERT INTO BoxInfo VALUES ("+str(position_id)+","+str(length)+","+str(width)+","+str(depth)+","+str(weight)+")")
                _sqlhandler.runQuery(command,"write")
        except Exception, e:
                print "box could not be inserted"
                return False
        print "box inserted succesfully"
        return True

def removeBox(box_id):
        try:
                command = _sqlhandler.makeCommand("DELETE FROM BoxInfo WHERE BoxID = "+str(boxid))
                _sqlhandler.runQuery(command,"write")
        except Exception, e:
                print "box could not be removed, check box id"
                return False
        print "box removed succesfully"
        return True

def moveToCubePosition(cube_id):
        if (_robot.moveToCubePosition == True):
                print "moved succesfully to cube position"
                return True
        else:
                print "moved unsuccessfully / could not find cube with cube_id"
                return False


vector = [(10,10,10,10,10),(20,20,20,20,20)]

#def moveByRelativeVector(name,vector):
#        print "stay in school and remember to home the robot kids!"
#        if (_robot.defineRelativeVector(name,len(vector)) == True):
#                print "vector defined"
#                for element in vector:
#                        vec = ControlSystem.VecPoint(element[0],element[1],element[2],element[3],element[4])
#                        _robot.vectorlist[_robot.vectorlist.Count-1].addPoint(vec)
#                if (_robot.teach(_robot.vectorlist[_robot.vectorlist.Count-1]) == False):
#                        print "robot could not be taught"
#                if (_robot.moveLinear(name,1) == False):
#                        print "could not move robot linear"
#                return True
#        else:
#                print "relative vector didnt work"
#                return False
        
def Test():
	return _robot.moveToAPosition()	

def moveByAbsoluteVector(vector):
    return _robot.moveByAbsoluteVector(vector)

def defineAbsoluteVector(vector):
    return _robot.defineAbsoluteVector(vector)

def moveByXCoordinate(x):
    return _robot.moveByXCoordinate(x)

def moveByYCoordinate(y):
    return _robot.moveByYCoordinate(x)

def moveByZCoordinate(z):
    return _robot.moveByXCoordinate(z)

def moveByPitch(pitch):
    return _robot.moveByPitch(pitch)

def moveByRoll(roll):
    return _robot.moveByRoll(roll)

def getJawMilimeters():
    return _robot.getJawOpeningWidthMilimeters()

def defineRelativeVector(vector):
    return _robot.defineRelativeVector(vector)

def moveByRelativeVector(vector):
        return _robot.moveByRelativeVector(vector)
    
def getCurrentPosition():
    return _robot.getCurrentPositionAsString()

def getWeight():
    return _robot.getWeight()

def teach(vector):
    return _robot.teach(vector)

def moveLinear(vector, index):
    return _robot.moveLinear(vector, index)
