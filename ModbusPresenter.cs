using NModbus;
using NModbus.Device;
using System.Net.Sockets;
using Newtonsoft.Json;
using System.Reflection;

namespace Demo;

public class Subscription
{
    public string MasterName { get; set; }
    public byte SlaveId { get; set; }
    public ushort Address { get; set; }
    public string OnDataChangedFunction { get; set; }
    public string BeforeDataChangedFunction { get; set; }
    public string AfterDataChangedFunction { get; set; }
}


public class ModbusPresenter
{
    public Dictionary<string, IModbusMaster> _modbusMasters = new Dictionary<string, IModbusMaster>();
    private Dictionary<string, DataMappingCollection> _dataMappingCollections = new Dictionary<string, DataMappingCollection>();
    private Dictionary<string, List<Subscription>> _subscriptions = new Dictionary<string, List<Subscription>>();


    public ModbusPresenter()
    {
        // 初始化Modbus Masters
        InitializeModbusMaster("Master1", "127.0.0.1", 502);
        InitializeModbusMaster("Master2", "127.0.0.1", 502);
    }

    private void InitializeModbusMaster(string name, string ipAddress, int port)
    {
        var tcpClient = new TcpClient(ipAddress, port);
        _modbusMasters[name] = new ModbusFactory().CreateMaster(tcpClient);
        _dataMappingCollections[name] = new DataMappingCollection();
        _subscriptions[name] = new List<Subscription>();
    }

    public void LoadSubscriptionsFromFile(string filePath, object eventHandler)
    {
        var json = File.ReadAllText(filePath);
        var subscriptions = JsonConvert.DeserializeObject<List<Subscription>>(json);

        foreach (var subscription in subscriptions)
        {
            AddSubscription(subscription, eventHandler);
        }
    }


    public void AddSubscription(Subscription subscription, object eventHandler)
    {
        var master = _modbusMasters[subscription.MasterName];
        var dataMappingCollection = _dataMappingCollections[subscription.MasterName];
        var subscriptions = _subscriptions[subscription.MasterName];

        var onDataChangedMethodInfo = eventHandler.GetType().GetMethod(subscription.OnDataChangedFunction);
        if (onDataChangedMethodInfo == null)
        {
            throw new InvalidOperationException($"Method '{subscription.OnDataChangedFunction}' not found in the event handler object.");
        }

        MethodInfo beforeDataChangedMethodInfo = null;
        if (!string.IsNullOrEmpty(subscription.BeforeDataChangedFunction))
        {
            beforeDataChangedMethodInfo = eventHandler.GetType().GetMethod(subscription.BeforeDataChangedFunction);
            if (beforeDataChangedMethodInfo == null)
            {
                throw new InvalidOperationException($"Method '{subscription.BeforeDataChangedFunction}' not found in the event handler object.");
            }
        }

        MethodInfo afterDataChangedMethodInfo = null;
        if (!string.IsNullOrEmpty(subscription.AfterDataChangedFunction))
        {
            afterDataChangedMethodInfo = eventHandler.GetType().GetMethod(subscription.AfterDataChangedFunction);
            if (afterDataChangedMethodInfo == null)
            {
                throw new InvalidOperationException($"Method '{subscription.AfterDataChangedFunction}' not found in the event handler object.");
            }
        }

        subscriptions.Add(subscription);
        dataMappingCollection.DataChanged += (sender, e) =>
        {
            if (e.Address != subscription.Address) 
                return;
            beforeDataChangedMethodInfo?.Invoke(eventHandler, new object[] { subscription.MasterName, subscription.SlaveId, e.Address, e.Value });
            onDataChangedMethodInfo.Invoke(eventHandler, new object[] { subscription.MasterName, subscription.SlaveId, e.Address, e.Value });
            afterDataChangedMethodInfo?.Invoke(eventHandler, new object[] { subscription.MasterName, subscription.SlaveId, e.Address, e.Value });
        };
    }

    public async Task UpdateDataAsync(string masterName, byte slaveId, ushort startAddress, ushort numberOfRegisters)
    {
        var master = _modbusMasters[masterName];
        var dataMappingCollection = _dataMappingCollections[masterName];

        var data = await master.ReadHoldingRegistersAsync(slaveId, startAddress, numberOfRegisters);

        for (var i = 0; i < data.Length; i++)
        {
            dataMappingCollection.UpdateData((ushort)(startAddress + i), data[i]);
        }
    }
}