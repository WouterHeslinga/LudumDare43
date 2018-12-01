public delegate void StatChanged(IStats stats);

public interface IStats
{
    StatChanged OnStatChanged { get; set; }
}
