import clr
clr.AddReference("ControlSystem")
import ControlSystem
clr.AddReference("System.Data")
import System.Data.SqlClient
clr.AddReference("SqlInteraction")
import SqlInteraction

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
        if (position_id == -1):
                print "box could not be found and therefore not inserted"
                return False
        try:
                command = _sqlhandler.makeCommand("INSERT INTO BoxInfo VALUES ("+str(position_id)+","+str(length)+","+str(width)+","+str(depth)+","+str(weight)+")")
                _sqlhandler.runQuery(command,"write")
        except Exception, e:
                print "box could not be inserted"
                return False
        print "box inserted succesfully"
        return True

def getPositionIDFromMeasurements(volume,weight):
		density = weight/(volume/1000)
		command = _sqlhandler.makeCommand("SELECT PositionID FROM Position Inner Join Category ON Position.CategoryID = Category.ID WHERE Position.Occupied = 'False' AND Category.MaxDensity >= " + str(density).replace(',','.') + " AND Category.MinDensity <= " + str(density).replace(',','.'));
		reader = _sqlhandler.runQuery(command, 'read')
		list = reader.readRow()
		reader.close()
		if (len(list) != 0):
			return list[0]
		else:
			return -1

def removeBox(box_id):
        try:
                command = _sqlhandler.makeCommand("DELETE FROM BoxInfo WHERE BoxID = "+str(boxid))
                _sqlhandler.runQuery(command,"write")
        except Exception, e:
                print "box could not be removed, check box id"
                return False
        print "box removed succesfully"
        return True

def moveToCubePosition(name,cube_id):
	if (_robot.moveToCubePosition(name,cube_id) == True):
		print "moved succesfully to cube position"
		command = _sqlhandler.makeCommand("UPDATE Position SET Position.Occupied='True' WHERE Position.PositionID=" + str(cube_id))
		_sqlhandler.runQuery(command,"write")
		return True
	else:
		print "moved unsuccessfully / could not find cube with cube_id"
		return False

def defineRelativeVectorFromTuple(name,tuple):
        print "stay in school and remember to home the robot kids!"
        vector = ControlSystem.RelCoordSirVector(name)
        for element in tuple:
                vec = ControlSystem.VecPoint(element[0],element[1],element[2],element[3],element[4])
                vector.addPoint(vec)
        if (_robot.defineVector(vector) == True):
                print "vector defined"
        else:
            print "vector could not be defined"
            return False
        if (teach(vector) == True):
                print "robot taught"
                return True
        else:
                print "robot could not be taught"
                return False
            
def defineAbsoluteVectorFromTuple(name,tuple):
        print "stay in school and remember to home the robot kids!"
        vector = ControlSystem.AbsCoordSirVector(name)
        for element in tuple:
                vec = ControlSystem.VecPoint(element[0],element[1],element[2],element[3],element[4])
                vector.addPoint(vec)
        if (_robot.defineVector(vector) == True):
                print "vector defined"
        else:
                print "vector could not be defined"
                return False
        if (teach(vector) == True):
                print "robot taught"
                return True
        else:
                print "robot could not be taught"
                return False
                
def Test():
	return _robot.moveToAPosition()	

def moveByAbsoluteVector(vector):
    return _robot.moveByAbsoluteVector(vector)

def defineVector(vector):
    return _robot.defineVector(vector)

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
