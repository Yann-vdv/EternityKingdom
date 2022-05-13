public class Enemy : Characters
{
    public TypeMobs type;
    public Enemy(Coordonnee c, TypeMobs t) : base(c)
    {
        this.type = t;
    }
}