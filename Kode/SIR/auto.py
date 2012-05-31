Slæt hånden klar til måling: 350132 58727 171901 -91863 12974
mål Klods ved position 1: 349980 58746 117367 -91935 13010
flyt hånden op og drej 90 grader: 350756 58832 171901 -91720 103189
Mål klods ved position 2: 350705 58823 121557 -91684 103154
træk den op: 350855 58759 292952 -91612 103082

smid klods ved vægten: 5733 391635 86829 -1075 183691
hånd klar til måling 3: 4299 397196 130235 -89175 277455
mål klods ved position 3: 4298 397121 71287 -89175 277455

vector = ControlSystem.AbsCoordSirVector("AutoSort")
point1 = ControlSystem.VecPoint(350132, 58727, 171901, -91863, 12974)
point2 = ControlSystem.VecPoint(349980, 58746, 117367, -91935, 13010)
point3 = ControlSystem.VecPoint(350132, 58727, 171901, -91863, 12974)
point4 = ControlSystem.VecPoint(350756, 58832, 171901, -91720, 103189)
point5 = ControlSystem.VecPoint(350705, 58823, 121557, -91684, 103154)
point6 = ControlSystem.VecPoint(350855, 58759, 292952, -91612, 103082)
point7 = ControlSystem.VecPoint(18637, 395314, 280653, -91720, 181218)
point8 = ControlSystem.VecPoint(5733, 391635, 86829, -1075, 183691)
point9 = ControlSystem.VecPoint(4299, 397196, 130235,-89175, 277455)
point10 = ControlSystem.VecPoint(4298, 397121, 71287, -89175, 277455)
vector.addPoint(point1)
vector.addPoint(point2)
vector.addPoint(point3)
vector.addPoint(point4)
vector.addPoint(point5)
vector.addPoint(point6)
vector.addPoint(point7)
vector.addPoint(point8)
vector.addPoint(point9)
vector.addPoint(point10)
defineAbsoluteVector(vector)
teach(vector)

import time
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

	