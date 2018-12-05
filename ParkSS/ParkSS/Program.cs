using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;

namespace ParkSS
{
    class Program
    {
        MqttClient mqttClient = null;
        string[] topics = { "spotsParkA", "spotsParkB", "parksInfo" };

        static void Main(string[] args)
        {
            Program program = new Program();
            program.Init();
        }

        private void Init()
        {
            mqttClient = new MqttClient("127.0.0.1");
            mqttClient.Connect(Guid.NewGuid().ToString());
            if (!mqttClient.IsConnected)
            {
                Console.WriteLine("client not connected");
            }

            mqttClient.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;

            byte[] qosLevels = { MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE, MqttMsgBase.QOS_LEVEL_EXACTLY_ONCE };
            mqttClient.Subscribe(topics, qosLevels);
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            switch (e.Topic)
            {
                case "spotsParkA":
                    Console.WriteLine($"{e.Topic} : {Encoding.UTF8.GetString(e.Message)} spotsParkA" + Environment.NewLine);
                    break;
                case "spotsParkB":
                    Console.WriteLine($"{e.Topic} : {Encoding.UTF8.GetString(e.Message)} spotsParkB" + Environment.NewLine);
                    break;
                case "parksInfo":
                    Console.WriteLine($"{e.Topic} : {Encoding.UTF8.GetString(e.Message)} INFO" + Environment.NewLine);
                    break;
            }
        }
    }
}
