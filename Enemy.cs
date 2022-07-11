using Microsoft.Xna.Framework;

public class Enemy : Characters
{
    public TypeMobs Type;
    public string NameSprite { get; set; }
    public Coordonnee _Position;
    public Coordonnee Target { get; set; }
    public Enemy(Coordonnee coordonne, TypeMobs type) : base(coordonne)
    {
        this.Type = type;
        this._Position = coordonne;
        // this.Colision=new Colision(100,50,);
    }
    public void SetTarget(Coordonnee target)
    {
        this.Target = target;
    }
    public string SetNameSprite(TypeSprite typeSprite)
    {
        this.NameSprite = $"{this.Type}_{typeSprite}_{this.Id}";
        return this.NameSprite;
    }
    public void UpdateTarget()
    {

    }
}