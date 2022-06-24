using Microsoft.Xna.Framework;
public class Enemy : Characters
{
    public TypeMobs Type;
    public string NameSprite { get; set; }
    public Coordonnee _Position;
    public Vector2 Target { get; set; }
    public Enemy(Coordonnee coordonne, TypeMobs type) : base(coordonne)
    {
        this.Type = type;
        this._Position = coordonne;
    }
    public void SetTarget(Vector2 target)
    {
        this.Target = target;
    }
    public string SetNameSprite(TypeSprite typeSprite)
    {
        this.NameSprite = $"{this.Type}_{typeSprite}_{this.Id}";
        return this.NameSprite;
    }
}