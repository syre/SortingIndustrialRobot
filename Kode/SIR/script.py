vector = ControlSystem.RelCoordSirVector("Sam")
point = ControlSystem.VecPoint(200000, 200000, 100000, 100000, 1000000)
point2 = ControlSystem.VecPoint(-200000, -200000, -100000, -100000, -1000000)
vector.addPoint(point)
vector.addPoint(point2)
defineRelativeVector(vector)
moveByRelativeVector(vector)

vector = ControlSystem.AbsCoordSirVector("Sam")
point = ControlSystem.VecPoint(352711, 61079, 123529, -93333, 16881)
vector.addPoint(point)
defineAbsoluteVector(vector)
moveByAbsoluteVector(vector)


brickPosition = 352711 61079 123529 -93333 16881


import time
for i in range(0,3):
	closeGripper()
	time.sleep(2)
	print getJawMilimeters()
	openGripper()
	time.sleep(3)
print getWeight()
time.sleep(0.5)
print getWeight()
time.sleep(0.5)
print getWeight()
