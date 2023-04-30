using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.VisualBasic.Devices;

namespace Demo
{
    public partial class AlarmFm : Form
    {
        public readonly Dictionary<string, AlarmItem> AlarmDictionary = new();
        private readonly object _lock = new();


        public AlarmFm()
        {
            InitializeComponent();
        }

        private void AddAlarm(string message)
        {
            lock (_lock)
            {
                var strArrange = message.Split(',');
                Action alarmAction = () =>
                {
                    try
                    {
                        var alarmItem = new AlarmItem();
                        if (strArrange.Length == 5) //"报警设备,报警号,报警的发生时间,报警的内容,报警的提示"
                        {
                            if (Enum.TryParse(strArrange[0], out AlarmType alarmType))
                            {
                                alarmItem.AlarmType = alarmType;
                            }
                            else
                            {
                                alarmType = AlarmType.Normal;
                            }

                            if (!AlarmDictionary.ContainsKey(alarmItem.AlarmMessage))
                            {

                            }
                        }
                       

                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        throw;
                    }
                };
            }
        }

    }
}
