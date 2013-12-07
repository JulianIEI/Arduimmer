//-----------------------------------------------------------------------
// <copyright file="ArduinoCommunication.cs" company="none">
// Copyright (C) 2013
//
//   This program is free software: you can redistribute it and/or modify
//   it under the terms of the GNU General Public License as published by 
//   the Free Software Foundation, either version 3 of the License, or
//   (at your option) any later version.
//
//   This program is distributed in the hope that it will be useful, 
//   but WITHOUT ANY WARRANTY; without even the implied warranty of
//   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//   GNU General Public License for more details. 
//
//   You should have received a copy of the GNU General Public License
//   along with this program.  If not, see "http://www.gnu.org/licenses/". 
// </copyright>
// <author>pleoNeX</author>
// <email>benito356@gmail.com</email>
// <date>07/12/2013</date>
//-----------------------------------------------------------------------
using System;
using System.IO.Ports;

namespace Arduimmer
{
	public class ArduinoCommunication
	{
		private const int      BaudRate     = 9600;
		private const Parity   ParityMode   = Parity.None;
		private const int      DataBits     = 8;
		private const StopBits StopBitsMode = StopBits.One;
		private const int      ReadTimeOut  = 500;
		private const string   NewLine      = "\r\n";

		private SerialPort port;

		public ArduinoCommunication(string portName)
		{
			this.PortName = portName;
			this.port = new SerialPort() { 
				PortName    = portName, 
				BaudRate    = BaudRate, 
				Parity      = ParityMode, 
				DataBits    = DataBits, 
				StopBits    = StopBitsMode,
				NewLine     = NewLine,
				ReadTimeout = ReadTimeOut,
				RtsEnable   = true,
			    DtrEnable   = true
			};
		}

		public string PortName {
			get;
			private set;
		}

		public static ArduinoCommunication SearchArduino()
		{
			// DOES NOT RETURN /dev/ttyACM0 !!1!
			//string[] portNames = SerialPort.GetPortNames(); 
			string[] portNames = { "/dev/ttyACM0" };

			foreach (string portName in portNames) {
				ArduinoCommunication arduino = new ArduinoCommunication(portName);
				try {
					arduino.Open();
					bool isArduino = arduino.Ping();
					arduino.Close();

					if (isArduino)
						return arduino;
				} catch { }
			}

			return null;
		}

		public void Open()
		{
			this.port.Open();
		}

		public void Close()
		{
			this.port.Close();
		}

		public bool Ping()
		{
			this.port.Write("Hey!");

			if (this.port.ReadLine() == "Yes?")
				return true;
			else
				return false;
		}
	}
}
