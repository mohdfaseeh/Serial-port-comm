using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

namespace COMPort_Test
{
    class Program
    {
        static SerialPort _serialPort = new SerialPort();         //<-- declares a SerialPort Variable to be used throughout the form
        static int BaudRate = 9600;
        static void Main(string[] args)
        {

            string[] portNames = SerialPort.GetPortNames(); //<-- Reads all available comPorts

            Console.WriteLine(portNames.Length + " ports found!");
            foreach (var portName in portNames)
            {
                Console.WriteLine(portName);
            }

            Console.Write("Select a Port: ");
            string input = Console.ReadLine();
            int index = Convert.ToInt32(input) - 1;

            //<-- This block ensures that no exceptions happen
            if (_serialPort != null && _serialPort.IsOpen)
                _serialPort.Close();
            if (_serialPort != null)
                _serialPort.Dispose();
            //<-- End of Block
            _serialPort = new SerialPort("COM5", BaudRate, Parity.None, 8, StopBits.One);
            _serialPort.Open();//<-- Creates new SerialPort using the name selected in the combobox
            _serialPort.DataReceived += _serialPort_DataReceived;       //<-- this event happens everytime when new data is received by the ComPort
            //_serialPort.Open();     //<-- make the comport listen
            Console.WriteLine("Listening on " + _serialPort.PortName + "...\r\n");
            Console.ReadLine();
        }

        static string text;
        private static void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            while (_serialPort.BytesToRead > 0) //<-- repeats until the In-Buffer is empty
            {
                text += string.Format("{0:X2} ", _serialPort.ReadByte());
                Console.WriteLine(text + "\n");

                //<-- bytewise adds inbuffer to textbox
            }
        }
    }
}
