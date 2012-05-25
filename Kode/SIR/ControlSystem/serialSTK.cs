/** \file serialSTK.cs */
/** \author Robotic Global Organization(RoboGO) */
using System;
using System.Collections.Generic;
using System.IO.Ports;

namespace ControlSystem
{
    /// <summary>
    /// Class for functions to communicating with the STK kit.(Serial communication.)
    /// </summary>
    public class SerialSTK
    {
        
            private SerialPort sp;
            /// <summary>
            /// Classconstructor
            /// </summary>
            /// <param name="port"> Comport to communicate through </param>
            /// <param name="baud"> baudrate (has to be the same as the STK-kit) </param>
            /// <param name="parity"> Parity </param>
            /// <param name="dataBits"> Databits </param>
            /// <param name="stopBits"> Stopbits </param>
            public SerialSTK(int baud = 9600, Parity parity = Parity.None, int dataBits = 8, StopBits stopBits = StopBits.One)
            {
                string[] ports = SerialPort.GetPortNames();

                foreach (string port in ports)
                {
                    sp = new SerialPort(port, baud, parity, dataBits, stopBits);
                }
            }


            /// <summary>
            /// Opens for communication
            /// </summary>
            /// <returns> True if opened, false otherwise </returns>
            public bool Open()
            {
                if (!sp.IsOpen)
                    sp.Open();

                if (!sp.IsOpen)
                    return true;
                return false;
            }

            /// <summary>
            /// Closes for communication
            /// </summary>
            /// <returns> True if closed, false otherwise </returns>
            public bool Close()
            {
                if (sp.IsOpen)
                    sp.Close();

                if (!sp.IsOpen)
                    return true;
                return false;
            }

            /// <summary>
            /// Reads an ADC value
            /// </summary>
            /// <returns> Value from the ADC </returns>
            public double ReadADC()
            {
                double temp;
                sp.Open();
                sp.Write("R\n");
                temp = Convert.ToDouble(sp.ReadLine());
                temp = (double)950 / (double)1023 * Math.Round(temp, 2);
                sp.Close();
                return temp;
            }
    }
}
