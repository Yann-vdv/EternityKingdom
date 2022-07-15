using Microsoft.Xna.Framework;

public class Enemy : Characters
{
    public TypeMobs Type;
    public string NameSprite { get; set; }
    public Coordonnee _Position;
    public Entity Target { get; set; }
    public Enemy(Coordonnee coordonne, TypeMobs type) : base(coordonne)
    {
        this.Colision = new Colision(100, 50, this);
        this.Type = type;
        this._Position = coordonne;
        this.Damage = 1;
    }
    public void SetTarget(Entity target)
    {
        this.Target = target;
    }
    public string SetNameSprite(TypeSprite typeSprite)
    {
        this.NameSprite = $"{this.Type}_{typeSprite}_{this.Id}";
        return this.NameSprite;
    }
    public void UpdateTarget(GamePlayInfo gpi)
    {
        if (this.Colision.IsOut(Target.Co))
        {
            var t = new Entity();
            SetTarget(t);
            foreach (Entity e in this.Colision.WhoIn())
            {
                switch (e)
                {
                    case (Joueur j):
                        SetTarget(j);
                        break;
                        // case (Structure s):
                        //     SetTarget(s);
                        //     break;
                }
            }
            if (this.Target == t || this.Target == null)
            {
                SetTarget(gpi.J1);
            }
        }
    }
}