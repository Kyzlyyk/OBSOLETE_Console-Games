namespace Mario;

internal interface IRigidble
{
    public bool ColliderIsOn { get; set; }

    public int ColliderCenterX { get; set; }
    public int ColliderCenterY { get; set; }

    public int ColliderWidth { get; set; }
    public int ColliderHeight { get; set; }
}
