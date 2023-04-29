namespace Demo;

public class DataChangedEventArgs : EventArgs
{
    public ushort Address { get; set; }
    public ushort Value { get; set; }
}


public class DataMappingCollection
{
    private Dictionary<ushort, ushort> _data = new Dictionary<ushort, ushort>();

    public event EventHandler<DataChangedEventArgs> DataChanged;

    public void UpdateData(ushort address, ushort value)
    {
        if (!_data.ContainsKey(address) || _data[address] != value)
        {
            _data[address] = value;
            OnDataChanged(address, value);
        }
    }

    protected virtual void OnDataChanged(ushort address, ushort value)
    {
        DataChanged?.Invoke(this, new DataChangedEventArgs { Address = address, Value = value });
    }
}