import time
# GAMMEL 1st position 350132, 58727, 171901, -91863, 12974)

tuple = [(325033,68057,170847,-91541,15017),
	(325033,68057, 117367, -91935, 13010),
	(325033,68057, 171901, -91863, 12974),
	(325033,68057, 171901, -91720, 103189),
	(325033,68057, 121557, -91684, 103154),
	(350855, 58759, 292952, -91612, 103082),
	(18637, 395314, 280653, -91720, 181218),
	(5729, 391412, 71729, -1111, 183655),
	(7736, 390586, 275364, -16093, 183154),
	(4299, 397196, 130235,-89175, 277455),
	(4298, 397121, 71287, -89175, 277455),
	(18637, 395314, 280653, -91720, 181218),
         (317669,-2071,348378,-90215,-179),
         (-35939, -201917, 222128, -91577, 174695)]

defineAbsoluteVectorFromTuple("AutoSort",tuple)

# DISABLE BUILT-IN TIMER...!
if (_robot.cubeAtConveyorBelt == False):
	moveConveyorBelt(-50)
while(_robot.cubeAtConveyorBelt == False):
	time.sleep(0.05)		
stopAllMovement()

moveLinear("AutoSort", 1)
openGripper()
moveLinear("AutoSort", 2)
closeGripper()
time.sleep(2.5)
print getJawMilimeters()
time.sleep(1)
openGripper()
moveLinear("AutoSort", 3)
time.sleep(1)
moveLinear("AutoSort", 4)
time.sleep(1)
moveLinear("AutoSort", 5)
closeGripper()
time.sleep(2.5)
print getJawMilimeters()
time.sleep(1)
moveLinear("AutoSort", 6)
time.sleep(1)
moveLinear("AutoSort", 7)
time.sleep(2)
moveLinear("AutoSort", 8)
time.sleep(2.5)
openGripper()
time.sleep(2.5)
print getWeight()
time.sleep(1)
moveLinear("AutoSort", 9)
time.sleep(1)
moveLinear("AutoSort", 10)
time.sleep(1)
moveLinear("AutoSort", 11)
time.sleep(1)
closeGripper()
time.sleep(2.5)
print getJawMilimeters()
time.sleep(1)
moveLinear("AutoSort", 12)
time.sleep(1)
moveLinear("AutoSort", 13)
time.sleep(1)
moveLinear("AutoSort", 14)
