namespace Mario;

internal abstract class Entity : IRigidble
{
    public abstract bool ColliderIsOn { get; set; }
   
    public abstract int ColliderCenterX { get; set; }
    public abstract int ColliderCenterY { get; set; }

    public abstract int ColliderWidth { get; set; }
    public abstract int ColliderHeight { get; set; }

    public abstract void Draw();

    public abstract void Clear();
}
