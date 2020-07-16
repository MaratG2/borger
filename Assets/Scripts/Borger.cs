public class Borger
{
    private float[] _sideRoasting;

    public float GetRoast(ESide side) => _sideRoasting[(int)side];
    public float SetRoast(ESide side, float value) => 
        _sideRoasting[(int) side] = value;
    public float AddRoast(ESide side, float value) => 
        _sideRoasting[(int) side] += value;

    public float[] Roast => _sideRoasting;

    public Borger()
    {
        _sideRoasting = new float[2];
    }
}

public enum ESide
{
    BOTTOM,
    TOP
}