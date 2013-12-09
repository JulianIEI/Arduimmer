//-----------------------------------------------------------------------
// <copyright file="PicProgrammer.cs" company="none">
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
// <date>09/12/2013</date>
//-----------------------------------------------------------------------
using System;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace Arduimmer
{
	public class PicProgrammer : ArduinoCommunication
	{
		public PicProgrammer(string portName)
			: base(portName)
		{
		}

		public static PicProgrammer SearchArduino()
		{
			// DOES NOT RETURN /dev/ttyACM0 !!1!
			//string[] portNames = SerialPort.GetPortNames(); 
			string[] portNames = { "/dev/ttyACM0" };

			foreach (string portName in portNames) {
				PicProgrammer arduino = new PicProgrammer(portName);
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

		public bool Ping()
		{
			this.Write("Hey!");

			if (this.ReadLine() == "Yes?")
				return true;
			else
				return false;
		}

		public ushort GetDeviceId()
		{
			this.Write("Dev?");
			return Convert.ToUInt16(this.ReadLine(), 16);
		}

		public void CodeDevice(Hex code)
		{
			// 1º Enter Programming mode
			this.EnterProgrammingMode();

			// 2º Erase chip
			this.EraseChip();

			// 3º Send & verify code and data EEPROM
			this.WriteCode(code);
			this.VerifyCode(code);

			// 4º Send & verify configuration bits
			this.WriteConfBits(code);
			this.VerifyConfBits(code);

			// 5º Exit Programming mode
			this.ExitProgrammingMode();
		}

		private void EnterProgrammingMode()
		{
			if (!this.Ping())
				throw new IOException("Can not communicate with Arduino");

			this.Write("Goo!");
		}

		private void ExitProgrammingMode()
		{
			if (!this.Ping())
				throw new IOException("Can not communicate with Arduino");

			this.Write("End!");
		}

		private bool EraseChip()
		{
			if (!this.Ping())
				throw new IOException("Can not communicate with Arduino");

			this.Write("Era!");
			Thread.Sleep(1000);
			return this.ReadLine() == "Erase done";
		}

		private void WriteCode(Hex code)
		{
			throw new NotImplementedException();
		}

		private void VerifyCode(Hex code)
		{
			throw new NotImplementedException();
		}

		private void WriteConfBits(Hex code)
		{
			throw new NotImplementedException();
		}

		private void VerifyConfBits(Hex code)
		{
			throw new NotImplementedException();
		}
	}
}

