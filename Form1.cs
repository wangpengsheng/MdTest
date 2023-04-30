using System.Collections.Generic;
using System.Xml;
using Timer = System.Windows.Forms.Timer;

namespace Demo
{
    public partial class Form1 : Form
    {

        public readonly ModbusPresenter ModbusPresenter;
        private readonly Timer _timer = new();
        public Form1()
        {
            InitializeComponent();
            ModbusPresenter = new ModbusPresenter();
            ModbusPresenter.LoadSubscriptionsFromFile("subscriptions.json", this);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _timer.Interval = 1000;
            _timer.Tick += (delegate
            {
                if (richTextBox1.Text.Length > 10000)
                {
                    richTextBox1.Text = "";
                }
            });
            _timer.Start();
        }

        // 处理数据变化事件的方法
        public void Master1HandleDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                });
            }
        }

        // 处理数据变化事件之前的方法
        public void Master1BeforeDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"Before data changed: Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                });
            }
        }

        // 处理数据变化事件之后的方法
        public void Master1AfterDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"After data changed: Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                    ModbusPresenter._modbusMasters[masterName]
                        .WriteMultipleRegistersAsync(slaveId, address, new ushort[] { 1 });
                });
            }
        }

        // 处理数据变化事件的方法
        public void Master2HandleDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                });
            }
        }

        // 处理数据变化事件之前的方法
        public void Master2BeforeDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"Before data changed: Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                });
            }
        }

        // 处理数据变化事件之后的方法
        public void Master2AfterDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"After data changed: Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                    ModbusPresenter._modbusMasters[masterName]
                        .WriteMultipleRegistersAsync(slaveId, address, new ushort[] { 1 });
                });
            }
        }


        // 处理数据变化事件的方法
        public void Master1_5_HandleDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                });
            }
        }

        // 处理数据变化事件之前的方法
        public void Master1_5_BeforeDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {

                    richTextBox1.Text += $@"Before data changed: Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                });
            }
        }

        // 处理数据变化事件之后的方法
        public void Master1_5_AfterDataChanged(string masterName, byte slaveId, ushort address, ushort value)
        {
            if (richTextBox1.InvokeRequired)
            {
                richTextBox1.Invoke(() =>
                {
                    richTextBox1.Text += $@"After data changed: Master: {masterName}, SlaveId: {slaveId}, Address: {address}, Value: {value}" + Environment.NewLine;
                    ModbusPresenter._modbusMasters[masterName]
                        .WriteMultipleRegistersAsync(slaveId, address, new ushort[] { 1 });
                });
            }
        }

        private void UpdateButton_Click(object sender, EventArgs e)
        {
            Task.Run(async () =>
            {
                while (!isPause)
                {
                    await ModbusPresenter.UpdateDataAsync("Master1", 1, 0, 10);
                    await ModbusPresenter.UpdateDataAsync("Master2", 1, 0, 10);
                    await Task.Delay(100);
                }
            });
        }

        private void button1_Click(object sender, EventArgs e)
        {
            isPause = !isPause;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = @"";
        }


        public bool isPause { get; set; }
    }
}