using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Microsoft.SPOT;
using Microsoft.SPOT.Hardware;
using SecretLabs.NETMF.Hardware;
using SecretLabs.NETMF.Hardware.Netduino;

namespace traficlight
{
    public class Program
    {
        static int _timeout = 2400;
        static int _p = 1200;
        static OutputPort _red;
        static OutputPort _yellow;
        static OutputPort _green;
   
        public static void Main()
        {
            _red = new OutputPort(Pins.GPIO_PIN_D0, false);
            _yellow = new OutputPort(Pins.GPIO_PIN_D1, false);
            _green = new OutputPort(Pins.GPIO_PIN_D2, false);

            while (true)
            {
                _turnon();
            }
        }

        public static void _turnon()
        {
            //only redlight
            _red.Write(true);
            Thread.Sleep(_timeout);
           
            //redlight + yellowlight + turnoff redlight
            if (_red.Read() == true)
            {
                _yellow.Write(true);
                Thread.Sleep(_p);
                _red.Write(false);
                Thread.Sleep(_p);
            }
            bool _ON1 = false;

            //go from yellowlight too greeenlight
            if (_ON1 == false)
            {
                _green.Write(true);
                _yellow.Write(false);
                Thread.Sleep(_timeout);
            }
            bool _ON2 = false;

            //go from greenlight to yellowlight
            if (_ON2 == false)
            {
                _yellow.Write(true);
                _green.Write(false);
                Thread.Sleep(_timeout);
            }
            bool _ON3 = false;

            //go from ywllowlight to redlight
            if (_ON3 == false)
            {
                _red.Write(true);
                _yellow.Write(false);
                Thread.Sleep(_timeout);
            }
            bool _OFF = false;

            //make a timeout before restart

            if (_OFF == false)
            {
                _red.Write(false);
                Thread.Sleep(_timeout);
                _ON1 = true;
                _ON2 = true;
                _ON3 = true;
                _OFF = true;
            }
            
        }

    }

}

